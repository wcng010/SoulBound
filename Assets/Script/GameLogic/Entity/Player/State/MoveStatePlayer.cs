
using UnityEngine;

public class MoveStatePlayer:PlayerState
{
    
        public override void Enter()
        {
            base.Enter();
            TransformOwner.eulerAngles = Vector3.zero;
        }

        public override void PhysicExcute()
        {
            MoveBehaviour(PlayerData.MaxSpeedX*3/4,PlayerData.AccelerationX*3/4);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        public override void LogicExcute()
        {
            base.LogicExcute();
            SwitchState();
        }

        public override void Exit() {
            base.Exit();
        }
        private void SwitchState()
        {
            if (!Owner.IsGroundThreeRays)
            {
                StateMachine.ChangeState(StateDictionary[StateType.OnAirStatePlayer]);
            }
            //Return OnGroundState
            else if (XAxis == 0)
                StateMachine.ChangeState(StateDictionary[StateType.OnGroundStatePlayer]);
            //Jump
            else if (SpaceKey)
            {
                StateMachine.ChangeState(StateDictionary[StateType.JumpStatePlayer]);
            }
        }

        public MoveStatePlayer(PhysicEntity owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {
            
        }
}
