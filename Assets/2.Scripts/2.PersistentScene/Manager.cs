using UnityEngine;

public class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {            
            if (instance == null)
            {
                // FindObjectOfType 를 사용하지 않고 해당 오브젝트 중 첫번째 오브젝트를 찾도록 변경
                instance = FindFirstObjectByType<T>();
                                
                if (instance == null)
                {
                    Debug.Log($"해당 싱글톤({typeof(T).Name})을 찾지 못하여 새롭게 생성");

                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                    //DontDestroyOnLoad(go);
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
            //DontDestroyOnLoad(this);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
