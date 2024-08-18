using UnityEngine;

namespace GMTK.MicroViruses
{
    public class InCellVirusManager : ObjectManager { };

    public class InCellViruses : MonoBehaviour
    {
        private Bounds _bounds;

        private void Awake()
        {
            gameObject.AddComponent<InCellVirusManager>();
        }


        public void SetSpawnArea(Bounds spawnArea)
        {
            _bounds = spawnArea;
        }

        public void SetVirusesCount(int _count)
        {

        }
    }
}