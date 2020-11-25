using Sirenix.OdinInspector;
using UnityEngine;

namespace PersonalDevelopment
{
    [CreateAssetMenu(fileName = "Enemy Property", menuName = "PersonalDevelopemnt/EnemyProperty", order = 1)]
    public class EnemyProperties : ScriptableObject, IEnemyProperties
    {
        [Title("Movement Properties")]
        [SerializeField] private float _moveDistance = 5f;
        [SerializeField] private float _moveSpeed = 5f;

        [Title("Melee Properties")] 
        [SerializeField] private float _pushBackHorizontal = 3f;
        [SerializeField] private float _pushBackVertical = 3f;

        public float MoveDistance()
        {
            return _moveDistance;
        }

        public float MoveSpeed()
        {
            return _moveSpeed;
        }

        public float PushBackHorizontal()
        {
            return _pushBackHorizontal;
        }

        public float PushBackVertical()
        {
            return _pushBackVertical;
        }
    }
}
