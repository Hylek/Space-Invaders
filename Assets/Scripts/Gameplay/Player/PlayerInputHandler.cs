using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player
{
    public class PlayerInputHandler : IDisposable
    {
        private StandardControlMap _input;
        private Vector3 _moveVector;
        private float _speed, _smoothing, _minXClamp, _maxXClamp;
        private bool _isActive;

        public void Init(float speed, float playerWidth)
        {
            _input = new StandardControlMap();
            _speed = speed;

            _input.Player.Fire.performed += OnFireCalled;
            
            Enable();
                
            // Determine screen bounds
            if (Camera.main == null) return;
            
            _minXClamp = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + playerWidth / 2;
            _maxXClamp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - playerWidth / 2;
        }

        private void OnFireCalled(InputAction.CallbackContext obj)
        {
            
        }

        public void Enable()
        {
            _input.Enable();
            _isActive = true;
        }

        public void Update(Transform transform)
        {
            if (!_isActive) return;
            
            MoveHorizontally(transform);
        }

        private void MoveHorizontally(Transform transform)
        {
            // Read input value
            _moveVector = _input.Player.Movement.ReadValue<Vector2>();
            
            // Calculate target position based on input and screen bounds
            var position = transform.position;
            var targetPosition = position;
            targetPosition.x += _moveVector.x * _speed * Time.deltaTime;
            targetPosition.x = Mathf.Clamp(targetPosition.x, _minXClamp, _maxXClamp);

            // Smoothly damp towards target position
            var newPosition = Vector3.SmoothDamp(position, targetPosition,
                ref _moveVector, _smoothing, _speed, Time.deltaTime);

            // Update position with clamping
            position = newPosition;
            transform.position = position;
        }

        public void Disable()
        {
            _input.Disable();
            _isActive = false;
        }

        public void Dispose() => _input?.Dispose();
    }
}