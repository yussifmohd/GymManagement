﻿using FluentAssertions;
using GymManagement.Api.IntegrationTests.Common;
using GymManagement.Contracts.Subscriptions;
using GymManagment.Api.IntegrationTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TestsCommon.TestConstants;

namespace GymManagement.Api.IntegrationTests.Controllers
{
    [Collection(GymManagementApiFactoryCollection.CollectionName)]
    public class CreateSubscriptionTests
    {
        private readonly HttpClient _client;

        public CreateSubscriptionTests(GymManagementApiFactory apiFactory)
        {
            _client = apiFactory.HttpClient;
            apiFactory.ResetDatabase();
        }

        [Theory]
        [MemberData(nameof(ListSubscriptionTypes))]
        public async Task CreateSubscription_WhenValidSubscription_ShouldCreateSubscription(SubscriptionType subscriptionType)
        {
            //Arrange
            var createSubscriptionRequest = new CreateSubscriptionRequest(subscriptionType, Constants.Admin.Id);

            //Act
            var response = await _client.PostAsJsonAsync("api/Subscriptions", createSubscriptionRequest);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();

            var subscriptionResponse = await response.Content.ReadFromJsonAsync<SubscriptionResponse>();
            subscriptionResponse.Should().NotBeNull();
            subscriptionResponse!.SubscriptionType.Should().Be(subscriptionType);

            response.Headers.Location!.PathAndQuery.Should().Be($"/api/Subscriptions/{subscriptionResponse.Id}");
        }

        public static TheoryData<SubscriptionType> ListSubscriptionTypes()
        {
            var subscriptionTypes = Enum.GetValues<SubscriptionType>().ToList();

            var theoryData = new TheoryData<SubscriptionType>();

            foreach (var subscriptionType in subscriptionTypes)
            {
                theoryData.Add(subscriptionType);
            }

            return theoryData;
        }
    }
}
