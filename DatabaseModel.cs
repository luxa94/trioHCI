using System.Data.Entity.ModelConfiguration.Conventions;

namespace HCI
{
    using Model;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class DatabaseModel : DbContext
    {
        // Your context has been configured to use a 'DatabaseModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'HCI.DatabaseModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DatabaseModel' 
        // connection string in the application configuration file.
        public DatabaseModel()
            : base("name=DatabaseModel")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseModel>()); // ovo odkomentarisati da ne bi brisao podatke iz baze!
//            Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseModel>()); // ovo je kada se prvi put pokrece!!!!

            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Premises>()
                .HasRequired<Model.Type>(p => p.Type)
                .WithMany(t => t.Premises)
                .HasForeignKey(p => p.TypeId);

            modelBuilder.Entity<Premises>()
                .HasMany<Tag>(p => p.Tags)
                .WithMany(t => t.Premises)
                .Map(pt =>
                {
                    pt.MapLeftKey("PremisesId");
                    pt.MapRightKey("TagId");
                    pt.ToTable("PremisesTag");
                });
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Premises> Premises { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Model.Type> Types { get; set; }
    }


    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}