using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEntity : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]private GameObject passTrigger;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Key"))
        {
            _animator.SetTrigger("Disappear");
        }
    }
    public void AudioPlay() => AudioSystem.Instance.OpenDoorAudioPlay();
    public void Pass()
    {
        passTrigger.SetActive(true);
        gameObject.SetActive(false);
    }
}
