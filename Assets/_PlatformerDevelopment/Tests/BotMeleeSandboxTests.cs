using System.Collections;
using NUnit.Framework;
using PersonalDevelopment;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class BotMeleeSandboxTests : InputTestFixture
    {

        private Keyboard _keyboard = null;
        private PlayerInput _input = null;
        private GameObject _player = null;
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("Bot_Melee_Sandbox");
            _keyboard = InputSystem.AddDevice<Keyboard>();
        }
        
        private void PlayerInputSetup()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _input = _player.GetComponent<PlayerInput>();
            _input.SwitchCurrentControlScheme("Keyboard&Mouse_Keys", Keyboard.current);
        }

        [UnityTest]
        public IEnumerator BotMeleePlayer_PushesBackPlayerLeft_LowerXPosition()
        {
            var playerStartingPosition = new Vector3(-0.5f, 1, 0);
            var player = GameObject.Find("Player_1");
            player.transform.position = playerStartingPosition;

            var botStartingPosition = new Vector3(0, 1, 0);
            var bot = GameObject.Find("Bot_Melee");
            var enemyproperties = new DummyEnemyProperties();
            enemyproperties._moveSpeed = 0;
            bot.GetComponent<BotMovementBehaviour>().Initialize(bot.GetComponent<Rigidbody>(), enemyproperties, false);
            bot.transform.position = botStartingPosition;
            yield return new WaitForSeconds(1f);

            Assert.Less(player.transform.position.x, playerStartingPosition.x, "Player should be pushed back");
        }

        [UnityTest]
        public IEnumerator BotMeleePlayer_PushesBackPlayerRight_HigherXPosition()
        {
            var playerStartingPosition = new Vector3(1, 0, 0);
            var player = GameObject.Find("Player_1");
            player.transform.position = playerStartingPosition;

            var botStartingPosition = new Vector3(0, 1, 0);
            var bot = GameObject.Find("Bot_Melee");
            bot.GetComponent<BotMovementBehaviour>().enabled = false;
            bot.transform.position = botStartingPosition;
            yield return new WaitForSeconds(1f);

            Assert.Greater(player.transform.position.x, playerStartingPosition.x, "Player should be pushed back");
        }

        [UnityTest]
        public IEnumerator BotMeleePlayer_PushesBackPlayerHigher_HigherYPosition()
        {
            var playerStartingPosition = new Vector3(1, 1, 0);
            var player = GameObject.Find("Player_1");
            player.transform.position = playerStartingPosition;

            var botStartingPosition = new Vector3(0, 1, 0);
            var bot = GameObject.Find("Bot_Melee");
            bot.GetComponent<BotMovementBehaviour>().enabled = false;
            bot.transform.position = botStartingPosition;
            yield return new WaitForSeconds(0.1f);

            Assert.Greater(player.transform.position.y, playerStartingPosition.y, "Player should be pushed back at higher position");
        }

        [UnityTest]
        public IEnumerator PlayerMeleeAttack_PushesBotBackToTheRight()
        {
            PlayerInputSetup();
            // When
            var playerStartingPosition = new Vector3(-2, 1, 0);
            var player = GameObject.Find("Player_1");
            player.transform.position = playerStartingPosition;

            var botProperties = new DummyEnemyProperties();
            botProperties._moveSpeed = 0;
            var botStartingPosition = new Vector3(0, 1, 0);
            var bot = GameObject.Find("Bot_Melee");
            bot.GetComponent<BotMovementBehaviour>().Initialize(bot.GetComponent<Rigidbody>(), botProperties, true);
            bot.transform.position = botStartingPosition;

            //Then
            PressAndRelease(_keyboard.slashKey);
            yield return new WaitForSeconds(1f);
            Assert.Greater(bot.transform.position.x, botStartingPosition.x, "Player's Melee should push bot back");
            
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator PlayerMeleeAttack_LookingRightOnRight_MeleesDoesNothing()
        {
            PlayerInputSetup();
            // When
            var playerStartingPosition = new Vector3(2, 1, 0);
            var player = GameObject.Find("Player_1");
            player.transform.position = playerStartingPosition;

            var botProperties = new DummyEnemyProperties();
            botProperties._moveSpeed = 0;
            var botStartingPosition = new Vector3(0, 1, 0);
            var bot = GameObject.Find("Bot_Melee");
            bot.GetComponent<BotMovementBehaviour>().Initialize(bot.GetComponent<Rigidbody>(), botProperties, true);
            bot.transform.position = botStartingPosition;

            //Then
            PressAndRelease(_keyboard.rightArrowKey);
            PressAndRelease(_keyboard.slashKey);
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(bot.transform.position.x, botStartingPosition.x, "Player's Melee should push bot back");
            
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator PlayerMeleeAttack_LookAtBotMelee_PushesBotLeft()
        {
            PlayerInputSetup();
            // When
            var playerStartingPosition = new Vector3(2, 1, 0);
            var player = GameObject.Find("Player_1");
            player.transform.position = playerStartingPosition;

            var botProperties = new DummyEnemyProperties();
            botProperties._moveSpeed = 0;
            var botStartingPosition = new Vector3(0, 1, 0);
            var bot = GameObject.Find("Bot_Melee");
            bot.GetComponent<BotMovementBehaviour>().Initialize(bot.GetComponent<Rigidbody>(), botProperties, true);
            bot.transform.position = botStartingPosition;

            //Then
            Press(_keyboard.leftArrowKey);
            yield return new WaitForSeconds(0.05f);
            Release(_keyboard.leftArrowKey);
            PressAndRelease(_keyboard.slashKey);
            yield return new WaitForSeconds(1f);
            Assert.Less(bot.transform.position.x, botStartingPosition.x, "Player's Melee should push bot back");
            
            yield return null;
        }
    }
}
