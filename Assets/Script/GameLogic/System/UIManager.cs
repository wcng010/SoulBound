using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : NormSingleton<UIManager>
{
    public GameObject Tip;
    public GameObject Tip2;
    public GameObject Dialogue1;
    public GameObject Dialogue2;
    public SpriteRenderer backGround;
    public Sprite changeGround;

    public void OnChangeGround() => backGround.sprite = changeGround;
}
