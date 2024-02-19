using System;
using UnityEngine;
using Utils;

// Made by Daniel Cumbor in 2024.

namespace Gameplay.Invaders
{
    public class Invader : PoolableObject
    {
        public event Action<Invader> IsDead;

        private Transform _startPosition;
        private SpriteRenderer _renderer;
        private BoxCollider2D _collider2D;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<BoxCollider2D>();
            _startPosition = transform;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _renderer.color = Color.clear;
            _collider2D.enabled = false;
            transform.position = new Vector3(-1000, 0, 0);
            IsDead?.Invoke(this);
        }
    }
}