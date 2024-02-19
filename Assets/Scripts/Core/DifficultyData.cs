using System.Collections.Generic;
using UnityEngine;

// Made by Daniel Cumbor in 2024.

namespace Core
{
    public enum DifficultyType
    {
        Default,
        Easy,
        Medium,
        Hard,
        Endless
    }
    
    /// <summary>
    /// Scriptable Object that stores variable fields that can alter the difficulty of the game.
    /// </summary>
    [CreateAssetMenu(menuName = "SpaceInvaders/Difficulty", fileName = "DifficultyX")]
    public class DifficultyData : ScriptableObject
    {
        public DifficultyType type;          // The type of difficulty.
        public int playerLives;              // How many lives the player should have.
        public float playerWeaponCooldown;   // How long should the weapon wait between shots.
        public float invaderMoveSpeed;       // How fast the Invader group should move.
        public List<int> rowLengths;         // Length of list determines rows, value determines invader count.
    }
}