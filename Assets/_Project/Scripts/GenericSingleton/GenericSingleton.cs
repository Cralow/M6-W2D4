using UnityEngine;
public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    private static bool _isApplicationQuitting = false;


    protected virtual bool ShouldBeDestroyedOnLoad() => false;

    protected virtual void Awake()
    {
        if (instance == null)
        {

            instance = GetComponent<T>();
            if (!ShouldBeDestroyedOnLoad())
            {
                DontDestroyOnLoad(gameObject);
            }

        }
        else if (instance != this)
        {

        }
    }
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>(FindObjectsInactive.Include);
                if (instance == null && !_isApplicationQuitting)
                {
                    Debug.LogWarning($"Generationg {typeof(T)} singleton ");
                    GameObject gameObject = new GameObject(typeof(T).ToString());
                    instance = gameObject.AddComponent<T>();
                }
            }
            return instance;
        }
    }
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    protected virtual void OnApplicationQuit()
    {
        if (instance == this)
        {
            instance = null;
        }
    }


}

