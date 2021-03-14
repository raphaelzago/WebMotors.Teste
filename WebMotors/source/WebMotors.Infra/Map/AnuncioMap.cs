using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMotors.Core.Entidades;

namespace WebMotors.Infra.Map
{
    public class AnuncioMap : IEntityTypeConfiguration<Anuncio>
    {
        public void Configure(EntityTypeBuilder<Anuncio> builder)
        {
            builder.ToTable("tb_AnuncioWebmotors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
            builder.Property(x => x.Marca).HasColumnName("marca").HasColumnType("varchar(45)").IsRequired();
            builder.Property(x => x.Modelo).HasColumnName("modelo").HasColumnType("varchar(45)").IsRequired();
            builder.Property(x => x.Versao).HasColumnName("versao").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Ano).HasColumnName("ano").IsRequired();
            builder.Property(x => x.Quilometragem).HasColumnName("quilometragem").HasColumnType("int").IsRequired();
            builder.Property(x => x.Observacao).HasColumnName("observacao").HasColumnType("text").IsRequired();
            builder.Ignore(x => x.MarcaId);
            builder.Ignore(x => x.ModeloId);
            builder.Ignore(x => x.VersaoId);
        }
    }
}
