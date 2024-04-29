using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class ConveyorBeltEntity : ObjectEntity
{
    private int _leave;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(KeepMove(col.transform));
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _leave = 1;
        }
    }

    IEnumerator KeepMove(Transform obj)
    {
        var rb = obj.GetComponent<Rigidbody2D>();
        var gs = rb.gravityScale;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        _leave = 0;
        while (true)
        {
            
            obj.position = Vector2.MoveTowards(obj.position, new Vector3(obj.position.x,transform.position.y+1.5f), 0.01f);
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space)||_leave == 1)
            {
                rb.gravityScale = gs;
                yield break;
            }
        }
    }
}
