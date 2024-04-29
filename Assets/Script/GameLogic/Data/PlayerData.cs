using Sirenix.OdinInspector;
using UnityEngine;
namespace Script.GameLogic.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/PlayerData", order = 0)]
    public class PlayerData : MyData
    {
        [field: FoldoutGroup("属性")][field:SerializeField] public float MoveSpeed { get; set;}
        [field: FoldoutGroup("属性")][field:SerializeField] public float JumpForce { get; set;}
        [field: FoldoutGroup("属性")][field:SerializeField] public float MaxSpeedX { get; set;}
        [field: FoldoutGroup("属性")][field:SerializeField] public float AccelerationX { get; set;}
        [field: FoldoutGroup("属性")][field:SerializeField] public float YSpeedUpTime { get; set;}
        [field: FoldoutGroup("属性")][field:SerializeField] public float MaxSpeedY { get; set;}
        [field: FoldoutGroup("属性")][field:SerializeField] public float AccelerationY { get; set;}

        [field: FoldoutGroup("属性")] [field: SerializeField] public Vector2 Size { get; set; }

        [field: FoldoutGroup("属性")] [field: SerializeField] public Vector3 EffectOffset { get; set; }
        
        [field: FoldoutGroup("属性")] [field: SerializeField] public GameObject EffectPrefab { get; set; }
    }
}