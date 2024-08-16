using GMTK.Services;
using System;
using System.Collections.Generic;

namespace GMTK.GameStates
{
    public class GameStateManager
    {
        private Dictionary<Type, IState> _states = new();
        private IState _currentState = null;

        private void AddState<T>(IState state) where T : IState
        {
            _states.Add(typeof(T), state);
        }

        public GameStateManager()
        {
            AddState<BootstrapState>(new BootstrapState(this));
            AddState<LoadSceneState>(new LoadSceneState(this));
            AddState<GamePlayState>(new GamePlayState(this));
        }

        public void Enter<T>() where T : IState
        {
            _currentState?.Exit();
            if (_states.TryGetValue(typeof(T), out IState state))
            {
                _currentState = state;
                _currentState.Enter();
            }
            else
                throw new ArgumentException($"Invalid GameStateManager state: '{typeof(T)}'");
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}