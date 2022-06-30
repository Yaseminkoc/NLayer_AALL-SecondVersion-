using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class LessonSeed : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasData(
                new Lesson { Id = 1, Name = "Programming Languages", Credit = 5 },
                new Lesson { Id = 2, Name = "Discrete Structures", Credit = 4},
                new Lesson { Id = 3, Name = "Database Management Systems", Credit = 7 },
                new Lesson { Id = 4, Name = "Operating Systems", Credit = 5 }
            );
        }
    }
}
