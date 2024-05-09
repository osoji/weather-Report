using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherReport.Domain;

namespace WeatherReport.Infrastructure.EntityConfig
{
    public class WeatherConfig : IEntityTypeConfiguration<Weather>
    {
        public void Configure(EntityTypeBuilder<Weather> builder)
        {
            builder.ToTable("Weather");
            builder.HasKey(o => o.WeatherHistoryId);
            builder.Property(o => o.Country).IsRequired().HasMaxLength(255);
            builder.Property(o => o.City).IsRequired().HasMaxLength(255);
            builder.Property(o => o.Icon).IsRequired().HasMaxLength(255);
            builder.Property(o => o.AvTempratureC).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(o => o.AvTempratureF).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(o => o.MaxWindMPH).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(o => o.MaxWindKPH).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(o => o.Humidity).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(o => o.WeatherCondition).IsRequired().HasMaxLength(255);
            builder.Property(o => o.ForecastDate).IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");
        }
    }
}
