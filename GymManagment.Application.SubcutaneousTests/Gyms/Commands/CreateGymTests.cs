using FluentAssertions;
using GymManagement.Application.SubcutaneousTests.Common;
using GymManagement.Domain.Subscription;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCommon.Gyms;
using TestsCommon.Subscriptions;

namespace GymManagement.Application.SubcutaneousTests.Gyms.Commands
{
    [Collection(MediatorFactoryCollection.CollectionName)]
    public class CreateGymTests(MediatorFactory mediatorFactory)
    {
        private readonly IMediator _mediator = mediatorFactory.CreateMediator();

        //Supposed To test the whole flow from underneath the presentation layer
        //Subcutaneous Testing is used to test the application layer as a whole instead of testing a specific function
        [Fact]
        public async void CreateGym_WhenValidCommand_ShouldCreateGym()
        {
            //Arrange
            //Create a subscription
            var subscription = await CreateSubscription();

            //Create a valid CreateGymCommand
            var createGymCommand = GymCommandFactory.CreateCreateGymCommand(subscriptionId: subscription.Id);

            //Act
            //Send the Create GymCommand To the MediatR
            var createGymResult = await _mediator.Send(createGymCommand);

            //Assert
            //The Result is a gym the corresponds to the details in the create gym command
            createGymResult.IsError.Should().BeFalse();
            createGymResult.Value.SubscriptionId.Should().Be(subscription.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(200)]
        public async Task CreateGym_WhenCommandContainsInvalidData_ShouldReturnValidationError(int gymNameLength)
        {
            //Arrange
            string gymName = new('a', gymNameLength);
            var createdGymCommand = GymCommandFactory.CreateCreateGymCommand(name: gymName);

            //Act
            var result = await _mediator.Send(createdGymCommand);

            //Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be("Name");
        }


        private async Task<Subscription> CreateSubscription()
        {
            // 1. Create a CreateSubscriptionCommand
            var createSubscriptionRequest = SubscriptionCommandFactory.CreateCreateSubscriptionCommand();

            //  2. Sending it to MediatR
            var result = await _mediator.Send(createSubscriptionRequest);

            // 3. Making sure it was created successfully
            result.IsError.Should().BeFalse();

            return result.Value;
        }
    }
}
