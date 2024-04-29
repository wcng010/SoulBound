namespace Script.GameLogic.Entity.Object.SpecialObject
{
    public class SpecialObjectEntity:PhysicEntity
    {
        protected override void InitStateDictionary()
        {
            StateMachine = new StateMachine<PhysicEntity>(this);
            StateDic.Add(StateType.MoveStatePlayer,new MoveStatePlayer(this,"Move","PlayerMove"));
            StateDic.Add(StateType.JumpStatePlayer,new JumpStatePlayer(this,"Jump","PlayerJump"));
            StateDic.Add(StateType.DeathStatePlayer,new DeathStatePlayer(this,null,null));
            StateDic.Add(StateType.OnAirStatePlayer,new OnAirStatePlayer(this,null,null));
            StateDic.Add(StateType.OnGroundStatePlayer, new OnGroundStatePlayer(this,null,null));
            StateDic.Add(StateType.GlobalStatePlayer, new GlobalStatePlayer(this,null,null));
        }
    }
}