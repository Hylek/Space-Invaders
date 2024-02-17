using System;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace Gameplay.Weaponry
{
    public class Bullet : PoolableObject
    {
        public event Action<Bullet> LifetimeReached;
        
        [SerializeField] private float speed;
        [SerializeField] private int damageToInflict;
        [SerializeField] private float lifetime;

        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;
        private bool _canMove;
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

        public void Shoot(Transform transform)
        {
            _spriteRenderer.color = Color.white;
            this.transform.position = transform.position;
            _canMove = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"Bullet has hit {other.transform.name}");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log($"Bullet has hit {other.transform.name}");
        }

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