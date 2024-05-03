using System;
namespace State
{
    public interface IState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
        void Interupt();
    }
}