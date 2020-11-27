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
        private PlayerInput _input = null;
        private GameObject _player = null;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("CharacterControl_Sandbox");
            _keyboard = InputSystem.AddDevice<Keyboard>();
        }

        private void PlayerInputSetup()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _input = _player.GetComponent<PlayerInput>();
            _input.SwitchCurrentControlScheme("Keyboard&Mouse_Keys", Keyboard.current);
        }

        [UnityTest]
        public IEnumerator Test_Sandbox_Start()
        {
            PlayerInputSetup();
            Assert.IsNotNull(_player);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_Sandbox_PlayerInput_NotNull()
        {
            Assert.IsNotNull(_input);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_PressLeftArrowKey_Lower_XPosition()
        {
            //Given
            PlayerInputSetup();
            var before = _player.gameObject.transform.position.x;
            
            //When
            Press(_keyboard.leftArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = _player.gameObject.transform.position.x;
            Assert.Less(after, before, "Player should be another position from pressing left arrow key");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressLeftArrowKeyWhileFacingRight_Equal_180YRotation()
        {
            //Given
            PlayerInputSetup();
            _player.gameObject.transform.rotation = Quaternion.identity;

            //When
            Press(_keyboard.leftArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var rotationAfter = _player.gameObject.transform.rotation.eulerAngles.y;
            Assert.AreEqual(180f, rotationAfter, "Player rotation should be 180 from pressing left arrow key");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressRightArrowKeyWhileFacingLeft_Equal_0YRotation()
        {
            //Given
            PlayerInputSetup();
            _player.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);

            //When
            Press(_keyboard.rightArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var rotationAfter = _player.gameObject.transform.rotation;
            Assert.AreEqual(Quaternion.identity, rotationAfter, "Player rotation should be 180 from pressing left arrow key");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressRightArrowKey_Higher_XPosition()
        {
            //Given 
            PlayerInputSetup();
            var before = _player.gameObject.transform.position.x;
            
            //When
            Press(_keyboard.rightArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = _player.gameObject.transform.position.x;
            Assert.Greater(after, before, "Player X Position should be in a higher than original");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressDownArrowKey_Equal_XPosition()
        {
            //Given 
            PlayerInputSetup();
            var before = _player.gameObject.transform.position.x;
            
            //When
            Press(_keyboard.upArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = _player.gameObject.transform.position.x;
            Assert.AreEqual(after, before, "Player should be in the same position since down key doesn't do anything");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressUpArrowKey_Equal_XPosition()
        {
            //Given 
            PlayerInputSetup();
            var before = _player.gameObject.transform.position.x;
            
            //When
            Press(_keyboard.downArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = _player.gameObject.transform.position.x;
            Assert.AreEqual(after, before, "Pressing Up Arrow should not change X Position");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressUpArrowKey_Equal_YPosition()
        {
            //Given 
            PlayerInputSetup();
            var before = _player.gameObject.transform.position.x;
            
            //When
            Press(_keyboard.upArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = _player.gameObject.transform.position.y;
            Assert.Greater(after, before, "Pressing Up Arrow key should change Y Position");
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Test_PressDownArrowKey_Equal_YPosition()
        {
            //Given 
            PlayerInputSetup();
            var before = _player.gameObject.transform.position.y;
            
            //When
            Press(_keyboard.downArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = _player.gameObject.transform.position.y;
            Assert.AreEqual(after, before, "Pressing Down Arrow key should have same Y Position");
        }
        
        [UnityTest]
        public IEnumerator Test_PressUpArrowKey_Greater_YPosition()
        {
            //Given 
            PlayerInputSetup();
            var before = _player.gameObject.transform.position.y;
            
            //When
            Press(_keyboard.upArrowKey);
            yield return new WaitForSeconds(0.5f);
            
            //Then
            var after = _player.gameObject.transform.position.y;
            Assert.Greater(after, before, "Player should be in the same position since jump and land on same position");
        }
        
        [UnityTest]
        public IEnumerator Test_PressUpArrowKey_Wait2Seconds_Equal_Position()
        {
            //Given 
            PlayerInputSetup();
            var before = _player.gameObject.transform.position;
            
            //When
            Press(_keyboard.upArrowKey);
            yield return new WaitForSeconds(2f);
            
            //Then
            var after = _player.gameObject.transform.position;
            Assert.AreEqual(after, before, "Player should be in the same position since jump and land on same position");
        }
        
        
        [UnityTest]
        public IEnumerator Test_PressUpAndRight_On_Platform()
        {
            //Given 
            PlayerInputSetup();
            var before = _player.gameObject.transform.position;
            
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
            var after = _player.gameObject.transform.position;
            Assert.Greater(after.x, before.x, "Player X value should be on the platform");
            Assert.Greater(after.y, before.y, "Player Y value should be on the platform");
        }
    }
}
