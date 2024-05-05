using System.Collections.Generic;
using UnityEngine;
using State;
using System;

namespace View
{
    public class ViewController<TEnum> where TEnum : struct
    {
        public TEnum CurrentView => (CurrentState != null) ? CurrentState.ViewType : default;
        public IView<TEnum> CurrentState { get; private set; }

        private Dictionary<TEnum, IView<TEnum>> _views;
        private IView<TEnum> _exitingState;
        private IView<TEnum> _incomingState;

        public ViewController(IView<TEnum>[] views, TEnum startState = default)
        {
            InitializeViews(views);
            ChangeState(_views[startState]);
        }

        public void UpdateViewController()
        {
            _exitingState?.UpdateState();
            CurrentState?.UpdateState();
        }

        public void Show(TEnum viewToShow, bool immediate = true)
        {
            if (!_views.TryGetValue(viewToShow, out var view))
            {
                Debug.LogError($"[ViewController-{typeof(TEnum).Name}] No View! ({viewToShow})");
                return;
            }

            ChangeState(view, immediate);
        }

        public void Show(TEnum viewToShow, AnimationClip onClip, AnimationClip offClip, bool immediate = true)
        {
            if (_views.TryGetValue(viewToShow, out var view))
            {
                CurrentState?.SetAnimationClips(onClip, offClip);
                view.SetAnimationClips(onClip, offClip);
                Show(viewToShow, immediate);
            }
            else
            {
                Debug.LogError($"[ViewController-{typeof(TEnum).Name}] No View! ({viewToShow})");
            }
        }

        private void InitializeViews(IView<TEnum>[] views)
        {
            _views = new Dictionary<TEnum, IView<TEnum>>();
            foreach (var view in views)
            {
                _views.Add(view.ViewType, view);
                view.OnTransitionOnCompleted += HandleViewTransitionOnCompleted;
                view.OnTransitionOffCompleted += HandleViewTransitionOffCompleted;
                view.Init();
            }
        }

        private void ChangeState(IView<TEnum> newState, bool immediate = true)
        {
            if (CurrentState == newState)
            {
                return;
            }

            Debug.Log($"[ViewController-{typeof(TEnum).Name}] Showing ({newState})");

            if (_exitingState != null)
            {
                return;
                //InterruptOngoingTransitions();
            }

            if (CurrentState == null || immediate)
            {
                TransitionToStateImmediately(newState);
            }
            else
            {
                TransitionToStateSequentially(newState);
            }
        }

        private void InterruptOngoingTransitions()
        {
            _exitingState?.Interupt();
            _exitingState?.ExitState();
            _exitingState = null;

            CurrentState?.Interupt();
            CurrentState?.ExitState();
            CurrentState = null;

            _incomingState = null;
        }

        private void TransitionToStateImmediately(IView<TEnum> newState)
        {
            _exitingState = CurrentState;
            _exitingState?.ExitState();

            CurrentState = newState;
            CurrentState.EnterState();
        }

        private void TransitionToStateSequentially(IView<TEnum> newState)
        {
            _incomingState = newState;
            _exitingState = CurrentState;
            CurrentState = null;

            _exitingState?.ExitState();
        }

        private void HandleViewTransitionOnCompleted()
        {
            _exitingState = null;
        }

        private void HandleViewTransitionOffCompleted()
        {
            if (_incomingState != null)
            {
                CurrentState = _incomingState;
                CurrentState.EnterState();
                _incomingState = null;
            }
        }
    }
}