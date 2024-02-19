using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

// Made by Daniel Cumbor in 2024.

namespace Core
{
    /// <summary>
    /// The overarching class that controls the high level functions of the game.
    /// Implements the Singleton pattern for global access.
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        public DifficultyData currentDifficulty;

        private List<DifficultyData> _allDifficulties;

        public override void Awake()
        {
            base.Awake();

            AcquireDifficultyData();
        }

        /// <summary>
        /// Finds all difficulty scriptable object resources and sets the "Default" type as the current difficulty.
        /// </summary>
        private void AcquireDifficultyData()
        {
            _allDifficulties = Resources.LoadAll<DifficultyData>("Difficulties").ToList();
            foreach (var difficulty in _allDifficulties.Where(difficulty => difficulty.type == DifficultyType.Default))
            {
                currentDifficulty = difficulty;
                
                break;
            }
        }
    }
}