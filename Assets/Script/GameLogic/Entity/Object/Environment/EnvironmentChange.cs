using System;
using System.Collections;
using System.Collections.Generic;
using Script.GameLogic.Entity.Object.Clock;
using UnityEngine;

public class EnvironmentChange : MonoBehaviour,IChangeWithClock
{
    [SerializeField]private List<CloseObject> objList  = new List<CloseObject>();

    // Start is called before the first frame update
    private void OnEnable()
    {
        PlayerManager.Instance.ClockChange += OnChangeWithTime;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.ClockChange -= OnChangeWithTime;
    }

    public void OnChangeWithTime(int layer, int time)
    {
        if (layer == 2 && time == 1)
        {
            AudioSystem.Instance.WindowAudioPlay();
            objList[0].gameObject.SetActive(true);
            if(objList[0].transform.childCount!=0)
                objList[0].transform.GetChild(0).gameObject.SetActive(true);   
            if(objList[1].gameObject.activeSelf)
                objList[1]?.OnClose();
            if(objList[2].gameObject.activeSelf)
                objList[2]?.OnClose();
        }
        else if (layer == 2 && time == 2)
        {
            AudioSystem.Instance.WindowAudioPlay();
            objList[1].gameObject.SetActive(true);
            if(objList[1].transform.childCount!=0)
                objList[1].transform.GetChild(0).gameObject.SetActive(true);   
            if(objList[0].gameObject.activeSelf)
                objList[0].OnClose();
            if(objList[2].gameObject.activeSelf)
                objList[2].OnClose();
        }
        else if (layer == 2&&time==3)
        {
            objList[2].gameObject.SetActive(true);
            if(objList[2].transform.childCount!=0)
                objList[2].transform.GetChild(0).gameObject.SetActive(true);   
            if(objList[1].gameObject.activeSelf)
                objList[1]?.OnClose();
            if(objList[0].gameObject.activeSelf)
                objList[0]?.OnClose();
        }
    }
}
