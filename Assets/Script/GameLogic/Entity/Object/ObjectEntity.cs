using System;
using System.Collections;
using DG.Tweening;
using Script.GameLogic.Data;
using Script.GameLogic.Entity.Object.Clock;
using UnityEngine;

public class ObjectEntity:Entity,IChangeWithClock
{
    [SerializeField] private int index;
    [SerializeField] private Sprite changeSprite;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    protected SpriteRenderer SpriteRenderer;
    private PlayerData _playerData;
    protected Animator Animator;
    private Transform _pointer;
    protected virtual void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _playerData = GetComponent<PhysicEntity>().playerData;
        index = transform.GetSiblingIndex();
        Animator = GetComponent<Animator>();
        _pointer = transform.GetChild(0).transform;
    }
    //第一次转换调用
    public virtual void OnChange(Transform target)
    {

        StartCoroutine(ChangeIEnu(target));
    }
    
    private IEnumerator ChangeIEnu(Transform target)
    {
        /*
        _boxCollider2D.isTrigger = true;
        //物体移动时间
        transform.DOMove(target.transform.position, 0.5f);
        transform.DOScale(0, 0.5f);
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
        //
        yield return new WaitForSeconds(1.2f);
        Animator.enabled = false;
        while (SpriteRenderer.sprite != changeSprite)
        {
            SpriteRenderer.sprite = changeSprite;
            yield return null;
        }
        //_animator.enabled = true;
        transform.DOScale(_playerData.Size, 0.7f);
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.OnDeleteCurrentEntity(index);
        //当前为第几个子物体
        PlayerManager.Instance.ChangeIndex(index);
        _boxCollider2D.isTrigger = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        gameObject.layer = LayerMask.NameToLayer("Player");*/
        PlayerManager.Instance.CanChangePlayer = false;
        yield return new WaitForSeconds(1f);
        _boxCollider2D.isTrigger = true;
        //物体移动时间
        transform.DOMove(target.transform.position, 1f);
        transform.DOScale(0, 1f);
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(1f);
        Animator.enabled = false;
        while (SpriteRenderer.sprite != changeSprite)
        {
            SpriteRenderer.sprite = changeSprite;
            yield return null;
        }
        //_animator.enabled = true;
        transform.DOScale(_playerData.Size, 1f);
        yield return new WaitForSeconds(1f);
        PlayerManager.Instance.OnDeleteCurrentEntity(index);
        //当前为第几个子物体
        PlayerManager.Instance.ChangeIndex(index);
        _boxCollider2D.isTrigger = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        gameObject.layer = LayerMask.NameToLayer("Player");
        PlayerManager.Instance.CanChangePlayer = true;
        UIManager.Instance?.Tip.SetActive(false);
    }



    //切换其他角色，Object变为Ground
    public void OnBeGround()
    {
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        gameObject.layer = LayerMask.NameToLayer("Ground");
    }
    //切换该角色，Ground再次变为Player
    public void OnBeObject()
    {
        _rigidbody2D.constraints = RigidbodyConstraints2D.None;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public virtual void OnChangeWithTime(int layer, int time)
    {
    }
}
