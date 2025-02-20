using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    public static T Instance 
    { 
        get 
        { 
            if(instance == null)
            {
                instance = FindAnyObjectByType<T>();
                
                if(instance == null)
                {
                    var manager = new GameObject(typeof(T).Name);
                    instance = manager.AddComponent<T>();
                }
            }

            return instance; 
        } 
    }

    protected virtual void Awake()
    {
        if (instance == null) 
        {
            instance = this as T;
            DontDestroyOnLoad(this);
        }
        else if(instance != this)
        {
            Destroy(instance);
        }
    }
}
