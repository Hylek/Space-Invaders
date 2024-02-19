using UnityEngine;
using Utils;

// Made by Daniel Cumbor in 2024.

namespace Gameplay.Invaders
{
    public class Invader : PoolableObject
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"Something has struck {transform.name}");
        }
    }
}