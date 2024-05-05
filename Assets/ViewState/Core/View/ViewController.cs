using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using State;
using System;

namespace View
{
    public class ViewController<TEnum> where TEnum : struct
    {
        public TEnum CurrentView => CurrentState.ViewType;
        public IView<TEnum> CurrentState { get; private set; }

        private Dictionary<TEnum, IView<TEnum>> _views = new Dictionary<TEnum, IView<TEnum>>();
        private IView<TEnum> _exitingState;

        public ViewController(IView<TEnum>[] views, TEnum startState = default)
        {
            Debug.Log($"[ViewController-{typeof(TEnum).Name}] Assigning ({views.Length}) views");
            for (int i = 0; i < views.Length; i++)
            {
                _views.Add(views[i].ViewType, views[i]);
                views[i].OnTransitionOnCompleted += HandleViewTransitionOnCompleted;
                views[i].OnTransitionOffCompleted += HandleViewTransitionOffCompleted;
                views[i].Init();
            }

            ChangeState(_views[startState]);
        }

        public void UpdateViewController()
        {
            _exitingState?.UpdateState();
            CurrentState?.UpdateState();
        }

        public void Show(TEnum viewToShow)
        {
            if (!_views.ContainsKey(viewToShow))
            {
                Debug.LogError($"[ViewController] No View! ({viewToShow})");
                return;
            }

            Debug.Log($"[ViewController-{typeof(TEnum).Name}] Showing ({viewToShow})");
            ChangeState(_views[viewToShow]);
        }

        public void Show(TEnum viewToShow, AnimationClip onClip, AnimationClip offClip)
        {
            CurrentState.SetAnimationClips(onClip, offClip);
            _views[viewToShow].SetAnimationClips(onClip, offClip);

            Show(viewToShow);
        }

        private void ChangeState(IView<TEnum> newState)
        {
            if (CurrentState == newState || (CurrentState != null && CurrentState.Equals(newState)))
            {
                return;
            }

            if (CurrentState != null)
            {
                _exitingState = CurrentState;
                _exitingState.ExitState();
            }

            CurrentState = newState;
            CurrentState.EnterState();
        }

        private void HandleViewTransitionOnCompleted()
        {
            _exitingState = null;
        }

        private void HandleViewTransitionOffCompleted()
        {

        }
    }
}