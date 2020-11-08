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
            var player = GameObject.FindGameObjectWithTag("Player");
            Assert.IsNotNull(player);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestCharacterControlsPressingLeftArrowKeyGetsLowerXPosition()
        {
            //Given
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(keyboard.leftArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.Less(after, before, "Player should be another position from pressing left arrow key");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestCharacterControlsPressingRightArrowKeyGetsHigherXPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(keyboard.rightArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.Greater(after, before, "Player X Position should be in a higher than original");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestCharacterControlsPressingDownArrowKeyGetsSameXPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(keyboard.upArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.AreEqual(after, before, "Player should be in the same position since down key doesn't do anything");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestCharacterControlsPressingUpArrowKeyGetsSameXPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(keyboard.downArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.AreEqual(after, before, "Pressing Up Arrow should not change X Position");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestCharacterControlsPressingUpArrowKeyDifferentYPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(keyboard.upArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.y;
            Assert.Greater(after, before, "Pressing Up Arrow key should change Y Position");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestCharacterControlsPressingDownArrowKeySameYPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position.y;
            
            //When
            Press(keyboard.downArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.y;
            Assert.AreEqual(after, before, "Pressing Down Arrow key should have same Y Position");
        }
        
        [UnityTest]
        public IEnumerator TestCharacterControlsPressingUpArrowKeyWaitFor2SecondsLandOnSamePosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position;
            
            //When
            Press(keyboard.upArrowKey);
            yield return new WaitForSeconds(2f);
            
            //Then
            var after = player.gameObject.transform.position;
            Assert.AreEqual(after, before, "Player should be in the same position since jump and land on same position");
        }
        
        
        [UnityTest]
        public IEnumerator TestCharacterControlsPressUpAndRightToLandOnPlatform()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var before = player.gameObject.transform.position;
            
            //When
            PressAndRelease(keyboard.upArrowKey);
            yield return new WaitForSeconds(0.25f);
            Press(keyboard.rightArrowKey);
            yield return new WaitForSeconds(0.25f);
            Release(keyboard.rightArrowKey);
            yield return new WaitForSeconds(1f);
            
            //Then
            var after = player.gameObject.transform.position;
            Assert.Greater(after.x, before.x, "Player should be on the platform");
            Assert.Greater(after.y, before.y, "Player should be on the platform");
        }
    }
}
