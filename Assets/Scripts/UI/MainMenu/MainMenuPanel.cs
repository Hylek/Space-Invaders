using UI.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuPanel : BasePanel
    {
        [SerializeField] private BasePanel optionsPanel;
        [SerializeField] private Button startButton, optionsButton, exitButton;

        private bool _startingGame;

        protected override void Awake()
        {
            base.Awake();
            
            startButton.onClick.AddListener(OnStartButton);
            optionsButton.onClick.AddListener(OnOptionsButton);
            exitButton.onClick.AddListener(OnQuitButton);
            
            startButton.interactable = false;
            optionsButton.interactable = false;
            exitButton.interactable = false;
            _startingGame = false;
        }

        public override async void Open()
        {
            await StartTransition(PanelStatus.Visible);

            startButton.interactable = true;
            optionsButton.interactable = true;
            exitButton.interactable = true;
        }

        public override async void Close()
        {
            startButton.interactable = false;
            optionsButton.interactable = false;
            exitButton.interactable = false;

            await StartTransition(PanelStatus.Hidden);

            if (_startingGame)
            {
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
            }
        }

        private void OnStartButton()
        {
            _startingGame = true;
            Close();
        }

        private void OnOptionsButton() => Manager.GoToNextPanel(this);
        
        private void OnQuitButton() => Application.Quit();
    }
}