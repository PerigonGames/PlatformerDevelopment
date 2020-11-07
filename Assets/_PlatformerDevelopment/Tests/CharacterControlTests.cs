using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class CharacterControlTests : InputTestFixture
    {
        private Keyboard keyboard = null;
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            Time.timeScale = 100;
            SceneManager.LoadScene("CharacterControl_Sandbox");
            keyboard = InputSystem.AddDevice<Keyboard>();
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
            Time.timeScale = 0;
        }

        [UnityTest]
        public IEnumerator TestCharacterControlSandboxStart()
        {
            yield return null;
            var player = GameObject.FindGameObjectWithTag("Player");
            Assert.IsNotNull(player);
        }

        [UnityTest]
        public IEnumerator TestCharacterControlsPressingLeftArrowKeyGetsLowerXPosition()
        {
            //Given
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(keyboard.leftArrowKey);
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(2);
            }
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.Less(after, before, "Player should be another position from pressing left arrow key");
        }
        
        [UnityTest]
        public IEnumerator TestCharacterControlsPressingRightArrowKeyGetsHigherXPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(keyboard.rightArrowKey);
            
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(2);
            }
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.Greater(after, before, "Player should be another position from pressing left arrow key");
        }
        
        [UnityTest]
        public IEnumerator TestCharacterControlsPressingDownArrowKeyGetsSameXPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(keyboard.upArrowKey);
            
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(2);
            }
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.AreEqual(after, before, "Player should be another position from pressing left arrow key");
        }
        
        [UnityTest]
        public IEnumerator TestCharacterControlsPressingUpArrowKeyGetsSameXPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(keyboard.downArrowKey);
            
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(2);
            }
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.AreEqual(after, before, "Player should be another position from pressing left arrow key");
        }
    }
}
