using System;
using UnityEngine;
using Utils;

namespace Gameplay.Weaponry
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private ObjectPool bulletPool;
        [SerializeField] private float fireRate;

        private float _currentRate;
        private bool _isOnCooldown;

        private void Awake()
        {
            _currentRate = fireRate;
        }

        public void Fire()
        {
            var bullet = bulletPool.GetObject();
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Shoot(transform);
        }

        private void Update()
        {
            if (!_isOnCooldown) return;
            
            _currentRate -= Time.deltaTime;
            
            if (!(_currentRate <= 0)) return;
            
            _isOnCooldown = false;
            _currentRate = fireRate;
        }
    }
}