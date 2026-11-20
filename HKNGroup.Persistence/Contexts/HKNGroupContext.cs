using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace HKNGroup.Persistence.Contexts
{
    public class HKNGroupContext : DbContext
    {
        public HKNGroupContext(DbContextOptions<HKNGroupContext> options) : base(options)
        {
        }

        public HKNGroupContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = GetConnectionStringFromAppSettings();
                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }
        }

        private static string? GetConnectionStringFromAppSettings()
        {
            var baseDir = AppContext.BaseDirectory;
            var currentDir = Directory.GetCurrentDirectory();

            var candidateDirs = new[]
            {
                currentDir,
                Path.Combine(currentDir, "HKNGroup.MvcUI"),
                Path.Combine(currentDir, "..", "HKNGroup.MvcUI"),
                baseDir,
                Path.Combine(baseDir, "..", "..", "..", "..", "HKNGroup.MvcUI")
            };

            foreach (var dir in candidateDirs)
            {
                try
                {
                    var fullPath = Path.GetFullPath(dir);
                    var appsettingsPath = Path.Combine(fullPath, "appsettings.json");
                    if (File.Exists(appsettingsPath))
                    {
                        var json = File.ReadAllText(appsettingsPath);
                        using var doc = JsonDocument.Parse(json);
                        if (doc.RootElement.TryGetProperty("ConnectionStrings", out var connStrings) &&
                            connStrings.TryGetProperty("DefaultConnection", out var connProp))
                        {
                            return connProp.GetString();
                        }
                    }
                }
                catch
                {
                    // Ignore search errors and check next candidate
                }
            }

            return null;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Assembly assemblyConfiguration = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyConfiguration);
        }

        public virtual DbSet<Admin> Admins => Set<Admin>();
        public virtual DbSet<Menus> Menus => Set<Menus>();
        public virtual DbSet<OurService> OurServices => Set<OurService>();
        public virtual DbSet<Article> Article => Set<Article>();
        public virtual DbSet<ArticleComment> ArticleComments => Set<ArticleComment>();
        public virtual DbSet<ArticleCategory> ArticleCategories => Set<ArticleCategory>();
        public virtual DbSet<Slider> Sliders => Set<Slider>();
        public virtual DbSet<Referance> Referances => Set<Referance>();
        public virtual DbSet<PopupMessages> PopupMessages => Set<PopupMessages>();
        public virtual DbSet<Message> Messages => Set<Message>();
        public virtual DbSet<SiteSetting> SiteSettings => Set<SiteSetting>();
        public virtual DbSet<Page> Pages => Set<Page>();
        public virtual DbSet<Project> Project => Set<Project>();
        public virtual DbSet<ProjectCategory> ProjectCategory => Set<ProjectCategory>();
        public virtual DbSet<ProjectImages> ProjectImages => Set<ProjectImages>();
    }
}
