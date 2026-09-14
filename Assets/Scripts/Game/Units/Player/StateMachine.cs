using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Scripts.Player
{
    public enum EState
    {
        None,
        Idle,
        Move,
        BallMode,
        Fly,
        Attack,
        GetDamage,
        Die,
        Interactive,
    }
    
    public class StateMachine
    {
        private Dictionary<EState, bool> _states = new()
        {
            {EState.Idle, false},
            {EState.Move, false},
            {EState.BallMode, false},
            {EState.Fly, false},
            {EState.Attack, false},
            {EState.GetDamage, false},
            {EState.Die, false},
            {EState.Interactive, false},
        };

        public Dictionary<EState, bool> States => _states;

        public ReactiveProperty<EState> CurrentState { get; private set; } = new();
        
        public void SetState(EState state)
        {
            _states[CurrentState.Value] = false;
            CurrentState.Value = state;
        }
    }
}