using System;
using Gameplay.Common;
using Gameplay.Weaponry;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private int maxLives;
        [SerializeField] private float speed;
        
        private PlayerInputHandler _inputHandler;
        private Lives _playerLives;
        private Weapon _weapon;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _weapon = GetComponent<Weapon>();
            _renderer = GetComponent<SpriteRenderer>();
            
            _inputHandler = new PlayerInputHandler();
            _playerLives = new Lives(maxLives);
            _inputHandler.Init(speed, _renderer.size.x);

            _playerLives.NoLivesRemaining += OnNoLivesRemaining;
        }

        private void Update()
        {
            _inputHandler.Update(transform);
        }

        private void OnEnable()
        {
            _inputHandler.Enable();
        }

        private void OnDisable()
        {
            _inputHandler.Disable();
        }
        
        private void OnNoLivesRemaining()
        {
            // todo
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Player hit!");
        }

        private void OnDestroy()
        {
            _inputHandler.Disable();
            _inputHandler.Dispose();
            _playerLives.NoLivesRemaining -= OnNoLivesRemaining;
        }
    }
}