namespace PersonalDevelopment
{
    public interface IPlayerProperties : ICharacterProperties
    {
        float JumpForce();
    }

    public interface ICharacterProperties
    {
        int Health();
        float MoveSpeed();
    }
}