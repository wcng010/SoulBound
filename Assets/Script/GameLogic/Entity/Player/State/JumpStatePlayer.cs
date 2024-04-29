using System.Collections;
using UnityEngine;

public class JumpStatePlayer:PlayerState
{

        private float _speedUpTime;
        private float _jumpOriginalY;
        private float _yLerp;
        private bool _isSpeedUp = true;
        private RaycastHit2D _headRaycastHit2D;
        private readonly string _jumpAsh;
        private Collider2D _lastCollider;
        private Collider2D _collider;

        public override void Enter() 
        {   base.Enter();
            _isSpeedUp = true;
            Owner.StartCoroutine(JumpChange());
            if(Owner.CompareTag("Cube")) 
                AudioSystem.Instance.BoxJumpAudPlay();
        }

        public override void PhysicExcute()
        {
            if (_isSpeedUp)
            {
                MoveBehaviour(PlayerData.MaxSpeedX,PlayerData.AccelerationX);
                JumpBehaviour(PlayerData.MaxSpeedY,PlayerData.AccelerationY);
            }
            else
            {
                MoveBehaviour(PlayerData.MaxSpeedX,PlayerData.AccelerationX);
            }

            if (Rigidbody2DOwner.velocity.y < 0)
            {
                StateMachine.ChangeState(Owner.StateDic[StateType.OnAirStatePlayer]);
            }

        }
        public override void LogicExcute()
        {
            base.LogicExcute();
            SwitchState();
            _collider = Owner.CeilingOneRay.collider;
            /*if (_collider!=null&&_lastCollider == null)
            {
                Collider2DOwner.isTrigger = true;
                _lastCollider = _collider;
            }
            else if (_collider != null && _collider != _lastCollider)
            {
                Collider2DOwner.isTrigger = true;
                _lastCollider = _collider;
            }
            else if (_collider == null&&_lastCollider!=null)
            {

            }*/
            if (_collider!=null)
            {
                Collider2DOwner.isTrigger = true;
            }
            else
            {
                Collider2DOwner.isTrigger = false;
            }
        }

        public override void Exit()
        {
            base.Exit();
            Collider2DOwner.isTrigger = false;
        }

        
        protected void JumpBehaviour(float maxSpeedY,float accelerationY)
        {
            float velocityY = Mathf.Clamp(Rigidbody2DOwner.velocity.y + accelerationY * Time.deltaTime,-maxSpeedY,maxSpeedY);
            Rigidbody2DOwner.velocity = new Vector2(Rigidbody2DOwner.velocity.x, velocityY);
        }
        IEnumerator JumpChange()
        {
            yield return new WaitForSeconds(PlayerData.YSpeedUpTime);
            _isSpeedUp = false;
        }
        
       private void SwitchState()
       {

       }
       
       public JumpStatePlayer(PhysicEntity owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
       {

       }
}
