using UnityEngine;

namespace Script.GameLogic.Entity.Player.State
{
    public class AbsorbStatePlayer:PlayerState
    {
        public override void Enter()
        {
            Rigidbody2DOwner.velocity = Vector2.zero; 
            Rigidbody2DOwner.constraints =
                RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            UIManager.Instance?.Tip.SetActive(false);
        }

        public override void PhysicExcute()
        {

        }

        public override void LogicExcute()
        {

        }

        public override void Exit()
        { 

        }
        
        public AbsorbStatePlayer(PhysicEntity owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
    }
}