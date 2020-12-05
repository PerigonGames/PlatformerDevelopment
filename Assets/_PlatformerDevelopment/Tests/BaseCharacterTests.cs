using NUnit.Framework;
using PersonalDevelopment;

namespace Tests
{
    public class BaseCharacterTests
    {
        [Test]
        public void BaseCharacter_OnHitOnce3Health_Alive()
        {
            var characterProperties = new DummyPlayerClass();
            characterProperties._health = 3;
            var character = new BaseCharacter(characterProperties);
            
            character.HitCharacter();
            
            Assert.AreEqual(2, character.Health, "Health should be at 2");
        }
        
        [Test]
        public void BaseCharacter_OnHitOnce1Health_Death()
        {
            var characterProperties = new DummyPlayerClass();
            characterProperties._health = 1;
            var character = new BaseCharacter(characterProperties);
            var delegateCalls = 0;
            character.OnDeath += () =>
            {
                delegateCalls++;
            };
            
            character.HitCharacter();
            
            Assert.AreEqual(delegateCalls, 1, "Delegate should be called once.");
            Assert.AreEqual(0, character.Health, "Health should be at 0");
        }
    }

}
