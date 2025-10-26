using Core.Clientes.Domain.Entities;
using Core.Clientes.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Clientes.Persistence.Repositories.EFCore.Config
{
    internal class Cliente_contactConfig : IEntityTypeConfiguration<contacto>
    {
        public void Configure(EntityTypeBuilder<contacto> builder)
        {
            builder.ToTable("contacto");
            builder.HasKey(p => p.contacto_id);

            // Relación muchos a uno
            builder.HasOne(p => p.cliente).WithMany(p => p.contacto).HasForeignKey(p => p.cliente_id).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
