using System.Data.Entity;
using Seds.PMAS.Infra.Data.EntityConfig;
using Seds.PMAS.Dominio.Entities;


namespace Seds.PMAS.Infra.Data.Context
{
    public partial class DBPMASContext : DbContext
    {
        static DBPMASContext()
        {
            Database.SetInitializer<DBPMASContext>(null);
        }


        public DBPMASContext()
            : base("Name=DBPMASContext")
        {
            base.Configuration.LazyLoadingEnabled = true;
        }


        public DbSet<PrefeitoEntity> Prefeitos { get; set; }
        public DbSet<PrefeituraEntity> Prefeituras { get; set; }
        public DbSet<RecursoEntity> Recursos { get; set; }
        public DbSet<RecursoPerfilEntity> RecursosPerfis { get; set; }
        public DbSet<SituacaoEntity> Situacoes { get; set; }
        public DbSet<NivelGestaoEntity> NiveisGestao { get; set; }
        public DbSet<StatusEntity> Status { get; set; }
        public DbSet<UsuarioEntity> Usuarios { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public virtual void Dispose()
        {
            base.Dispose();
        }

        public virtual void OpenConnection()
        {
            if (Database.Connection != null && Database.Connection.State == System.Data.ConnectionState.Closed)
                Database.Connection.Open();
        }

        public virtual void CloseConnection()
        {
            if (Database.Connection != null && Database.Connection.State == System.Data.ConnectionState.Open)
                Database.Connection.Close();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RecursoMap());
            modelBuilder.Configurations.Add(new RecursoPerfilMap());
            modelBuilder.Configurations.Add(new PrefeitoMap());
            modelBuilder.Configurations.Add(new PrefeituraMap());
            modelBuilder.Configurations.Add(new NivelGestaoMap());
            modelBuilder.Configurations.Add(new SituacaoMap());
            modelBuilder.Configurations.Add(new UsuarioMap());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));
        }
    }
}
