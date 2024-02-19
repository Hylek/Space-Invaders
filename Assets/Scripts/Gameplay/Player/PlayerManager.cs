using System;
using Gameplay.Common;
using UnityEngine;
using UnityEngine.InputSystem;

// Made by Daniel Cumbor in 2024.

namespace Gameplay.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static event Action PlayerHit;
        
        [SerializeField] private int maxLives;
        [SerializeField] private float speed;
        
        private StandardControlMap _input;
        private Lives _playerLives;
        private Weapon _weapon;
        private SpriteRenderer _renderer;
        private float _smoothing, _minXClamp, _maxXClamp;
        private Vector3 _moveVector;

        private void Awake()
        {
            _weapon = GetComponent<Weapon>();
            _renderer = GetComponent<SpriteRenderer>();
            
            _playerLives = new Lives(maxLives);
            _input = new StandardControlMap();

            _playerLives.NoLivesRemaining += OnNoLivesRemaining;
            _input.Player.Fire.performed += OnFireAction;
            
            if (Camera.main == null) return;
            
            _minXClamp = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + _renderer.size.x / 2;
            _maxXClamp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - _renderer.size.x / 2;
        }

        private void OnFireAction(InputAction.CallbackContext obj) => _weapon.Fire();

        private void Update()
        {
            MoveHorizontally();
        }
        
        private void MoveHorizontally()
        {
            // Read input value
            _moveVector = _input.Player.Movement.ReadValue<Vector2>();
            
            // Calculate target position based on input and screen bounds
            var position = transform.position;
            var targetPosition = position;
            targetPosition.x += _moveVector.x * speed * Time.deltaTime;
            targetPosition.x = Mathf.Clamp(targetPosition.x, _minXClamp, _maxXClamp);

            // Smoothly damp towards target position
            var newPosition = Vector3.SmoothDamp(position, targetPosition,
                ref _moveVector, _smoothing, speed, Time.deltaTime);

            // Update position with clamping
            position = newPosition;
            transform.position = position;
        }

        private void OnEnable() => _input.Enable();

        private void OnDisable() => _input.Disable();

        private void OnNoLivesRemaining()
        {
            // todo
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var bullet = other.GetComponent<Bullet>();
            if (bullet)
            {
                if (bullet.IsBulletFriendly()) return;
                
                Debug.Log("Player hit, taking damage!");
                
                var damage = bullet.GetDamage();
                _playerLives.RemoveLife(damage);
                PlayerHit?.Invoke();
            }
            else
            {
                Debug.LogWarning("Could not get bullet script from colliding object!");
            }
        }

        private void OnDestroy()
        {
            _input.Player.Fire.performed -= OnFireAction;
            _input.Disable();
            _input.Dispose();
            
            _playerLives.NoLivesRemaining -= OnNoLivesRemaining;
        }
    }
}