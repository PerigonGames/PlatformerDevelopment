using NUnit.Framework;
using PersonalDevelopment;

namespace Tests
{
    public class StateManagerTests
    {
        [SetUp]
        protected void Setup()
        {
            StateManager.Instance.SetState(State.StartMenu);
        }

        [Test]
        public void TestOnApplicationStartState()
        {
            //Assert
            Assert.AreEqual(StateManager.Instance.GetState(), State.StartMenu, "State should start as Start Menu");
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void TestStateManagerChangeStateToPlay()
        {
            // Arrange
            var stateManager = StateManager.Instance;
            
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
            var stateManager_1 = StateManager.Instance;
            var stateManager_2 = StateManager.Instance;
            
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

            var stateManager = StateManager.Instance;
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

            var stateManager = StateManager.Instance;
            stateManager.OnStateChanged += OnStateChanged;

            //Act
            stateManager.SetState(State.Play);

            //Assert
            Assert.AreEqual(resultingState, State.Play, "Resulting state should be Play");
        }
        
        [Test]
        public void TestStateManagerCallsBackOnStateChangeFromAnotherStateManagerObject()
        {
            //Arrange
            var result = 0;
            void OnStateChanged(State state)
            {
                result++;
            }

            var stateManager = StateManager.Instance;
            var stateManagerOther = StateManager.Instance;
            stateManager.OnStateChanged += OnStateChanged;

            //Act
            stateManagerOther.SetState(State.Play);

            //Assert
            Assert.AreEqual(result, 1, "Result should be 1, since callback adds 1");
        }

        
    }
}
