public class OnAirStatePlayer:PlayerState
{
    
        public override void Enter()
        {
            base.Enter();
        }

        public override void PhysicExcute()
        {
            FallBahaviour();
        }

        public override void LogicExcute()
        {
            SwitchState();
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        protected void FallBahaviour()=> MoveBehaviour(PlayerData.MaxSpeedX,PlayerData.AccelerationX);
        
        private void SwitchState()
        {
            if (Owner.IsGroundOneRay)
            {
                if(Owner.CompareTag("Cube")) AudioSystem.Instance.BoxFallAudioPlay();
                
                StateMachine.RevertOrinalState();
            }
        }
        
        public OnAirStatePlayer(PhysicEntity owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        {

        }
}
