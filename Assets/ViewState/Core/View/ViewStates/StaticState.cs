using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using State;

namespace View
{
    public class StaticState : IState
    {
        public Action OnEnterAction = delegate { };
        public Action OnUpdateAction = delegate { };
        public Action OnExitAction = delegate { };

        public void EnterState()
        {
            OnEnterAction();
        }

        public void UpdateState()
        {
            OnUpdateAction();
        }

        public void ExitState()
        {
            OnExitAction();
        }

        public void Interupt()
        {

        }
    }
}