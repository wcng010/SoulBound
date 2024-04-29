using System.Collections;
using System.Collections.Generic;
using C_Script.BaseClass;
using Script.GameLogic.Component;
using Script.GameLogic.Data;
using Script.GameLogic.Entity.Object.Clock;
using Script.GameLogic.Entity.Player.State;
using UnityEngine;
using UnityEngine.Serialization;

public enum StateType
{
    OnGroundStatePlayer,
    MoveStatePlayer,
    JumpStatePlayer,
    DeathStatePlayer,
    OnAirStatePlayer,
    GlobalStatePlayer,
    AbsorbStatePlayer
}
public class PhysicEntity : PhysicObject<PhysicEntity>
{
    
        [Header("角色状态字典")] public readonly Dictionary<StateType, State<PhysicEntity>> StateDic=new Dictionary<StateType, State<PhysicEntity>>();

        private CollisionComponent _collisionComponent;

        public CollisionComponent CollisionComponent => _collisionComponent
            ? _collisionComponent
            : _collisionComponent = GetComponent<CollisionComponent>(); 
        
        public PlayerData playerData;
        public bool IsGroundThreeRays => CollisionComponent.ThreeRaysGroundCheck;
        public bool IsGroundOneRay => CollisionComponent.RayGroundCheck;

        public RaycastHit2D CeilingOneRay => CollisionComponent.RayCeilingCheck();

        public Collider2D IsObjectOnRange => CollisionComponent.OverlapCheckObject;

        private Vector3 _birthPoint;
        
        protected virtual void Awake()
        {
            FindComponent();
            InitStateDictionary();
            InitOriginState();
            InitDataSetting();
        }

        private void Start()
        {
            _birthPoint = transform.position;
        }

        protected virtual void OnEnable()
        {
            PlayerSubScribe();
        }
        
        private void FixedUpdate()
        {
            PhysicBehaviour();
        }
        
        protected virtual void Update()
        {
            SwitchState();
            LogicBehaviour();
        }
        
        protected virtual void OnDisable()
        {
           PlayerRemoveScribe();
        }
        
        protected virtual void FindComponent()
        {
            InitAnimator(GetComponent<Animator>());
            InitRigidbody2D(GetComponent<Rigidbody2D>());
            InitCollider2D(GetComponent<Collider2D>());
            InitTransform(transform);
            InitData(playerData);
        }
        public void PlayerSubScribe()
        {
            EventCentreManager.Instance.Subscribe(MyEventType.ReStart,DeathEvent);
        }
        public void PlayerRemoveScribe()
        {
            EventCentreManager.Instance.Unsubscribe(MyEventType.ReStart,DeathEvent);
        }
        
        protected override void InitOriginState()
        {
            StateMachine.SetPreviousState(null);
            StateMachine.SetGlobalState(StateDic[StateType.GlobalStatePlayer]);
            StateMachine.SetCurrentState(StateDic[StateType.OnGroundStatePlayer]);
            StateMachine.SetOriginalState(StateDic[StateType.OnGroundStatePlayer]);
        }
        protected override void InitDataSetting()
        {

        }
        
        public override void HurtEvent()
        {
            throw new System.NotImplementedException();
        }

        public override void DeathEvent()
        {
            transform.position = _birthPoint;
        }

        protected override void InitStateDictionary()
        {
            StateMachine = new StateMachine<PhysicEntity>(this);
            /*StateDic.Add(PlayerStateType.MoveStatePlayer,new MoveStatePlayer(this,"Move","player_Move"));
            StateDic.Add(PlayerStateType.JumpStatePlayer,new JumpStatePlayer(this,"Jump","player_Jump"));
            StateDic.Add(PlayerStateType.DeathStatePlayer,new DeathStatePlayer(this,"Death","player_Death"));
            StateDic.Add(PlayerStateType.OnAirStatePlayer,new OnAirStatePlayer(this,"OnAir","player_OnAir"));
            StateDic.Add(PlayerStateType.OnGroundStatePlayer, new OnGroundStatePlayer(this,null,null));*/
            StateDic.Add(StateType.MoveStatePlayer,new MoveStatePlayer(this,"Move","PlayerMove"));
            StateDic.Add(StateType.JumpStatePlayer,new JumpStatePlayer(this,"Jump","PlayerJump"));
            StateDic.Add(StateType.DeathStatePlayer,new DeathStatePlayer(this,null,null));
            StateDic.Add(StateType.OnAirStatePlayer,new OnAirStatePlayer(this,"Fall","PlayerFall"));
            StateDic.Add(StateType.OnGroundStatePlayer, new OnGroundStatePlayer(this,null,null));
            StateDic.Add(StateType.GlobalStatePlayer, new GlobalStatePlayer(this,null,null));
            StateDic.Add(StateType.AbsorbStatePlayer, new AbsorbStatePlayer(this,null,null));
        }
        // ReSharper disable Unity.PerformanceAnalysis
        
        protected override void SwitchState()
        {
        }

        public virtual void OnChangeWithTime(int layer, int time)
        {
            
        }


        public virtual void PlayerChangeAudio() => AudioSystem.Instance.ChangeAudioPlay();
        
        public virtual void PlayerAbsorbBeginAudio() => AudioSystem.Instance.AbsorbBeginAudioPlay();
        
        public virtual void PlayerAbsorbLoopAudio() => AudioSystem.Instance.AbsorbLoopAudioPlay();
        
        public virtual void PlayerAbsorbEndAudio() => AudioSystem.Instance.AbsorbLoopAudioStop();
        public void SetFalseObj() => gameObject.SetActive(false);
}


