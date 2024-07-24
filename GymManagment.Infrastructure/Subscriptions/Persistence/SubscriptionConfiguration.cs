﻿using GymManagment.Domain.Subscription;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.Infrastructure.Subscriptions.Persistence
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .ValueGeneratedNever();

            builder.Property("_adminId")
                .HasColumnName("AdminId");

            builder.Property(s => s.SubscriptionType)
                .HasConversion( //mapping values in and out DB
                    subsriptionType => subsriptionType.Value, //way in the DB
                    value => SubscriptionType.FromValue(value) //way out the DB
                );
        }
    }
}
