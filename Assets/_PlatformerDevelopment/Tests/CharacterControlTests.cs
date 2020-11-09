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
        private Keyboard _keyboard = null;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("CharacterControl_Sandbox");
            _keyboard = InputSystem.AddDevice<Keyboard>();
        }

        [UnityTest]
        public IEnumerator Test_Sandbox_Start()
        {
            Assert.IsNotNull(GameObject.FindGameObjectWithTag("Player"));
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_Sandbox_PlayerInput_NotNull()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var input = player.GetComponent<PlayerInput>();
            Assert.IsNotNull(input);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_PressLeftArrowKey_Lower_XPosition()
        {
            //Given
            var player = GameObject.FindGameObjectWithTag("Player");
            var input = player.GetComponent<PlayerInput>();
            input.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(_keyboard.leftArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.Less(after, before, "Player should be another position from pressing left arrow key");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressRightArrowKey_Higher_XPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var input = player.GetComponent<PlayerInput>();
            input.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(_keyboard.rightArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.Greater(after, before, "Player X Position should be in a higher than original");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressDownArrowKey_Equal_XPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var input = player.GetComponent<PlayerInput>();
            input.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(_keyboard.upArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.AreEqual(after, before, "Player should be in the same position since down key doesn't do anything");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressUpArrowKey_Equal_XPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var input = player.GetComponent<PlayerInput>();
            input.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(_keyboard.downArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.x;
            Assert.AreEqual(after, before, "Pressing Up Arrow should not change X Position");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressUpArrowKey_Equal_YPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var input = player.GetComponent<PlayerInput>();
            input.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
            var before = player.gameObject.transform.position.x;
            
            //When
            Press(_keyboard.upArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.y;
            Assert.Greater(after, before, "Pressing Up Arrow key should change Y Position");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressDownArrowKey_Equal_YPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var input = player.GetComponent<PlayerInput>();
            input.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
            var before = player.gameObject.transform.position.y;
            
            //When
            Press(_keyboard.downArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.y;
            Assert.AreEqual(after, before, "Pressing Down Arrow key should have same Y Position");
        }
        
        [UnityTest]
        public IEnumerator Test_PressUpArrowKey_Greater_YPosition()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var input = player.GetComponent<PlayerInput>();
            input.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
            var before = player.gameObject.transform.position.y;
            
            //When
            Press(_keyboard.upArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = player.gameObject.transform.position.y;
            Assert.Greater(after, before, "Player should be in the same position since jump and land on same position");
        }
        
        [UnityTest]
        public IEnumerator Test_PressUpArrowKey_Wait2Seconds_Equal_Position()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var input = player.GetComponent<PlayerInput>();
            input.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
            var before = player.gameObject.transform.position;
            
            //When
            Press(_keyboard.upArrowKey);
            yield return new WaitForSeconds(2f);
            
            //Then
            var after = player.gameObject.transform.position;
            Assert.AreEqual(after, before, "Player should be in the same position since jump and land on same position");
        }
        
        
        [UnityTest]
        public IEnumerator Test_PressUpAndRight_On_Platform()
        {
            //Given 
            var player = GameObject.FindGameObjectWithTag("Player");
            var input = player.GetComponent<PlayerInput>();
            input.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);
            var before = player.gameObject.transform.position;
            
            //When
            PressAndRelease(_keyboard.upArrowKey);
            yield return new WaitForSeconds(0.25f);
            Press(_keyboard.rightArrowKey);
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForEndOfFrame();
            }
            Release(_keyboard.rightArrowKey);
            yield return new WaitForSeconds(1f);
            
            //Then
            var after = player.gameObject.transform.position;
            Assert.Greater(after.x, before.x, "Player X value should be on the platform");
            Assert.Greater(after.y, before.y, "Player Y value should be on the platform");
        }
    }
}
