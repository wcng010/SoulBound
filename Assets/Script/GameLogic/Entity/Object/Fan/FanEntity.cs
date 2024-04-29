using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanEntity : ObjectEntity
{
    [SerializeField] private GameObject WindZone;
    [SerializeField] private Sprite OpenSprite;
    public override void OnChange(Transform target)
    {
        base.OnChange(target);
        gameObject.tag = "Fan";
    }


    public void OnWindZoneOpen()
    {
        AudioSystem.Instance.FanAudioPlay();
        gameObject.tag = "Untagged";
        WindZone.SetActive(true);
        SpriteRenderer.sprite = OpenSprite;
    }
}
