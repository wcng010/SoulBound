using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BeltSystem : MonoBehaviour
{   [SerializeField] private Transform entity;
    [SerializeField] private Transform originPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float roundTime;
    [SerializeField] private float standTime;
    
    private void Start()
    {
        entity.transform.DOMove(originPos.position,roundTime/2);
        StartCoroutine(nameof(ConveyorBeltMove));
    }

    IEnumerator ConveyorBeltMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(roundTime/2 + standTime);
            entity.transform.DOMove(endPos.position, roundTime / 2);
            yield return new WaitForSeconds(roundTime/2 + standTime);
            entity.transform.DOMove(originPos.position, roundTime / 2);
        }
    }
}
