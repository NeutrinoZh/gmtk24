using UnityEngine;

namespace GMTK.Services
{
    public class PlayerStats : IService
    {
        public float Speed { get; set; } = 1f;
        public Transform Player { get; set; }
    }
}