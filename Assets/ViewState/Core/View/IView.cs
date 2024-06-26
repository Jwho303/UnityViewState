using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jwho303.ViewState
{
    public interface IView<TEnum> : IState where TEnum : struct
    {
        TEnum ViewType { get; }

        Action OnTransitionOnCompleted { get; set; }
        Action OnTransitionOffCompleted { get; set; }

        bool IsShowing { get; }
        bool IsTransitioning { get; }

        void Init();
        void SetAnimationClips(AnimationClip onClip, AnimationClip offClip);
    }
}