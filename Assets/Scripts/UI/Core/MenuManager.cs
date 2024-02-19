using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

// Made by Daniel Cumbor in 2024.

namespace UI.Core
{
    /// <summary>
    /// Manager class responsible for all panels under it.
    /// </summary>
    public class MenuManager : ExtendedBehaviour
    {
        private List<BasePanel> _panels;
        private int _currentIndex;

        protected override void Awake()
        {
            base.Awake();

            _panels = new List<BasePanel>();

            // Get all available panels under this manager.
            // Note: Only goes 1 child deep.
            foreach (Transform t in transform)
            {
                var panel = t.GetComponent<BasePanel>();
                
                if (!panel) continue;
                
                panel.Init(this);
                _panels.Add(panel);
            }

            _currentIndex = 0;
        }

        private void Start()
        {
            // Open the 1st panel.
            _panels[0].Open();
        }

        /// <summary>
        /// Jump to a specific panel in the hierarchy.
        /// </summary>
        /// <param name="caller">The panel that made this request.</param>
        /// <param name="panelToOpen">The panel that we want to jump to.</param>
        public void GoToPanel(BasePanel caller, BasePanel panelToOpen)
        {
            caller.Close();
            panelToOpen.Open();
            
            DetermineCurrentIndex(panelToOpen);
        }

        /// <summary>
        /// Go to the next available panel in the hierarchy.
        /// </summary>
        /// <param name="caller">The panel that made this request.</param>
        public void GoToNextPanel(BasePanel caller)
        {
            caller.Close();
            _currentIndex++;

            if (_currentIndex >= _panels.Count)
            {
                Debug.LogWarning("We have reached the end of the menu timeline, no panel to open!");
                _currentIndex = _panels.Count - 1;

                return;
            }
            _panels[_currentIndex].Open();
        }
        
        /// <summary>
        /// Go to the previous available panel in the hierarchy.
        /// </summary>
        /// <param name="caller">The panel that made this request</param>
        public void GoToPreviousPanel(BasePanel caller)
        {
            caller.Close();
            _currentIndex--;

            if (_currentIndex <= 0)
            {
                Debug.LogWarning("We have reached the start of the menu timeline, no panel to open!");
                _currentIndex = 0;

                return;
            }
            _panels[_currentIndex].Open();
        }

        /// <summary>
        /// If we are jumping to a specific page, figure out what index we're currently at.
        /// </summary>
        /// <param name="panel">The panel we are going to.</param>
        private void DetermineCurrentIndex(BasePanel panel)
        {
            for (var i = 0; i < _panels.Count; i++)
            {
                var basePanel = _panels[i];
                
                if (panel != basePanel) continue;
                
                _currentIndex = i;

                break;
            }
        }
    }
}