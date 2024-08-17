using GMTK.Services;

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

        private WorldState _worldState = WorldState.MACRO_WORLD;
        private bool _isTransition;
        private VirusManager _virusManager;

        public WorldState State
        {
            get => _worldState;
            set
            {
                _virusManager ??= ServiceLocator.Instance.Get<VirusManager>();

                if (value == _worldState)
                    return;

                _isTransition = true;

                if (value == WorldState.MICRO_WORLD)
                    TransitionIntoMicroWorld();

                if (value == WorldState.MACRO_WORLD)
                    EnterIntoMacroWorld();

                _worldState = value;
            }
        }

        private void TransitionIntoMicroWorld()
        {
            new TransitionIntoMicroWorld()
                .Start(this);
        }

        private void EnterIntoMacroWorld()
        {
            foreach (var virus in _virusManager.Pool)
                virus.gameObject.SetActive(true);
        }

        public void EnterIntoMicroWorld()
        {
            foreach (var virus in _virusManager.Pool)
                virus.gameObject.SetActive(false);
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
