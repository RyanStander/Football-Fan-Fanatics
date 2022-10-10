using UnityEngine;

namespace NotFound.Utils
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {

        public static T Instance => _instance;
        private static T _instance;

        protected virtual void Awake()
        {
            if (_instance != null && _instance == this)
            {
                Debug.LogError($"There already is a MonoSingleton<{typeof(T)}> in the scene");
                return;
            }

            _instance = (T)this;
        }

    }
}