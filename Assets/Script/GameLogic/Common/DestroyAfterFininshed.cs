using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterFininshed : MonoBehaviour
{
    [SerializeField] private float time;

    private void OnEnable()
    {
        OnMyDestroy();
    }

    public void OnMyDestroy() => Invoke("MyDestroy", time);
    
    private void MyDestroy() => Destroy(gameObject);
}
