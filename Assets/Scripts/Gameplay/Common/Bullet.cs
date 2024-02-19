using System;
using UnityEngine;
using Utils;

// Made by Daniel Cumbor in 2024.

namespace Gameplay.Common
{
    public class Bullet : PoolableObject
    {
        public event Action<Bullet> LifetimeReached;
        
        [SerializeField] private float speed;
        [SerializeField] private int damageToInflict;
        [SerializeField] private float lifetime;

        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;
        private bool _canMove, _isFriendly;
        private float _currentLifetime;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        public override void Init()
        {
            base.Init();

            _currentLifetime = lifetime;
            _spriteRenderer.color = Color.clear;
        }

        public override void Reset()
        {
            base.Reset();
            
            _spriteRenderer.color = Color.clear;
        }

        public void Shoot(Transform startTransform, bool isFriendly = false)
        {
            _spriteRenderer.color = Color.white;
            transform.position = startTransform.position;
            _canMove = true;
            _isFriendly = isFriendly;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Invader"))
            {
                _canMove = false;
                LifetimeReached?.Invoke(this);
            }
        }

        public bool IsBulletFriendly() => _isFriendly;

        public int GetDamage() => damageToInflict;

        private void Update()
        {
            if (!_canMove) return;
            
            transform.Translate(Vector3.up * (speed * Time.deltaTime));
            
            if (_currentLifetime > 0)
            {
                _currentLifetime -= Time.deltaTime;
            }
            else
            {
                _canMove = false;
                LifetimeReached?.Invoke(this); 
            }
        }
    }
}