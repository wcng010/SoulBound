using System;
using UnityEngine;

public class PortalEntity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        { 
            if(col.transform.localScale.x>0) 
                FloorSystem.Instance.OnChangeColdEnvironment();
            else
                FloorSystem.Instance.OnChangeHeatEnvironment();
        }
    }
}
