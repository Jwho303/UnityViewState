using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jwho303.ViewState.Example
{
    public class ConfirmationPopup : View, IView<PopupView>
    {
        public PopupView ViewType => PopupView.Confirmation;

        [SerializeField]
        private UIController _uiController; //HACK for demo purposes

        [SerializeField]
        private Button _retryButton;

        [SerializeField]
        private Button _proceedButton;

        public override void Init()
        {
            base.Init();

            _retryButton.onClick.AddListener(() => { _uiController.ShowPopup(PopupView.LoadingBar, ViewTransition.Bounce, false); });
            _proceedButton.onClick.AddListener(() => { _uiController.HidePopup(); });
        }

        protected override void UpdateView()
        {

        }
    }
}