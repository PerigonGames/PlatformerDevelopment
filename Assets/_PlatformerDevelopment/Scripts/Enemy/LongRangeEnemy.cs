using UnityEngine;

namespace PersonalDevelopment
{
    public class LongRangeEnemy : Enemy
    {
        protected override void Attack()
        {
            _canAttack = CanHitPlayer();

            Debug.Log("The player can Attack? " + _canAttack);
            //Add Shooting Mechanics here
            
        }

        private bool CanHitPlayer()
        {
            RaycastHit hit;

            if (_player != null)
            {
                Vector3 directionToPlayer = _player.transform.position - transform.position;

                if (Physics.Raycast(transform.position, directionToPlayer, out hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}

