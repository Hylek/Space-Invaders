using UnityEngine;
using Utils;

// Made by Daniel Cumbor in 2024.

namespace Gameplay.Common
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private ObjectPool bulletPool;
        [SerializeField] private float fireRate;
        [SerializeField] private bool isPlayerWeapon;

        private float _currentRate;
        private bool _isOnCooldown;

        private void Awake()
        {
            _currentRate = fireRate;
        }

        public void Fire()
        {
            if (_isOnCooldown) return;
            
            var bullet = bulletPool.GetObject();
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.transform.parent = null;
            bulletScript.Shoot(transform, isPlayerWeapon);
            bulletScript.LifetimeReached += OnBulletLifetimeReached;
            _isOnCooldown = true;
        }

        private void OnBulletLifetimeReached(Bullet bullet)
        {
            bullet.LifetimeReached -= OnBulletLifetimeReached;
            bulletPool.ReturnObject(bullet);
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