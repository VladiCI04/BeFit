﻿using BeFit.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeFit.Data.Configurations
{
    public class BeFitEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder
                .HasOne(e => e.EventCategory)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.EventCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Coach)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CoachId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
