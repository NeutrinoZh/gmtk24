using System;

namespace GMTK.Services
{
    public class GameStatistics : IService
    {
        public event Action<int, int, int> OnStatisticsChange = null;

        private int _livingCells = 0;
        private int _sickCells = 0;
        private int _cellsDied = 0;


        public int SickCells() => _sickCells;

        public void SetLivingCells(int livingCells)
        {
            _livingCells = livingCells;
            OnStatisticsChange?.Invoke(_livingCells, _sickCells, _cellsDied);
        }

        public void CellSick()
        {
            _sickCells += 1;
            OnStatisticsChange?.Invoke(_livingCells, _sickCells, _cellsDied);
        }

        public void CellDied()
        {
            _livingCells -= 1;
            _sickCells -= 1;
            _cellsDied += 1;
            OnStatisticsChange?.Invoke(_livingCells, _sickCells, _cellsDied);
        }

        public void CellRecovered()
        {
            _sickCells -= 1;
            OnStatisticsChange?.Invoke(_livingCells, _sickCells, _cellsDied);
        }
    }
}