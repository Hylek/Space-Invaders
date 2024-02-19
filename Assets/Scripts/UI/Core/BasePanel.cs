using System.Threading.Tasks;
using UnityEngine;
using Utils;

// Made by Daniel Cumbor in 2024.

public enum PanelStatus
{
    Hidden, Visible
}

namespace UI.Core
{
    /// <summary>
    /// Base class that handles common behaviours amongst all UI panels.
    /// Such as fading in and out. Extends from ExtendedBehaviour.
    /// This class should be attached to the parent of the panel that has a CanvasGroup attached.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class BasePanel : ExtendedBehaviour
    {
        protected CanvasGroup CanvasGroup;
        protected MenuManager Manager;

        public void Init(MenuManager manager)
        {
            Manager = manager;
            CanvasGroup = GetComponent<CanvasGroup>();

            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
            CanvasGroup.alpha = 0;
        }

        public virtual void Open() { }

        public virtual void Close() { }
        
        /// <summary>
        /// Begins a transition Task that allows you to wait for it to complete before attempting other logic
        /// This ensures that the screen is covered before performing visual changes.
        /// </summary>
        /// <param name="newStatus">What status would you like to move to, uncovered or covered screen.</param>
        /// <param name="duration">How long the fade should take to complete</param>
        protected async Task StartTransition(PanelStatus newStatus, float duration = .35f)
        {
            float startValue =  newStatus == PanelStatus.Hidden ? 1 : 0;
            float targetValue = newStatus == PanelStatus.Hidden ? 0 : 1;
            
            var startTime = Time.time;
            var endTime = startTime + duration;

            while (Time.time < endTime)
            {
                var normalizedTime = (Time.time - startTime) / duration;
                var currentValue = Mathf.Lerp(startValue, targetValue, normalizedTime);

                CanvasGroup.alpha = currentValue;

                // Yield control back to the caller
                await Task.Yield();
            }

            // Ensure values are strictly set after animation has completed.
            var isVisible = newStatus == PanelStatus.Visible;
            CanvasGroup.alpha = isVisible ? 1 : 0;
            CanvasGroup.interactable = isVisible;
            CanvasGroup.blocksRaycasts = isVisible;
        }
    }
}