#pragma warning disable CS8981
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HKNGroup.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class hkngroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryFA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    siteBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    LinkPage = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    MenuRow = table.Column<int>(type: "int", nullable: true),
                    AutomaticListing = table.Column<int>(type: "int", nullable: true),
                    MenuSeo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YanSekme = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OurServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    siteBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoTags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageNumber = table.Column<int>(type: "int", nullable: false),
                    IsMainPage = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListingImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    siteBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEOTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEOKeywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEODescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PopupMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    MessageUrl = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    siteBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowNumbar = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopupMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CategoryNumber = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    siteBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEOTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEOKeywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEODescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Referances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    siteBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowNumbar = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteDefaultTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteKeyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteMailPass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteOwner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwitterSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GooglePlusSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YoutubeSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasImage1 = table.Column<bool>(type: "bit", nullable: false),
                    HasImage2 = table.Column<bool>(type: "bit", nullable: false),
                    SiteAdress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteDefaultLongTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactAdress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IFrame = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mainTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetkili1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetkili2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetkili1Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Yetkili2Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteSlogan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Description2 = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    siteBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowNumbar = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShortContent = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    siteBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    SeoTags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeoDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Headline = table.Column<bool>(type: "bit", nullable: true),
                    HeadlineStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HeadlineEndingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ArticleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShortContent = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    siteBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewsCount = table.Column<int>(type: "int", nullable: true),
                    SeoTags = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    SeoDescription = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    ProjectCategoryId = table.Column<int>(type: "int", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectCategories_ProjectCategoryId",
                        column: x => x.ProjectCategoryId,
                        principalTable: "ProjectCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleComments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    siteBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectImages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "CreatedTime", "Email", "IsActive", "IsDeleted", "ModifiedTime", "Password", "SiteId", "UserName" },
                values: new object[] { 1, new DateTime(2024, 4, 30, 16, 20, 37, 182, DateTimeKind.Local).AddTicks(3308), "hakancali@hkngrup.com", true, false, new DateTime(2024, 4, 30, 16, 20, 37, 182, DateTimeKind.Local).AddTicks(3309), "toyota2023", 0, "toyotaparcam2023" });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "CreatedTime", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "ModifiedTime", "SiteId", "Subject", "Text" },
                values: new object[] { 1, new DateTime(2024, 4, 30, 16, 20, 37, 184, DateTimeKind.Local).AddTicks(1733), "deneme@mail.com", "Mehmet", true, false, "Şevket", new DateTime(2024, 4, 30, 16, 20, 37, 184, DateTimeKind.Local).AddTicks(1733), 0, "deneme", "Bu bir deneme mesajıdır. Dikkate almayınız!" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Pages",
                columns: new[] { "Id", "BackgroundImage", "CreatedTime", "Description", "IsActive", "ListingImage", "ModifiedTime", "PageNumber", "PageTitle", "SEODescription", "SEOKeywords", "SEOTitle", "SiteId", "Text1", "Text2", "Url", "siteBaseUrl" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2024, 4, 30, 16, 20, 37, 184, DateTimeKind.Local).AddTicks(8142), "<p> yılında kurulan Hema İnşaat Taahhüt ve Ticaret Limited Şirketi olarak büyük projelere imza atıyoruz ve sizlere en iyi hizmeti sağlamak için çalışıyoruz. İlk günkü heyecanımız ve iş aşkımızla, sevdiklerinizle birlikte huzur içinde yaşayıp, mutlu anılar biriktirebilmeniz için anahtar teslim projeler yapıyoruz ve sizler için güvenli yapılar inşa ediyoruz. </p><p>35 yıllık hikayemizde yaşamlarına dokunduğumuz tüm mutlu aileler, başarımızın teminatıdır. Hema İnşaat olarak sizlere hizmet vermekten gurur duyuyoruz<br></p>", true, "", new DateTime(2024, 4, 30, 16, 20, 37, 184, DateTimeKind.Local).AddTicks(8143), 1, "Kurumsal", "", "", "", 0, "Bir Farkımız da Yörüngemiz", "", "kurumsal", null },
                    { 2, "", new DateTime(2024, 4, 30, 16, 20, 37, 184, DateTimeKind.Local).AddTicks(8234), " <div class=\"info-media\">\r\n <div class=\"info-media_icon\">\r\n <i class=\"far fa-lightbulb-on\"></i>\r\n </div>\r\n <div class=\"media-body\">\r\n  <h3 class=\"h6 info-media_title\">Mimari</h3>\r\n  <p class=\"info-media_text\">Dynamically extend 24/365 human capital whereas premium opportunities. Continually evisculate.</p>\r\n </div>\r\n </div>\r\n <div class=\"info-media\">\r\n <div class=\"info-media_icon\">\r\n <i class=\"far fa-circle-dollar\"></i>\r\n </div>\r\n <div class=\"media-body\">\r\n  <h3 class=\"h6 info-media_title\">Tasarım</h3>\r\n  <p class=\"info-media_text\">Collaboratively customize efficient initiatives whereas cooperative synergy. Credibly promote ethical.</p>\r\n </div>\r\n </div>\r\n <div class=\"info-media mb-0\">\r\n <div class=\"info-media_icon\">\r\n<i class=\"far fa-headset\"></i>\r\n </div>\r\n <div class=\"media-body\">\r\n  <h3 class=\"h6 info-media_title\">Planlama</h3>\r\n  <p class=\"info-media_text\">Seamlessly evisculate B2C supply chains rather than efficient architectures. Rapidiously.</p>\r\n </div>\r\n </div>", true, "", new DateTime(2024, 4, 30, 16, 20, 37, 184, DateTimeKind.Local).AddTicks(8235), 2, "Neden Biz", "", "", "", 0, "Gelecek nesiller için sürdürülebilir yapılar inşa ediyoruz.", "Kent mimarisine estetik ve yaşama değer katan projeler üreten Şua İnşaat; köklü geçmişi, güçlü finansal yapısı, yüksek kalite anlayışı, yenilikçi mimari tarzı ve doğaya dost yaklaşımı ile bugüne kadar binlerce konut ve ticari üniteye imza attı.", "neden-biz", null },
                    { 3, "", new DateTime(2024, 4, 30, 16, 20, 37, 184, DateTimeKind.Local).AddTicks(8265), "<p>Firmamız geniş ekipman parkı ve sektöründe uzmanlaşmış deneyimli personeli ile her türlü taahhüdü yerine getirebilecek donanıma sahiptir.&nbsp;</p><p>Deneyimli ve çağın gerektirdiği yönetim anlayışı ile sorunlarınıza duyarlı, anında çözüm üreten teknik personelimizle müşteri memnuniyetini ön planda tutarak, güven duygusu ile hizmet üretmeye devam etmektedir.&nbsp;</p><p>Deneyimli ve çağın gerektirdiği yönetim anlayışı ile sorunlarınıza duyarlı, anında çözüm üreten teknik personelimizle müşteri memnuniyetini ön planda tutarak, güven duygusu ile hizmet üretmeye devam etmektedir.&nbsp;<br></p>", true, "", new DateTime(2024, 4, 30, 16, 20, 37, 184, DateTimeKind.Local).AddTicks(8265), 3, "Hakkımızda", "", "", "", 0, "Hakan İnşaat", "Kente estetik, yaşama değer katar.", "hakkimizda", null },
                    { 4, "", new DateTime(2024, 4, 30, 16, 20, 37, 184, DateTimeKind.Local).AddTicks(8307), " <div class=\"title-area mb-30\"> \r\n  <h2 class=\"sec-title\">Misyon</h2>\r\n </div>\r\n <p class=\"mt-n2 mb-4\">\r\n  Holisticly whiteboard user-centric e-business before professional deliverables. Globally re-engineer cutting-edge information.\r\n  </p>\r\n \r\n <div class=\"about-grid-wrap\">\r\n </div>\r\n\r\n <div class=\"title-area mb-30\">\r\n  <h2 class=\"sec-title\">Vizyon</h2>\r\n </div>\r\n <p class=\"mt-n2 mb-4\">\r\n  Holisticly whiteboard user-centric e-business before professional deliverables. Globally re-engineer cutting-edge information.\r\n </p>", true, "", new DateTime(2024, 4, 30, 16, 20, 37, 184, DateTimeKind.Local).AddTicks(8307), 4, "Vizyon & Misyon", "", "", "", 0, "Vizyon & Misyon", "..", "vizyon-misyon", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "SiteSettings",
                columns: new[] { "Id", "ContactAdress", "ContactNumber", "CreatedTime", "FacebookSite", "GooglePlusSite", "HasImage1", "HasImage2", "IFrame", "Image1", "Image2", "InstagramSite", "IsActive", "IsDeleted", "Logo", "MailServer", "Maps", "ModifiedTime", "SiteAdress", "SiteDefaultLongTitle", "SiteDefaultTitle", "SiteDescription", "SiteId", "SiteKeyword", "SiteMail", "SiteMailPass", "SiteOwner", "SiteSlogan", "TwitterSite", "Video1", "Video2", "WorkingTime", "Yetkili1", "Yetkili1Tel", "Yetkili2", "Yetkili2Tel", "YoutubeSite", "mainTitle" },
                values: new object[] { 1, "demo", "demo", new DateTime(2024, 4, 30, 16, 20, 37, 186, DateTimeKind.Local).AddTicks(1872), "demo.com", "demo.com", false, false, "demo", "demo", "demo", "demo.com", true, false, "demo", "demo", "demo", new DateTime(2024, 4, 30, 16, 20, 37, 186, DateTimeKind.Local).AddTicks(1873), "demo.com", "demo", "demo", "demo", 0, "demo", "demo@demo.com", "1234", "demo", null, "demo.com", "demo", "demo", "0", "demo", "5556667788", "demo", "4446665554", "demo.com", "demo" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComments_ArticleId",
                table: "ArticleComments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_ProjectId",
                table: "ProjectImages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectCategoryId",
                table: "Projects",
                column: "ProjectCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "ArticleComments");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "OurServices");

            migrationBuilder.DropTable(
                name: "Pages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PopupMessages");

            migrationBuilder.DropTable(
                name: "ProjectImages");

            migrationBuilder.DropTable(
                name: "Referances");

            migrationBuilder.DropTable(
                name: "SiteSettings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "ArticleCategories");

            migrationBuilder.DropTable(
                name: "ProjectCategories");
        }
    }
}
