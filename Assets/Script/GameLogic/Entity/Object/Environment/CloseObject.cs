using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseObject : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnClose()
    {
        _animator.SetTrigger("Close");
        if(transform.childCount!=0) 
            transform.GetChild(0).GetComponent<CloseObject>().OnClose();
    }

    public void OnFalse() => gameObject.SetActive(false);
}
