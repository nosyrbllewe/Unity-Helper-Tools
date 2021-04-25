using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<T>();
                if (!_instance)
                    _instance = new GameObject($"{typeof(T).Name} (Singleton)").AddComponent<T>();
            }

            return _instance;
        }
        private set => _instance = value;
    }

    protected void Awake()
    {
        if (!Instance)
        {
            Instance = (T)this;
        }
        else if (Instance != (T)this)
        {
            Debug.LogError($"Singleton of type '{nameof(T)}' already exists.");
        }
    }
}
