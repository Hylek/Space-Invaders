using System;
using UnityEngine;

namespace Gameplay.Invaders
{
    public class Invader : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"Something has struck {transform.name}");
        }
    }
}