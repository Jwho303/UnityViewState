using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class NoneMenu : MonoBehaviour, IView<MenuView>
    {
        public MenuView ViewType => MenuView.None;

        public bool IsShowing => throw new NotImplementedException();

        public bool IsTransitioning => false;

        public event Action OnTransitionOnCompleted;
        public event Action OnTransitionOffCompleted;

        public void DisableView()
        {

        }

        public void EnableView()
        {

        }

        public void EnterState()
        {

        }

        public void ExitState()
        {

        }

        public void Init()
        {

        }

        public void Interupt()
        {

        }

        public void SetAnimationClips(AnimationClip onClip, AnimationClip offClip)
        {

        }

        public void UpdateState()
        {

        }

        public void UpdateView()
        {

        }
    }
}