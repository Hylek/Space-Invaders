using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private PlayerInputHandler _inputHandler;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _inputHandler = new PlayerInputHandler();
            _inputHandler.Init(speed, _renderer.size.x);
        }

        private void Update()
        {
            _inputHandler.Update(transform);
        }

        private void OnEnable()
        {
            _inputHandler.Enable();
        }

        private void OnDisable()
        {
            _inputHandler.Disable();
        }

        private void OnDestroy()
        {
            _inputHandler.Disable();
            _inputHandler.Dispose();
        }
    }
}