namespace PersonalDevelopment
{
    public interface IEnemyProperties : ICharacterProperties
    { 
        float MoveDistance();
        float PushBackHorizontal();
        float PushBackVertical();
    }
}