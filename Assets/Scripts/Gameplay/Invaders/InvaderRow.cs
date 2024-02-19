using System.Collections.Generic;
using UnityEngine;

// Made by Daniel Cumbor in 2024.

namespace Gameplay.Invaders
{
    public class InvaderRow : MonoBehaviour
    {
        private List<Invader> _invaders;

        private void Awake() => _invaders = new List<Invader>();

        public void AddInvader(Invader invader)
        {
            _invaders.Add(invader);
            invader.transform.parent = transform;
        }

        public void ClearInvaders() => _invaders.Clear();

        public int GetInvaderCount() => _invaders.Count;

        public void Order()
        {
            var invaderRef = _invaders[0];
            var totalWidth = invaderRef.GetComponent<Renderer>().bounds.size.x * _invaders.Count + 1.0f;
            var halfWidthOffset = totalWidth / 2f;
            
            for (var i = 0; i < _invaders.Count; i++)
            {
                var invader = _invaders[i];
                invader.transform.position = new Vector3(i * 1.0f - halfWidthOffset, 0f, 0f);
            }
        }
    }
}