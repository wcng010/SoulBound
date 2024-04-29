using Script.GameLogic.Data;
using UnityEngine;

namespace C_Script.BaseClass
{
    public abstract class PhysicObject<T> :Entity where T :PhysicObject<T>
    {
        public Transform Transform { get; protected set; }
        public Rigidbody2D Rigidbody2D { get;protected set; }
        public Animator Animator { get; protected set; }
        public SpriteRenderer SpriteRenderer { get; protected set; }
        public Collider2D Collider2D { get; protected set; }
        public StateMachine<T> StateMachine{ get; protected set; }
        
        public MyData Data{ get; protected set; }
        //初始化起始状态
        protected abstract void InitOriginState();
        //状态字典
        protected abstract void InitStateDictionary();
        //选择状态
        protected abstract void SwitchState();
        //初始化数据内容
        protected abstract void InitDataSetting();

        #region Event

        public abstract void HurtEvent();

        public abstract void DeathEvent();

        #endregion
        //物理步
        public virtual void PhysicBehaviour()
        {
            StateMachine.StatePhysicBehaviour();
        }
        //逻辑步
        public virtual void LogicBehaviour()
        {
            StateMachine.StateLogicBehaviour();
        }

        #region InitFunction

        protected void InitTransform(Transform transform) => Transform = transform;
        protected void InitAnimator(Animator animator)=>Animator = animator;
        protected void InitRigidbody2D(Rigidbody2D rigidbody2D) => Rigidbody2D = rigidbody2D;
        protected void InitCollider2D(Collider2D collider2D) => Collider2D = collider2D;
        
        protected void InitSpriteRenderer(SpriteRenderer spriteRenderer) => SpriteRenderer = spriteRenderer;
        protected void InitData(MyData data) => Data = data;

        #endregion
    }
}
