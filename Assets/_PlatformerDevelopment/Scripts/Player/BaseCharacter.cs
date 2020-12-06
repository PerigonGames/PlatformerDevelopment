using System;

namespace PersonalDevelopment
{
    public class BaseCharacter
    {
        private int _health = 0;

        public event Action OnDeath;
        public int Health => _health;
        
        public BaseCharacter(ICharacterProperties properties)
        {
            _health = properties.Health();
        }

        public void HitCharacter()
        {
            _health = Math.Max(_health - 1, 0);
            if (IsDead() && OnDeath != null)
            {
                OnDeath();
            }
        }

        private bool IsDead()
        {
            return _health <= 0;
        }
    }
}