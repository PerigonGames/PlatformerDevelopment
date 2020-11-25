using Sirenix.OdinInspector;
using UnityEngine;

namespace PersonalDevelopment
{
    [CreateAssetMenu(fileName = "Enemy Property", menuName = "PersonalDevelopemnt/EnemyProperty", order = 1)]
    public class EnemyProperties : ScriptableObject
    {
        [Title("Movement Properties")]
        [SerializeField] private float _moveDistance = 5f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private bool _willMoveLeftFirst = false;

        [Title("Melee Properties")] 
        [SerializeField] private float _pushBackHorizontal = 3f;
        [SerializeField] private float _pushBackVertical = 3f;
        
        public float MoveDistance => _moveDistance;
        
        public float MoveSpeed => _moveSpeed;

        public bool WillMoveLeftFirst => _willMoveLeftFirst;

        public float PushBackHorizontal => _pushBackHorizontal;

        public float PushBackVertical => _pushBackVertical;
    }
}
