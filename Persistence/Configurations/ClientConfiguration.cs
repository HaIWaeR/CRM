using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    /// <summary>
    /// Настраивает отображение <see cref="ClientEntity"/> на таблицу <c>Clients</c>:
    /// первичный ключ, обязательные поля и максимальную длину строк.
    /// </summary>
    /// <remarks>
    /// Ограничения длины совпадают с правилами валидаторов команд Client,
    /// поэтому при изменении одного нужно менять и другое.
    /// </remarks>
    public class ClientConfiguration : IEntityTypeConfiguration<ClientEntity>
    {
        /// <summary>
        /// Применяет настройки сущности клиента к модели EF Core.
        /// </summary>
        /// <param name="builder">Построитель конфигурации сущности, передаётся EF Core.</param>
        public void Configure(EntityTypeBuilder<ClientEntity> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.Property(x => x.MiddleName).HasMaxLength(100);
            builder.Property(x => x.Phone).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.Telegram).HasMaxLength(50);
            builder.Property(x => x.Address).HasMaxLength(300);
            builder.Property(x => x.Notes).HasMaxLength(500);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(ClientStatus.Active);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
        }
    }
}