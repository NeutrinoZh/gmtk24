using UnityEngine;

namespace GMTK.Services
{
    public class ResourceManager : IService
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}