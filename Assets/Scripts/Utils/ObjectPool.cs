using System.Collections.Generic;
using UnityEngine;

// Made by Daniel Cumbor in 2024.

namespace Utils
{
    /// <summary>
    /// Simple class that implements the object pool pattern for optimal recycling of objects.
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private PoolableObject poolObjectPrefab;   // The object to be used in this pool.
        [SerializeField] private int startingPoolSize;              // Pre-warm the pool with an initial amount of objects.
        [SerializeField] private bool expandDynamically;            // Should this pool expand past it's initial size?

        private List<PoolableObject> _pool;                         // The pool list itself where pooled objects are stored.

        public void Awake()
        {
            _pool = new List<PoolableObject>();
            
            if (startingPoolSize <= 0) return;
            
            for (var i = 0; i < startingPoolSize; i++)
            {
                var newObject = Instantiate(poolObjectPrefab, transform);
                newObject.Init();
                _pool.Add(newObject);
            }
        }

        /// <summary>
        /// Get an object from the pool. If ExpandDynamically is true,
        /// and the pool is empty; a new object will be made.
        /// </summary>
        /// <returns>An object from the pool, ready to be used.</returns>
        public PoolableObject GetObject()
        {
            if (_pool.Count <= 0)
            {
                if (expandDynamically)
                {
                    var newObject = Instantiate(poolObjectPrefab, transform);
                    newObject.Init();
                    
                    // Do not store it as we plan to immediately return it.
                    return newObject;
                }

                Debug.LogWarning("The pool has no available objects!");

                return null;
            }

            var objectToRelease = _pool[0];      
            objectToRelease.Init();
            _pool.RemoveAt(0); 
                
            return objectToRelease;
        }
        
        /// <summary>
        /// Return the object back to the pool to be recycled.
        /// </summary>
        /// <param name="objectToReturn">The object to return to the pool.</param>
        public void ReturnObject(PoolableObject objectToReturn)
        {
            objectToReturn.transform.parent = transform;
            objectToReturn.Reset();
            _pool.Add(objectToReturn);
        }
    }
}
