using System;
using Script.GameLogic.Data;
using Script.GameLogic.Entity.Object.SpecialObject;
using UnityEngine;
using UnityEngine.Serialization;

public class WorkObjectEntity : ObjectEntity
{
    [SerializeField] private float reduceRate;
    [SerializeField]private float localScaleY;
    [SerializeField]private float checkDis=0.5f;
    [SerializeField]private PlayerData playerData;
    [SerializeField]private bool canAbSorb;
    private bool isAbSorb;
    public Collider2D OverlapCheckPlayer
    {
        get => Physics2D.OverlapCircle(transform.position, checkDis,
            1 << LayerMask.NameToLayer("Player"));
    }
    
    
    protected override void Awake()
    {
        base.Awake();
        localScaleY = transform.localScale.y;
    }

    private void OnEnable()
    {
        PlayerManager.Instance.ClockChange += OnChangeWithTime;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.ClockChange -= OnChangeWithTime;
    }

    public void Update()
    {
        Collider2D collider2D = OverlapCheckPlayer;
        if (collider2D != null)
        {
            //如果不能吸收，弹出作业太多对话框
            if(!canAbSorb)
                UIManager.Instance.Dialogue2.SetActive(true);
            else
                UIManager.Instance.Dialogue2.SetActive(false);
            //如果可以吸收
            if (Input.GetKeyDown(KeyCode.E)&&canAbSorb)
            {
                UIManager.Instance.Dialogue2.SetActive(false);
                isAbSorb = true;
            }
        }
        else 
        {
            UIManager.Instance.Dialogue2.SetActive(false);
        }
    }
    
    
    
    public override void OnChangeWithTime(int layer, int time)
    {
        if (!isAbSorb)
        {
            base.OnChangeWithTime(layer, time);
            if (layer == 0)
            {
                canAbSorb = false;
                gameObject.layer = LayerMask.NameToLayer("Ground");
            }
            if (layer == 0 && time != 1)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / reduceRate,
                    transform.localScale.z);
                
                if (transform.localScale.y == 0.25f)
                {
                    canAbSorb = true;
                    gameObject.layer = LayerMask.NameToLayer("Object");
                }
            }
            else if (layer == 0 && time == 1)
            {
                transform.position += new Vector3(0, 1, 0);
                transform.localScale = new Vector3(transform.localScale.x, localScaleY, transform.localScale.z);
            }
            
            playerData.Size = transform.localScale;
        }
        else
        {
            base.OnChangeWithTime(layer, time);
            if (layer == 0 && time != 1)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / reduceRate,
                    transform.localScale.z);
            }
            else if (layer == 0 && time == 1)
            {
                transform.localScale = new Vector3(transform.localScale.x, localScaleY, transform.localScale.z);
            }
            playerData.Size = transform.localScale;
        }
    }
}
