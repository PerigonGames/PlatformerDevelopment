using NUnit.Framework;
using PersonalDevelopment;

namespace Tests
{
    public class StateManagerTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestStateManagerChangeStateToPlay()
        {
            // Arrange
            var stateManager = new StateManager();
            
            //Act
            stateManager.SetState(State.Play);
            var result = stateManager.GetState();
            
            //Assert
            Assert.AreEqual(result, State.Play, "State should be play");
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void TestStateManagerStateTheSameWithDifferentObjects()
        {
            // Arrange
            var stateManager_1 = new StateManager();
            var stateManager_2 = new StateManager();
            
            //Act
            stateManager_2.SetState(State.PreGame);
            stateManager_1.SetState(State.Play);
            var result = stateManager_2.GetState();
            
            //Assert
            Assert.AreEqual(result, State.Play, "State should be play");
        }

        [Test]
        public void TestStateManagerCallsBackOnStateChange()
        {
            //Arrange
            var result = 0;
            void OnStateChanged(State state)
            {
                result++;
            }

            var stateManager = new StateManager();
            stateManager.OnStateChanged += OnStateChanged;

            //Act
            stateManager.SetState(State.Play);

            //Assert
            Assert.AreEqual(result, 1, "Result should be 1, since callback adds 1");
        }
        
        [Test]
        public void TestStateManagerCallsBackReturnsCorrectState()
        {
            //Arrange
            var resultingState = State.StartMenu;
            void OnStateChanged(State state)
            {
                resultingState = state;
            }

            var stateManager = new StateManager();
            stateManager.OnStateChanged += OnStateChanged;

            //Act
            stateManager.SetState(State.Play);

            //Assert
            Assert.AreEqual(resultingState, State.Play, "Resulting state should be Play");
        }

        
    }
}
