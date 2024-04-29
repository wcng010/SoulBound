using System;
using DG.Tweening;
using Script.GameLogic.Entity.Object.SpecialObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.GameLogic.Entity.Object.Clock
{
    public class ClockObjectEntity:SpecialObjectEntity
    {
        //当前所在Layer0-2
        [SerializeField]private int  currentLayer;
        //对应Layer所对应状态
        [SerializeField]private int[] layerTime;
        
        private Transform _pointer;

        protected override void Awake()
        {
            base.Awake();
            layerTime = new int[3]{2,2,2};
            _pointer = transform.GetChild(0).transform;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            UIManager.Instance.Tip.SetActive(false);
            UIManager.Instance.Tip2.SetActive(true);
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            UIManager.Instance.Tip2.SetActive(false);
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Q))//时钟改变区域
            {
                AudioSystem.Instance.ClockAudioPlay();
                PlayerManager.Instance.ClockChange.Invoke(currentLayer,layerTime[currentLayer]++);
                if (layerTime[currentLayer] >= 4)
                {
                    layerTime[currentLayer] = 1;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.CompareTag("Area1"))
            {
                currentLayer = 0;
                if(layerTime[currentLayer]>=2)
                    _pointer.DORotate(new Vector3(0, 0, 270 - 90 * (layerTime[currentLayer]-1)), 1f);
                else
                {
                    _pointer.DORotate(new Vector3(0, 0, 0), 1f);
                }
            }
            if (col.CompareTag("Area2"))
            {
                currentLayer = 1;
                if(layerTime[currentLayer]>=2)
                    _pointer.DORotate(new Vector3(0, 0, 270 - 90 * (layerTime[currentLayer]-1)), 1f);
                else
                {
                    _pointer.DORotate(new Vector3(0, 0, 0), 1f);
                }
            }
            if (col.CompareTag("Area3"))
            {
                currentLayer = 2;
                if(layerTime[currentLayer]>=2)
                    _pointer.DORotate(new Vector3(0, 0, 270 - 90 * (layerTime[currentLayer]-1)), 1f);
                else
                {
                    _pointer.DORotate(new Vector3(0, 0, 0), 1f);
                }
            }
        }
    }
}