﻿namespace PersonalDevelopment
{
    public class DummyEnemyProperties : IEnemyProperties
    {
        public int _health = 3;
        public float _moveDistance = 5;
        public float _moveSpeed = 5;
        public float _pushBackHorizontal = 100;
        public float _pushBackVertical = 100;
        
        public float MoveDistance()
        {
            return _moveDistance;
        }

        public int Health()
        {
            return _health;
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
