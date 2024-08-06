using ErrorOr;
using FluentAssertions;
using GymManagment.Domain.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TestsCommon.Gyms;
using TestsCommon.Subscriptions;

namespace GymManagment.Domain.UnitTests.Subscriptions
{
    public class SubscriptionTests
    {
        //Arrange
        //Act
        //Assert
        [Fact]
        public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
        {
            //Arrange
            //Create Subscription
            var subscription = SubscriptionFactory.CreateSubscription();

            //Create the max number of gyms + 1
            var gyms = Enumerable.Range(0, subscription.GetMaxGyms() + 1)
                .Select(_ => GymFactory.CreateGym())
                .ToList();

            //Act
            //Add All Various Gyms
            var addGymResults = gyms.ConvertAll(subscription.AddGym);

            //Assert
            //Adding All gyms succeeded, but the last fail
            var allButLastGymResults = addGymResults[..^1];
            allButLastGymResults.Should().AllSatisfy(addGymResults => addGymResults.Value.Should().Be(Result.Success));

            var lastAddGymResult = addGymResults.Last();
            lastAddGymResult.IsError.Should().BeTrue();
            lastAddGymResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanTheSubscriptionAllows);
        }
    }
}
