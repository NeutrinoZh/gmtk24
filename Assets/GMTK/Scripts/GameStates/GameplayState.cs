using GMTK.Services;
using System;

namespace GMTK.GameStates
{
    public enum WorldState
    {
        MICRO_WORLD,
        MACRO_WORLD
    };

    public class GamePlayState : IState, IService
    {
        public GamePlayState(GameStateManager stateManager) { }
        public Action<WorldState> OnWorldChanged;

        private WorldState _worldState = WorldState.MACRO_WORLD;
        private VirusManager _virusManager;

        public WorldState State
        {
            get => _worldState;
            set
            {
                _virusManager ??= ServiceLocator.Instance.Get<VirusManager>();

                if (value == _worldState)
                    return;

                if (value == WorldState.MICRO_WORLD)
                    TransitionIntoMicroWorld();

                if (value == WorldState.MACRO_WORLD)
                    TransitionIntoMacroWorld();

                _worldState = value;
                OnWorldChanged?.Invoke(value);
            }
        }

        private void TransitionIntoMicroWorld()
        {
            new TransitionIntoMicroWorld()
                .Start(this);

            foreach (var virus in _virusManager.Pool)
                virus.gameObject.SetActive(false);
        }

        private void TransitionIntoMacroWorld()
        {
            new TransitionIntoMacroWorld()
                .Start(this);

            foreach (var virus in _virusManager.Pool)
                virus.gameObject.SetActive(true);
        }

        void IState.Enter()
        {
            ServiceLocator.Instance.Register(this);
        }

        void IState.Update()
        {

        }

        void IState.Exit()
        {

        }
    }
}
