using System;

namespace PersonalDevelopment
{
    public class BaseCharacter
    {
        private int _health = 0;

        public event Action OnDeath;

        public BaseCharacter(ICharacterProperties properties)
        {
            _health = properties.Health();
        }
        
        private bool IsDead()
        {
            return _health <= 0;
        }

        public void HitCharacter()
        {
            _health = Math.Max(_health - 1, 0);
            if (IsDead() && OnDeath != null)
            {
                OnDeath();
            }
        }
    }
}