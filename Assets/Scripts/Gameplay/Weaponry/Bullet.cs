using System;
using UnityEngine;
using Utils;

namespace Gameplay.Weaponry
{
    public class Bullet : PoolableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private int damageToInflict;

        private bool _canMove;

        public void Shoot(Transform transform)
        {
            transform.position = transform.position;
        }

        private void Update()
        {
            if (_canMove)
            {
                transform.Translate(Vector3.up * (speed * Time.deltaTime));
            }
        }
    }
}