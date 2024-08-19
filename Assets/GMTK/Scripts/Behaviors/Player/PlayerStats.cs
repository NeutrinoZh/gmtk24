using UnityEngine;

namespace GMTK.Services
{
    public class PlayerStats : IService
    {
        public float SpeedScale { get; set; } = 1f;
        public int Time { get; set; } = 0;
        public Transform Player { get; set; }
    }
}