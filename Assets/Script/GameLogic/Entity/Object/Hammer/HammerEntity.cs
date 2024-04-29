using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HammerEntity : ObjectEntity
{
    [SerializeField]private float checkRange;
    private void OnEnable()
    {
        StartCoroutine(CheckTarget());
    }

    private IEnumerator CheckTarget()
    {
        while(true)
        {
            Collider2D collider2D;
            if ((collider2D=OverlapCheckObject("Stool")) != null)
            {
                UIManager.Instance.Dialogue1.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //TODO:敲碎桌子逻辑
                    TimelineManager.Instance.TimeLine2.Play();
                    UIManager.Instance.Dialogue1.SetActive(false);
                    collider2D.gameObject.layer = LayerMask.NameToLayer("Default");
                    yield return new WaitForSeconds(2f);
                    AudioSystem.Instance.StoolBreakAudioPlay();
                    UIManager.Instance.OnChangeGround();
                    collider2D.gameObject.GetComponent<ChairEntity>().OnChange();
                    //collider2D.gameObject.SetActive(false);
                }
            }
            else
            {
                UIManager.Instance.Dialogue1.SetActive(false);
            }

            if ((collider2D=OverlapCheckObject("Ground")) != null&&collider2D.CompareTag("Fan"))
            {
                UIManager.Instance.Dialogue2.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //TODO:修好风扇逻辑
                    TimelineManager.Instance.TimeLine2.Play();
                    UIManager.Instance.Dialogue2.SetActive(false);
                    yield return new WaitForSeconds(2f);
                    AudioSystem.Instance.HammerAudioPlay();
                    collider2D.GetComponent<FanEntity>().OnWindZoneOpen();
                }
            }
            else
            {
                UIManager.Instance.Dialogue2.SetActive(false);
            }

            yield return null;
        }
    }
    public Collider2D OverlapCheckObject(string layerName)
    {
        return Physics2D.OverlapCircle(transform.position, checkRange,
            1 << LayerMask.NameToLayer(layerName));
    }
}
