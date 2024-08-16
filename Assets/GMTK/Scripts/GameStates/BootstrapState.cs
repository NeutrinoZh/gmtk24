using GMTK.Services;

namespace GMTK.GameStates
{
    public class BootstrapState : IState
    {
        private readonly GameStateManager _stateManager;

        public BootstrapState(GameStateManager stateManager)
        {
            _stateManager = stateManager;
        }

        void IState.Enter()
        {
            RegisterServices();
            _stateManager.Enter<LoadSceneState>();
        }

        void IState.Update()
        {
        }

        void IState.Exit()
        {

        }

        private void RegisterServices()
        {
            ServiceLocator.Instance.Register(new ResourceManager());
            ServiceLocator.Instance.Register(new InputController());
            ServiceLocator.Instance.Register(new PlayerStats());
        }
    }
}
