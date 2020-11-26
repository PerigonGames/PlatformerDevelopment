using System.Collections;
using NUnit.Framework;
using PersonalDevelopment;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class BotSandboxTests : InputTestFixture
    {
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("Bot_Melee_Sandbox");
        }
        
        [UnityTest]
        public IEnumerator BotMeleePlayer_PushesBackPlayerLeft_LowerXPosition()
        {
            var playerStartingPosition = new Vector3(-1, 0, 0);
            var player = GameObject.Find("Player_1");
            player.transform.position = playerStartingPosition;

            var botStartingPosition = new Vector3(0, 1, 0);
            var bot = GameObject.Find("Bot_Melee");
            bot.GetComponent<BotMovementBehaviour>().enabled = false;
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
            var playerStartingPosition = new Vector3(1, 0, 0);
            var player = GameObject.Find("Player_1");
            player.transform.position = playerStartingPosition;

            var botStartingPosition = new Vector3(0, 1, 0);
            var bot = GameObject.Find("Bot_Melee");
            bot.GetComponent<BotMovementBehaviour>().enabled = false;
            bot.transform.position = botStartingPosition;
            yield return new WaitForSeconds(0.1f);
            
            Assert.Greater(player.transform.position.y, playerStartingPosition.y, "Player should be pushed back at higher position");
        }
    }
}
