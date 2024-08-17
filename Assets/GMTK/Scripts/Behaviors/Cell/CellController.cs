using UnityEngine;

namespace GMTK
{
    public class CellController : MonoBehaviour
    {
        private CellStats _cellStats;

        private void Start() 
        {
            _cellStats = GetComponent<CellStats>();
            _cellStats.Init();
        }
    }
}