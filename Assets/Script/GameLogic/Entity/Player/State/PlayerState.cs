using System.Collections.Generic;
using C_Script.Manager;
using Script.GameLogic.Data;
using UnityEngine;

    /// <summary>
    /// In order to compound PlayerState function and data
    /// </summary>
    public abstract class PlayerState:State<PhysicEntity>
    {
        protected float XAxis => Input.GetAxis("Horizontal");

        protected bool Tabkey => Input.GetKeyDown(KeyCode.Tab);
        protected bool SpaceKey => InputManager.Instance.InputSpace;
        protected PlayerData PlayerData => DataSO as PlayerData;
        protected Dictionary<StateType, State<PhysicEntity>> StateDictionary => Owner.StateDic;
        
        protected virtual void MoveBehaviour(float maxSpeedX,float accelerationX)
        {
            if (XAxis<0)
            {
                TransformOwner.localScale = new Vector3(-PlayerData.Size.x, PlayerData.Size.y, 1);
            }
            else if(XAxis>0)
            {
                TransformOwner.localScale = new Vector3(PlayerData.Size.x ,PlayerData.Size.y, 1);
            }
            float inputX = XAxis;
                if (XAxis != 0) {
                    float velocityX = Mathf.Clamp(Rigidbody2DOwner.velocity.x + inputX * accelerationX * Time.fixedDeltaTime,-maxSpeedX,maxSpeedX);
                    Rigidbody2DOwner.velocity = new Vector2(velocityX,Rigidbody2DOwner.velocity.y);
                }
            
        }
        
        protected PlayerState(PhysicEntity owner, string animationName, string nameToTrigger) : base(owner, animationName, nameToTrigger)
        { }
    }
