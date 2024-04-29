using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Script.GameLogic.Entity.Object.Clock;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;

//管理对应角色的启用
public delegate void ChangeIndex(int index);

public delegate void ClockChange(int layer, int time);
public class PlayerManager : NormSingleton<PlayerManager>
{
    [SerializeField]private List<PhysicEntity> players;
    [SerializeField]private CinemachineVirtualCamera cm;
    [SerializeField]private Sprite selectedSprite;
    [SerializeField]private Sprite unselectedSprite;
    [SerializeField]private List<SpriteRenderer> selectedUIList;
    //激活的Player序号
    [SerializeField][Range(0,10)]private int activePlayerIndex;
    [SerializeField]private int[] readyIndex = new int[4]{0,1,2,3};
    public ChangeIndex ChangeIndex;
    public ClockChange ClockChange;
    public bool CanChangePlayer = true;
    protected override void Awake()
    {
        base.Awake();
        //cm = GameObject.FindWithTag("VCM").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        ChangeIndex += OnChangeIndex;
    }

    private void Start()
    {
        FindPlayer();
        players[activePlayerIndex].enabled=true;
    }

    private void OnChangeIndex(int index)
    {
        readyIndex[activePlayerIndex] = index;
    }

    private void FindPlayer()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            PhysicEntity player = transform.GetChild(i).gameObject.GetComponent<PhysicEntity>();
            players.Add(player);
            player.enabled = false;
        }
    }

    //传入的是readyIndex中的序号
    private void Update()
    {
        if (CanChangePlayer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && players.Count >= 1)
            {
                OnChangePlayer(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && players.Count >= 2)
            {
                OnChangePlayer(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && players.Count >= 3)
            {
                OnChangePlayer(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && players.Count >= 4)
            {
                OnChangePlayer(3);
            }
        }
    }
    
    private int GetIndex(int[] myList,int value) 
    {
        for (int i = 0; i < myList.Length; i++)
        {
            if (myList[i] == value) return i;
        }
        return -1;
    }
    //playerIndex为在readyIndex中序号，activePlayerIndex为当前选择角色序号0-3
    public void OnChangePlayer(int playerIndex)
    {
        //获得激活物体在准备队列的编号0-4

        //将当前激活的物体对应UI设为未选择

        selectedUIList[activePlayerIndex].sprite = unselectedSprite;
        selectedUIList[playerIndex].sprite = selectedSprite;
        //特殊物体的处理
        if (readyIndex[activePlayerIndex] >= 4)
        {
            players[readyIndex[activePlayerIndex]].GetComponent<ObjectEntity>().OnBeGround();
        }
        if (readyIndex[playerIndex] >= 4)
        {
            players[readyIndex[playerIndex]].GetComponent<ObjectEntity>().OnBeObject();
        }

        players[readyIndex[activePlayerIndex]].Rigidbody2D.constraints = 
            RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        players[readyIndex[activePlayerIndex]].enabled = false;
        players[readyIndex[playerIndex]].enabled = true;
        activePlayerIndex = playerIndex;
        //cm.Follow = players[playerIndex].transform;
        //cm.LookAt = players[playerIndex].transform;
    }
     //仅用于附着功能
    //Player转换成物体，将Player失活，将传进来的组件打开，并设置未激活组件
    public void OnDeleteCurrentEntity(int playerIndex)
    {
        //players[activePlayerIndex].gameObject.SetActive(false);
        players[playerIndex].enabled = true;
        //cm.Follow = players[playerIndex].transform;
        //cm.LookAt = players[playerIndex].transform;
    }

    public void OnClockChange(int layer,int time)
    {
        foreach (var player in players)
        {
            player.OnChangeWithTime(layer,time);
        }
    }
}
