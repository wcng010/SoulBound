using UnityEngine;

public class BottleEntity : ObjectEntity
{
    [SerializeField]private int disappearTime;
    protected override void Awake()
    {
        base.Awake();
        Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerManager.Instance.ClockChange += OnChangeWithTime;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.ClockChange -= OnChangeWithTime;
    }
    
    public override void OnChangeWithTime(int layer, int time)
    {
        base.OnChangeWithTime(layer, time);
        if (layer == 1 && time == disappearTime)
        {
            Debug.Log(1);
            Animator.SetTrigger("Disappear");
        }
        else if (layer == 1&&time==1)
        {
            Animator.SetTrigger("Appear");
        }
    }
}
