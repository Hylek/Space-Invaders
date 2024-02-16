using UnityEngine;

namespace Utils
{
    public interface IObjectPool
    {
        void Init();    // Method for starting behaviour for poolable objects.
        void Reset();   // Method for resetting behaviour for poolable objects.
    }
    
    public class PoolableObject : MonoBehaviour, IObjectPool
    {
        public virtual void Init()
        {
            //
        }

        public virtual void Reset()
        {
            // 
        }
    }
}