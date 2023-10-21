using Estacionamento.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.API.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Vaga>().ToTable("VAGA");

            builder.Entity<Vaga>().HasKey(va => va.Id);

            builder.Entity<Vaga>()
                .Property(va => va.Id)
                .HasColumnName("id");

            builder.Entity<Vaga>()
                .Property(va => va.Quantidade)
                .HasColumnName("quantidade");

            builder.Entity<Vaga>()
                .Property(va => va.TipoVaga)
                .HasColumnName("tipo_vaga");

            builder.Entity<Vaga>(entity => {
                entity.HasIndex(e => e.TipoVaga).IsUnique();
            });

            builder.Entity<Vaga>()
               .Property(va => va.CreatedAt)
               .HasColumnName("created_at");

            builder.Entity<Vaga>()
               .Property(va => va.UpdatedAt)
               .HasColumnName("updated_at");

            builder.Entity<Vaga>()
                .HasMany(va => va.Veiculos)
                .WithOne(ve => ve.Vaga)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(ve => ve.Id);


            builder.Entity<Veiculo>().ToTable("VEICULO");

            builder.Entity<Veiculo>().HasKey(ve => ve.Id);

            builder.Entity<Veiculo>()
                .Property(ve => ve.Id)
                .HasColumnName("id");

            builder.Entity<Veiculo>()
                .Property(ve => ve.Placa)
                .HasColumnName("placa");

            builder.Entity<Veiculo>()
                .Property(ve => ve.IdVaga)
                .HasColumnName("id_vaga");

            builder.Entity<Veiculo>()
                .Property(ve => ve.Entrada)
                .HasColumnName("entrada");

            builder.Entity<Veiculo>()
                .Property(ve => ve.Saida)
                .HasColumnName("saida");

            builder.Entity<Veiculo>()
                .Property(ve => ve.TipoVeiculo)
                .HasColumnName("tipo_veiculo");

            builder.Entity<Veiculo>()
              .Property(ve => ve.CreatedAt)
              .HasColumnName("created_at");

            builder.Entity<Veiculo>()
               .Property(ve => ve.UpdatedAt)
               .HasColumnName("updated_at");

            builder.Entity<Veiculo>()
               .HasOne(ve => ve.Vaga)
               .WithMany(va => va.Veiculos)
               .HasForeignKey(ve => ve.IdVaga);
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSucess,
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                DateTime dateTime = DateTime.Now; 

                if (entityEntry.State == EntityState.Added)
                    ((BaseEntity)entityEntry.Entity).CreatedAt = dateTime;
            }

            return base.SaveChangesAsync(acceptAllChangesOnSucess, cancellationToken);
        }

        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; } 
    }
}
