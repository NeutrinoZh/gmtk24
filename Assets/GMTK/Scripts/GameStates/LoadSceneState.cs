using GMTK.Services;

namespace GMTK.GameStates
{
    public class LoadSceneState : IState
    {
        private readonly GameStateManager _stateManager;
        private ResourceManager _resourceManager;

        public LoadSceneState(GameStateManager stateManager)
        {
            _stateManager = stateManager;
        }

        void IState.Enter()
        {
            _resourceManager = ServiceLocator.Instance.Get<ResourceManager>();
            _stateManager.Enter<GamePlayState>();
        }

        void IState.Update()
        {

        }

        void IState.Exit()
        {

        }
    }
}
