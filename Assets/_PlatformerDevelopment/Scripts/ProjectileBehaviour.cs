using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace PersonalDevelopment
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField] private float _distance = 5;
        [SerializeField] private float _speed = 5;
        [SerializeField] private string _enemyTag = "";

        private Vector3 _direction = Vector3.zero;
        private Vector3 _destination = Vector3.zero;

        public void Initialize(bool isGoingLeft, Vector3 startingPosition)
        {
            transform.position = startingPosition;
            _direction = isGoingLeft ? Vector3.left : Vector3.right;
            _destination = transform.position + (_direction * _distance);
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, _destination, _speed * Time.fixedDeltaTime);
            if (transform.position == _destination)
            {
                OnReachedDestination();
            }
        }

        private void OnReachedDestination()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            var hotObject = other.gameObject;
            if (hotObject.CompareTag(_enemyTag))
            {
                var enemy = hotObject.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.HurtBot();
                    return;
                }

                var player = hotObject.GetComponent<PlayerBehaviour>();
                if (player)
                {
                    player.HurtPlayer();
                    return;
                }
            }
        }
    }
}
