using NUnit.Framework;
using PersonalDevelopment;
using UnityEngine;

namespace Tests
{
    public class BotMovementTests
    {
        [Test]
        public void GetDestination_MoveLeft_LowerXValue()
        {
            //Arrange
            var originalPosition = Vector3.zero;
            var moveDistance = 1;
            var isMovingLeft = true;
            var deltaTime = 0.5f;
            var botMovement = new BotMovement(originalPosition, moveDistance, isMovingLeft, deltaTime);

            //Act
            var actual = botMovement.GetDestination(originalPosition, 5);
            
            //Assert
            Assert.Less(actual.x, originalPosition.x, "resulting position should be lower x position than original");
        }
        
        [Test]
        public void GetDestination_MoveRight_HigherXValue()
        {
            //Arrange
            var originalPosition = Vector3.zero;
            var moveDistance = 1;
            var isMovingLeft = false;
            var deltaTime = 0.5f;
            var botMovement = new BotMovement(originalPosition, moveDistance, isMovingLeft, deltaTime);

            //Act
            var actual = botMovement.GetDestination(originalPosition, 5);
            
            //Assert
            Assert.Greater(actual.x, originalPosition.x, "resulting position should be lower x position than original");
        }

        [Test]
        public void UpdateDestinationIfNeeded_OriginalPosition_ChangingDirectionLeftToRight()
        {
            //Arrange
            var originalPosition = Vector3.zero;
            var moveDistance = 3;
            var isMovingLeft = true;
            var deltaTime = 1f;
            var botMovement = new BotMovement(originalPosition, moveDistance, isMovingLeft, deltaTime);
            var currentPosition = new Vector3(-5, 0, 0);
            
            //Act
            botMovement.UpdateDestinationIfNeeded(currentPosition);
            var resulting = botMovement.GetDestination(currentPosition, 5);
            
            //Assert
            Assert.Greater(resulting.x, currentPosition.x, "Resulting Next Destination should be Greater than current Position");
        }
        
        [Test]
        public void UpdateDestinationIfNeeded_OriginalPosition_ChangingDirectionRightToLeft()
        {
            //Arrange
            var originalPosition = Vector3.zero;
            var moveDistance = 3;
            var isMovingLeft = false;
            var deltaTime = 1f;
            var botMovement = new BotMovement(originalPosition, moveDistance, isMovingLeft, deltaTime);
            var currentPosition = new Vector3(5, 0, 0);
            
            //Act
            botMovement.UpdateDestinationIfNeeded(currentPosition);
            var resulting = botMovement.GetDestination(currentPosition, 5);
            
            //Assert
            Assert.Less(resulting.x, currentPosition.x, "Resulting Next Destination should be Greater than current Position");
        }
    }
}
