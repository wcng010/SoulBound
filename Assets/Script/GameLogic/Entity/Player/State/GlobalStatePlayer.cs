using UnityEngine;

public class GlobalStatePlayer:PlayerState
{
    public override void Enter()
    {
        
    }
    
    public override void PhysicExcute()
    {
        
    }

    public override void LogicExcute()
    {
        if (TransformOwner.gameObject.CompareTag("Player")&&StateMachine.CurrentState!=StateDictionary[StateType.AbsorbStatePlayer])
        {
            Collider2D collider2D = Owner.IsObjectOnRange;
            if (collider2D != null && collider2D.transform != TransformOwner)
            {
                UIManager.Instance?.Tip.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    AnimatorOwner.SetBool("Absorb", true);
                    float value = collider2D.transform.position.x - TransformOwner.position.x;
                    if (value < 0)
                    {
                        var localScale = TransformOwner.localScale;
                        localScale = new Vector3(-Mathf.Abs(localScale.x), localScale.y,
                            localScale.z);
                        TransformOwner.localScale = localScale;
                    }
                    else
                    {
                        var localScale = TransformOwner.localScale;
                        localScale = new Vector3(Mathf.Abs(localScale.x), localScale.y,
                            localScale.z);
                        TransformOwner.localScale = localScale;
                    }

                    StateMachine.ChangeState(StateDictionary[StateType.AbsorbStatePlayer]);
                    GameObject obj = GameObject.Instantiate(PlayerData.EffectPrefab, TransformOwner);
                    obj.transform.localScale = new Vector3(-2, 2, 1);
                    collider2D.GetComponent<ObjectEntity>().OnChange(TransformOwner);
                }
            }
            else
            {
                UIManager.Instance?.Tip.SetActive(false);
            }
        }
    }
    
    public override void Exit()
    {
        
    }

    public GlobalStatePlayer(PhysicEntity owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
    {
        
    }
}
