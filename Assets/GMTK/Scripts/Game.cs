using GMTK.GameStates;
using UnityEngine;

namespace GMTK
{
    public class Game : MonoBehaviour
    {
        private GameStateManager _gameStateManager;

        private void Awake()
        {
            _gameStateManager = new GameStateManager();
            _gameStateManager.Enter<BootstrapState>();
        }

        private void Update()
        {
            _gameStateManager.Update();
        }
    }
}