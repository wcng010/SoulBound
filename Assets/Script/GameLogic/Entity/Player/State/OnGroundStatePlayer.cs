
using UnityEngine;

public class OnGroundStatePlayer:PlayerState
{ 
        public override void Enter()
        {
                TransformOwner.eulerAngles = Vector3.zero;
                Rigidbody2DOwner.velocity = Vector2.zero; 
                Rigidbody2DOwner.constraints =
                        RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        public override void PhysicExcute()
        {
                base.PhysicExcute();
        }

        public override void LogicExcute()
        {
                SwitchState();
        }

        public override void Exit()
        { 
                Rigidbody2DOwner.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        private void SwitchState()
        {
                if (!Owner.IsGroundOneRay)
                {
                        StateMachine.ChangeState(StateDictionary[StateType.OnAirStatePlayer]);
                }
                //Jump
                if (SpaceKey)
                {
                        StateMachine.ChangeState(StateDictionary[StateType.JumpStatePlayer]);
                }
                //Move
                else if (XAxis != 0)
                {
                        
                        StateMachine.ChangeState(StateDictionary[StateType.MoveStatePlayer]);
                }
        }

        public OnGroundStatePlayer(PhysicEntity owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
                
        }
}
