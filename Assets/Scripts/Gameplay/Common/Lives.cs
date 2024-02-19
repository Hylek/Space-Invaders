using System;

// Made by Daniel Cumbor in 2024.

namespace Gameplay.Common
{
    /// <summary>
    /// Simple class that provides "lives" functionality to any given object.
    /// </summary>
    public class Lives
    {
        public event Action NoLivesRemaining;
        
        private readonly int _maxLives;
        private int _currentLifeCount;

        public Lives(int maxLives)
        {
            _maxLives = maxLives;
            _currentLifeCount = _maxLives;
        }

        /// <summary>
        /// Adds more lives to this object.
        /// </summary>
        /// <param name="lifeCount">The amount of lives to add.</param>
        public void AddLife(int lifeCount = 1)
        {
            _currentLifeCount += lifeCount;
            if (_currentLifeCount > _maxLives)
            {
                _currentLifeCount = _maxLives;
            }
        }

        /// <summary>
        /// Removes lives from this object.
        /// </summary>
        /// <param name="lifeCount">The amount of lives to remove.</param>
        public void RemoveLife(int lifeCount = 1)
        {
            _currentLifeCount -= lifeCount;
            
            if (_currentLifeCount > 0) return;
            
            NoLivesRemaining?.Invoke();
            _currentLifeCount = 0;
        }

        /// <summary>
        /// Resets the lives of this object back to the original max lives count.
        /// </summary>
        public void ResetLives() => _currentLifeCount = _maxLives;

        /// <summary>
        /// Gets the current life count for this object.
        /// </summary>
        /// <returns>How many lives this object currently has.</returns>
        public int GetCurrentLifeCount() => _currentLifeCount;

    }
}