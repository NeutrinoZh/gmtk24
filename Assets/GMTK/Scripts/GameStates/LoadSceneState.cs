using GMTK.Services;

namespace GMTK.GameStates
{
    public class LoadSceneState : IState
    {
        private readonly GameStateManager _stateManager;

        public LoadSceneState(GameStateManager stateManager)
        {
            _stateManager = stateManager;
        }

        void IState.Enter()
        {
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
