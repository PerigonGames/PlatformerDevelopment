using System.Collections;
using NUnit.Framework;
using PersonalDevelopment;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class MenuSceneTests: SnapshotTestCase
    {

        [SetUp]
        protected override void Setup()
        {
            RecordMode = false;
            base.Setup();
            SceneManager.LoadScene("Menu");
        }

        [UnityTest]
        public IEnumerator TestMenuSceneScreens()
        {
            // Wait for 4 frames
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForEndOfFrame();
            }

            var startScreen = GameObject.FindObjectOfType(typeof(StartScreenBehaviour));
            var userInterfaceManager = GameObject.FindObjectOfType(typeof(MainMenuUserInterfaceManager));

            Assert.NotNull(startScreen, "Start screen should be in scene");
            Assert.NotNull(userInterfaceManager, "UI Manager should be on in the scene");
        }

        [UnityTest]
        public IEnumerator TestMenuSnapShotOnStart()
        {
            // Wait for 4 frames
            for (int i = 0; i < 50; i++)
            {
                yield return new WaitForEndOfFrame();
            }

            SnapshotVerifyView();
        }
        
        [UnityTest]
        public IEnumerator TestMenuSnapShotOnPlayerJoin()
        {
            // Wait for 4 frames
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForEndOfFrame();
            }
            
            var stateManager = StateManager.Instance;
            stateManager.SetState(State.CharacterSelection);

            // Wait for 4 frames
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForEndOfFrame();
            }
            
            SnapshotVerifyView();
        }
        
        [UnityTest]
        public IEnumerator TestMenuSnapShotOnEndGameStateHidesMenu()
        {
            // Wait for 4 frames
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForEndOfFrame();
            }
            
            var stateManager = StateManager.Instance;
            stateManager.SetState(State.EndGame);

            // Wait for 4 frames
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForEndOfFrame();
            }
            
            SnapshotVerifyView();
        }
    }
}
