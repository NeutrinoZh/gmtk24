using UnityEngine;

namespace GMTK
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.position += Time.deltaTime * _speed * transform.right;
        }
    }
}