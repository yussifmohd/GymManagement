﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.Domain.Subscription
{
    public class Subscription
    {
        private readonly List<Guid> _gymIds = [];
        private readonly int _maxGyms;

        public Guid AdminId { get; }
        //readonly properties
        public Guid Id { get; private set; }
        public SubscriptionType SubscriptionType { get; private set; } = null!;

        public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
        {
            SubscriptionType = subscriptionType;
            AdminId = adminId;
            Id = id ?? Guid.NewGuid();

            _maxGyms = GetMaxGyms();
        }

        public int GetMaxGyms() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 1,
            nameof(SubscriptionType.Pro) => 3,
            _ => throw new InvalidOperationException()
        };

        public int GetMaxRooms() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 1,
            nameof(SubscriptionType.Starter) => 3,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException()
        };

        public int GetMaxDailySessions() => SubscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => 4,
            nameof(SubscriptionType.Starter) => int.MaxValue,
            nameof(SubscriptionType.Pro) => int.MaxValue,
            _ => throw new InvalidOperationException()
        };

        private Subscription()
        {

        }
    }
}