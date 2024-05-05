using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

namespace View
{
    public interface IView<TEnum> : IState where TEnum : struct
    {
        TEnum ViewType { get; }

        event Action OnTransitionOnCompleted;
        event Action OnTransitionOffCompleted;

        bool IsShowing { get; }
        bool IsTransitioning { get; }

        void Init();
        void SetAnimationClips(AnimationClip onClip, AnimationClip offClip);
    }
}