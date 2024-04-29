using System;
using System.Collections;
using C_Script.BaseClass;
using Script.GameLogic.Data;
using UnityEngine;
using Random = UnityEngine.Random;

    public abstract class State<T> where T : PhysicObject<T>
    {
        protected readonly T Owner;
        
        #region AnimationValue

        protected float AnimationTime;

        protected bool IsAnimationName;

        protected bool IsAniamtionFinshed;

        protected string AnimationName;

        protected string NameToTrigger;
        
        #endregion

        #region Component
        private readonly Animator _animator;
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly Collider2D _collider2D;
        private readonly StateMachine<T> _stateMachine;
        private readonly MyData _dataSo;


        protected Transform TransformOwner => _transform;
        protected Rigidbody2D Rigidbody2DOwner => _rigidbody;
        protected Animator AnimatorOwner => _animator;
        protected Collider2D Collider2DOwner => _collider2D;
        protected StateMachine<T> StateMachine => _stateMachine;

        protected MyData DataSO => _dataSo;

        #endregion
        public State(T owner,string nameToTrigger,string animationName)
        {
             Owner = owner;
            _transform = owner.Transform;
            _animator = owner.Animator;
            _rigidbody = owner.Rigidbody2D;
            _collider2D = owner.Collider2D;
            _stateMachine = owner.StateMachine;
            _dataSo = owner.Data;
            NameToTrigger = nameToTrigger;
            AnimationName = animationName;
        }
        public virtual void Enter()
        {
            if (NameToTrigger != null)
            {
                IsAniamtionFinshed = false;
                IsAnimationName = false;
                AnimatorOwner.SetBool(NameToTrigger, true);
                Owner.StartCoroutine(CheckAnimationState(AnimatorOwner, AnimationName));
            }
        }
        public virtual void PhysicExcute()
        {
            
        }
        public virtual void LogicExcute()
        {
            if (IsAniamtionFinshed)
            {
                StateMachine.RevertOrinalState();
            }
        }
        public virtual void Exit()
        {
            if (NameToTrigger != null)
            {
                AnimatorOwner.SetBool(NameToTrigger, false);
            }
        }
        /// <summary>
        /// 攻击伤害计算
        /// </summary>
        /// <param name="attackPower"></param>
        /// <param name="criticalRate"></param>
        /// <param name="criticalDamage"></param>
        /// <returns></returns>
        protected float CalculateAttackWithoutCrit(float attackPower)
        {
            return attackPower;
        }
        /// <summary>
        /// 检测动画播放情况
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="animationName"></param>
        /// <param name="layerIndex"></param>
        /// <returns></returns>
        protected IEnumerator CheckAnimationState(Animator animator, string animationName,int layerIndex = 0)
        {
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(animationName));
            IsAnimationName = true;
            AnimationTime = animator.GetCurrentAnimatorStateInfo(layerIndex).length;
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime >= 0.98f);
            IsAniamtionFinshed = true;
        }

        /// <summary>
        /// 检测是否为当前State
        /// </summary>
        /// <returns></returns>
        protected bool CheckCurrentState()
        {
            if (!Equals(StateMachine.CurrentState, this))
                return false;
            return true;
        }
}
