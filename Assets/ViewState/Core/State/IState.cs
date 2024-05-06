using System;
namespace Jwho303.ViewState
{
    public interface IState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
        void Interupt();
    }
}