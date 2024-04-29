using Script.GameLogic.Component;
using Sirenix.OdinInspector;
using UnityEngine;

public class CollisionComponent : MyComponent
{
 #region PosSetting
        [field:FoldoutGroup("CommonSetting")] [field:SerializeField] public Transform BodyTrans { get; private set; }
        [field:FoldoutGroup("CommonSetting")] [field: SerializeField] public Transform FootTrans { get; private set; }
        [field:FoldoutGroup("CommonSetting")] [field: SerializeField] public Transform LowerRightPoint { get; private set;}
        [field:FoldoutGroup("CommonSetting")] [field: SerializeField] public Transform LowerLeftPoint { get; private set; }
        [field:FoldoutGroup("CommonSetting")] [field: SerializeField] public float GroundCheckDistance { get; private set; }
        [field:FoldoutGroup("CommonSetting")] [field: SerializeField] public float ObjectCheckDistance { get; private set; } 
        
        [field:FoldoutGroup("CommonSetting")] [field: SerializeField] public float CeilingCheckDistance { get; private set; } 
        
        [field: SerializeField] public Transform OwnerTrans { get; private set; }
        
        [field: SerializeField] public bool IsDebug { get; private set; }
        
        #endregion

        #region PlayerCheck
        public bool RayGroundCheck
        {
            get => Physics2D.Raycast(FootTrans.position, Vector2.down, GroundCheckDistance / 2,
                       1 << LayerMask.NameToLayer("Ground")) ||
                   Physics2D.Raycast(FootTrans.position, Vector2.down, GroundCheckDistance / 2,
                       1 << LayerMask.NameToLayer("Object"));
        }
        public bool ThreeRaysGroundCheck
        {
            get => Physics2D.Raycast(FootTrans.position, Vector2.down, GroundCheckDistance / 2,
                       1 << LayerMask.NameToLayer("Ground")) ||
                   Physics2D.Raycast(LowerLeftPoint.position, Vector2.down, GroundCheckDistance / 2,
                       1 << LayerMask.NameToLayer("Ground")) ||
                   Physics2D.Raycast(LowerRightPoint.position, Vector2.down, GroundCheckDistance / 2,
                       1 << LayerMask.NameToLayer("Ground")) ||
                   Physics2D.Raycast(FootTrans.position, Vector2.down, GroundCheckDistance / 2,
                       1 << LayerMask.NameToLayer("Object")) ||
                   Physics2D.Raycast(LowerLeftPoint.position, Vector2.down, GroundCheckDistance / 2,
                       1 << LayerMask.NameToLayer("Object")) ||
                   Physics2D.Raycast(LowerRightPoint.position, Vector2.down, GroundCheckDistance / 2,
                       1 << LayerMask.NameToLayer("Object"));
        }

        public RaycastHit2D RayCeilingCheck()
        {
            if (Physics2D.Raycast(FootTrans.position, Vector2.up, CeilingCheckDistance,
                    1 << LayerMask.NameToLayer("Object"))) 
                return default;
            return Physics2D.Raycast(FootTrans.position, Vector2.up, CeilingCheckDistance ,
                 1 << LayerMask.NameToLayer("Ground"));
        }

        public Collider2D OverlapCheckObject
        {
            get => Physics2D.OverlapCircle(BodyTrans.position, ObjectCheckDistance,
                1 << LayerMask.NameToLayer("Object"));
        }
        #endregion
    
}
