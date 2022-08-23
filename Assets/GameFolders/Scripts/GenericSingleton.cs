using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if((Object)_instance == (Object)null)
            {
                _instance = Object.FindObjectOfType<T>();
                if((Object)_instance == (Object)null)
                {
                    Debug.LogError("An instance of " + typeof(T)?.ToString() + "is needed in scene");
                }
            }
            return _instance;
        }
    }
}
