using UnityEngine;

namespace PersonalDevelopment {
    public class CollectibleBehaviour : MonoBehaviour
    {
        private GameObject _player = null;
        private Rigidbody _rigidbody = null;
        [SerializeField] private float _speed = 10f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _rigidbody.AddForce(Vector3.right * 5, ForceMode.Impulse);
            _rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

        private void FixedUpdate()
        {
            if (_player != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position,
                    _speed * Time.fixedDeltaTime);
                
                if (transform.position == _player.transform.position)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _rigidbody.isKinematic = true;
                _player = other.gameObject;
                GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }
}