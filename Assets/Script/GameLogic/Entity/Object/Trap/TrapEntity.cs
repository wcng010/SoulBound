using System;
using UnityEngine;

public class TrapEntity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            EventCentreManager.Instance.Publish(MyEventType.ReStart);
        }
    }
}
