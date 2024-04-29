using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Human : MonoBehaviour
{
    [SerializeField]private float checkDis=0.5f;
    private Animator _animator;
    private CapsuleCollider2D _boxCollider2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<CapsuleCollider2D>();
    }

    public Collider2D OverlapCheckCandy
    {
        get => Physics2D.OverlapCircle(transform.position, checkDis,
            1 << LayerMask.NameToLayer("Player"));
    }

    public void Update()
    {
        Collider2D collider2D = OverlapCheckCandy;
        if (collider2D != null)
        {
            UIManager.Instance.Dialogue1.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)&&collider2D.CompareTag("candy"))
            {
                _animator.SetTrigger("Disappear");
                UIManager.Instance.Dialogue1.SetActive(false);
                _boxCollider2D.enabled = false;
                enabled = false;
            }
        }
        else
        {
            UIManager.Instance.Dialogue1.SetActive(false);
        }
    }
}
