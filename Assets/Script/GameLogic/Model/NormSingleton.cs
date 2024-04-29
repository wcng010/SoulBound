using UnityEngine;

public class NormSingleton<T> :MonoBehaviour where T :NormSingleton<T>
{ 
    [SerializeField] private bool isSaveNextScene; 
    private static T _instance;
     public static T Instance
     {
         get 
         {
             return _instance;
         }
      }

     protected virtual void Awake()
     {
         if (_instance == null)
         {
             _instance = this as T;
         }
         else
         {
             Destroy(gameObject);
         }
         if (isSaveNextScene)
             DontDestroyOnLoad(gameObject);
     }
}
