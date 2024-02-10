using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFit.Data.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValue: "Test"),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValue: "Test"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoachCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CoachCategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coaches_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coaches_CoachCategories_CoachCategoryId",
                        column: x => x.CoachCategoryId,
                        principalTable: "CoachCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    CoachId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventCategoryId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_EventCategories_EventCategoryId",
                        column: x => x.EventCategoryId,
                        principalTable: "EventCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventClients",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventClients", x => new { x.ClientId, x.EventId });
                    table.ForeignKey(
                        name: "FK_EventClients_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventClients_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("0a7141a1-62c8-4a1f-9225-1d77f76412d1"), 0, "3126e7a6-a4cc-4433-b6ce-1e52f6aa47ea", "sami@coach.com", true, "Sami", "Hosni", false, null, "SAMI@COACH.COM", "SAMI@COACH.COM", "AQAAAAEAACcQAAAAEK/RAYa6evbIhLVitkLGMG1dwASATbFzp2kLlzikIG0ecKbN9ToCnrmcHoltGDWq6w==", null, false, "f5738f4e-c5bd-44b2-9234-87aba4891a52", false, "sami@coach.com" },
                    { new Guid("0bee301e-6e95-41ae-aa91-e8dc87112eea"), 0, "504b4b8e-770e-4cfc-a695-cac16a690499", "admin@befit.bg", true, "Admin", "Admin", false, null, "ADMIN@BEFIT.BG", "ADMIN@BEFIT.BG", "AQAAAAEAACcQAAAAENYCtM+6ooSEgeKWIB1Mi7rGJvbpqlgVFsxi/u+PIv3r1Fqg4PpJRdnieyo+UZzm4w==", null, false, "3c8597c2-c491-4c71-8895-ca8ff5f6d25d", false, "admin@befit.bg" },
                    { new Guid("283c422e-8e6e-450b-818e-65d8d4c9426c"), 0, "5d9bb0dd-fd77-4410-b59e-88752db70867", "bentley@user.com", true, "Bentley", "Ivanov", false, null, "BENTLEY@USER.COM", "BENTLEY@USER.COM", "AQAAAAEAACcQAAAAEK/RAYa6evbIhLVitkLGMG1dwASATbFzp2kLlzikIG0ecKbN9ToCnrmcHoltGDWq6w==", null, false, "86002846-7ecb-4034-9dce-963e10002c88", false, "bentley@user.com" },
                    { new Guid("40ab26f0-ce65-4276-8bf9-4ce80bbf256a"), 0, "318a8581-71c6-4bc4-8d8f-61459e946cc5", "quan@user.com", true, "Quan", "Rodriguez", false, null, "QUAN@USER.COM", "QUAN@USER.COM", "AQAAAAEAACcQAAAAEK/RAYa6evbIhLVitkLGMG1dwASATbFzp2kLlzikIG0ecKbN9ToCnrmcHoltGDWq6w==", null, false, "f4836823-07e9-4cd3-aad7-654992ef7c28", false, "quan@user.com" },
                    { new Guid("7ca25b19-34e1-4b20-b3e9-aa98e43bf574"), 0, "cc6ca61d-0372-491f-b8f2-b50404aaf552", "lenlen@user.com", true, "Leonardo", "Dicaprio", false, null, "LENLEN@USER.COM", "LENLEN@USER.COM", "AQAAAAEAACcQAAAAEK/RAYa6evbIhLVitkLGMG1dwASATbFzp2kLlzikIG0ecKbN9ToCnrmcHoltGDWq6w==", null, false, "41018a16-6966-449c-8616-3850c774c020", false, "lenlen@user.com" },
                    { new Guid("f4f678ce-62d4-4dde-97cf-e1de3f4e7482"), 0, "47aa19c2-5c5c-4030-80f5-23bc42c92dce", "lenyg@coach.com", true, "Elena", "Georgieva", false, null, "LENYG@COACH.COM", "LENYG@COACH.COM", "AQAAAAEAACcQAAAAEK/RAYa6evbIhLVitkLGMG1dwASATbFzp2kLlzikIG0ecKbN9ToCnrmcHoltGDWq6w==", null, false, "6823fac7-e949-454c-9186-526180a38e4a", false, "lenyg@coach.com" }
                });

            migrationBuilder.InsertData(
                table: "CoachCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fitness trainer" },
                    { 2, "Lifestyle coach" },
                    { 3, "Sports coach" },
                    { 4, "Personal trainer" },
                    { 5, "Athletic trainer" },
                    { 6, "Wellness specialist" },
                    { 7, "Health coach" },
                    { 8, "Exercise specialist" },
                    { 9, "Bodybuilding coach" }
                });

            migrationBuilder.InsertData(
                table: "EventCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Physical" },
                    { 2, "Mind" },
                    { 3, "Motorized" },
                    { 4, "Coordination" },
                    { 5, "Animal-supported" }
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "Age", "CoachCategoryId", "Description", "Gender", "Height", "PhoneNumber", "UserId", "Weight" },
                values: new object[] { new Guid("48d78e23-9007-4871-8d89-b8acb2c7e2e8"), 30, 5, "Admin admin admin", "Other", 1.8, "0886810123", new Guid("0bee301e-6e95-41ae-aa91-e8dc87112eea"), 85.0 });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "Age", "CoachCategoryId", "Description", "Gender", "Height", "PhoneNumber", "UserId", "Weight" },
                values: new object[] { new Guid("66f0823e-09a4-4858-8f47-a8e096c859b9"), 21, 5, "Very serious and hardworking coach!", "Female", 1.6499999999999999, "0886810542", new Guid("f4f678ce-62d4-4dde-97cf-e1de3f4e7482"), 61.0 });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "Age", "CoachCategoryId", "Description", "Gender", "Height", "PhoneNumber", "UserId", "Weight" },
                values: new object[] { new Guid("b2d36361-646f-496a-a5a7-26b9ed2a1a33"), 27, 2, "Very serious and hardworking coach!", "Male", 1.8999999999999999, "0886810378", new Guid("0a7141a1-62c8-4a1f-9225-1d77f76412d1"), 100.0 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Address", "ApplicationUserId", "CoachId", "CreatedOn", "Description", "End", "EventCategoryId", "ImageUrl", "IsActive", "Start", "Tax", "Title" },
                values: new object[,]
                {
                    { new Guid("08cd5700-99cb-4843-b5c4-f70df2ca4deb"), "Vasil Levski 118", null, new Guid("b2d36361-646f-496a-a5a7-26b9ed2a1a33"), new DateTime(2023, 8, 6, 1, 6, 22, 330, DateTimeKind.Unspecified), "Best car racing event ever!", new DateTime(2028, 8, 17, 1, 6, 0, 0, DateTimeKind.Unspecified), 3, "https://di-uploads-pod23.dealerinspire.com/lexusoflasvegas/uploads/2021/08/Lexus-Racing-Lime-Rock.jpg", true, new DateTime(2023, 8, 6, 1, 6, 0, 0, DateTimeKind.Unspecified), 50.0, "Car racing" },
                    { new Guid("110e4a17-9885-457b-b755-6bfcf69ccc17"), "Geo Milev 100", null, new Guid("66f0823e-09a4-4858-8f47-a8e096c859b9"), new DateTime(2023, 8, 6, 1, 6, 22, 330, DateTimeKind.Unspecified), "Best billiards event ever!", new DateTime(2031, 8, 17, 1, 6, 0, 0, DateTimeKind.Unspecified), 4, "https://i.ebayimg.com/images/g/B6QAAOSwTLBjBSwy/s-l1200.jpg", true, new DateTime(2023, 8, 6, 1, 6, 0, 0, DateTimeKind.Unspecified), 15.199999999999999, "Billiards" },
                    { new Guid("127cb9cf-490d-4baa-834a-f0bb5f9304f9"), "Kokiche 21", null, new Guid("b2d36361-646f-496a-a5a7-26b9ed2a1a33"), new DateTime(2023, 8, 6, 1, 6, 22, 330, DateTimeKind.Unspecified), "Best board game event ever!", new DateTime(2030, 8, 17, 1, 6, 0, 0, DateTimeKind.Unspecified), 2, "https://perfectescaperoom.com/wp-content/uploads/2020/09/Untitled-design-62.png", true, new DateTime(2023, 8, 6, 1, 6, 0, 0, DateTimeKind.Unspecified), 28.300000000000001, "Board game" },
                    { new Guid("3a2c6e63-e82f-4fc9-94da-9309d90b2578"), "Stoyan Zaimov 35", null, new Guid("66f0823e-09a4-4858-8f47-a8e096c859b9"), new DateTime(2023, 8, 6, 1, 6, 22, 330, DateTimeKind.Unspecified), "Best powerboating event ever!", new DateTime(2029, 8, 17, 1, 6, 0, 0, DateTimeKind.Unspecified), 3, "https://i0.wp.com/397566-www.web.tornado-node.net/wp-content/uploads/2021/11/RAF1074-scaled.jpg?resize=1080%2C720&ssl=1", true, new DateTime(2023, 8, 6, 1, 6, 0, 0, DateTimeKind.Unspecified), 45.600000000000001, "Powerboating" },
                    { new Guid("686e191d-946a-4542-ae8f-b9c82c59b054"), "Ivan Milev 1", null, new Guid("b2d36361-646f-496a-a5a7-26b9ed2a1a33"), new DateTime(2023, 8, 6, 1, 6, 22, 330, DateTimeKind.Unspecified), "Best equestrian event ever!", new DateTime(2027, 8, 17, 1, 6, 0, 0, DateTimeKind.Unspecified), 5, "https://www.cavalletti.com.au/wp-content/uploads/2022/11/FB_IMG_1667257848544.jpg", true, new DateTime(2023, 8, 6, 1, 6, 0, 0, DateTimeKind.Unspecified), 22.300000000000001, "Equestrian" },
                    { new Guid("73482205-eb8f-49a8-b622-1127f292c3ec"), "Ivan Vazov 51", null, new Guid("48d78e23-9007-4871-8d89-b8acb2c7e2e8"), new DateTime(2023, 8, 6, 1, 6, 22, 330, DateTimeKind.Unspecified), "Best rugby event ever!", new DateTime(2026, 8, 17, 1, 6, 0, 0, DateTimeKind.Unspecified), 1, "https://www.barnstaplerugby.co.uk/wp-content/uploads/2021/10/chiefs-v-westcliff101.jpg", true, new DateTime(2023, 8, 6, 1, 6, 0, 0, DateTimeKind.Unspecified), 40.0, "Rugby" },
                    { new Guid("a166347c-7050-468a-b046-0bbc681d74ae"), "Petko Ivanov 18", null, new Guid("b2d36361-646f-496a-a5a7-26b9ed2a1a33"), new DateTime(2023, 8, 6, 1, 6, 22, 330, DateTimeKind.Unspecified), "Best rodeo event ever!", new DateTime(2026, 8, 17, 1, 6, 0, 0, DateTimeKind.Unspecified), 5, "https://www.rubysinn.com/wp-content/uploads/2014/11/things-to-do-in-bryce-canyon-rodeo.jpg", true, new DateTime(2023, 8, 6, 1, 6, 0, 0, DateTimeKind.Unspecified), 35.75, "Rodeo" },
                    { new Guid("c51963d9-8074-423f-bcc6-15b60a35cecf"), "Hristo Botev 15", null, new Guid("66f0823e-09a4-4858-8f47-a8e096c859b9"), new DateTime(2023, 8, 6, 1, 6, 22, 330, DateTimeKind.Unspecified), "Best athletics event ever!", new DateTime(2027, 8, 17, 1, 6, 0, 0, DateTimeKind.Unspecified), 1, "https://images.unsplash.com/photo-1532444458054-01a7dd3e9fca?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8YXRobGV0aWN8ZW58MHx8MHx8fDA%3D", true, new DateTime(2023, 8, 6, 1, 6, 0, 0, DateTimeKind.Unspecified), 15.5, "Athletics" },
                    { new Guid("d2aaedfe-e4b4-4897-ba75-4cef430b8b93"), "Petko Karavelov 16", null, new Guid("66f0823e-09a4-4858-8f47-a8e096c859b9"), new DateTime(2023, 8, 6, 1, 6, 22, 330, DateTimeKind.Unspecified), "Best tennis event ever!", new DateTime(2028, 8, 17, 1, 6, 0, 0, DateTimeKind.Unspecified), 4, "https://tennisalberta.com/wp-content/uploads/2021/11/Presentation-Lifestyle_Penn.jpg", true, new DateTime(2023, 8, 6, 1, 6, 0, 0, DateTimeKind.Unspecified), 29.5, "Tennis" },
                    { new Guid("e2c4742f-7b1b-4e43-b574-af3fca09697c"), "Hristo Smirnenski 63", null, new Guid("b2d36361-646f-496a-a5a7-26b9ed2a1a33"), new DateTime(2023, 8, 6, 1, 6, 22, 330, DateTimeKind.Unspecified), "Best chess event ever!", new DateTime(2025, 8, 17, 1, 6, 0, 0, DateTimeKind.Unspecified), 2, "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6f/ChessSet.jpg/800px-ChessSet.jpg", true, new DateTime(2023, 8, 6, 1, 6, 0, 0, DateTimeKind.Unspecified), 31.199999999999999, "Chess" }
                });

            migrationBuilder.InsertData(
                table: "EventClients",
                columns: new[] { "ClientId", "EventId" },
                values: new object[,]
                {
                    { new Guid("0bee301e-6e95-41ae-aa91-e8dc87112eea"), new Guid("e2c4742f-7b1b-4e43-b574-af3fca09697c") },
                    { new Guid("283c422e-8e6e-450b-818e-65d8d4c9426c"), new Guid("e2c4742f-7b1b-4e43-b574-af3fca09697c") },
                    { new Guid("40ab26f0-ce65-4276-8bf9-4ce80bbf256a"), new Guid("e2c4742f-7b1b-4e43-b574-af3fca09697c") },
                    { new Guid("7ca25b19-34e1-4b20-b3e9-aa98e43bf574"), new Guid("110e4a17-9885-457b-b755-6bfcf69ccc17") },
                    { new Guid("7ca25b19-34e1-4b20-b3e9-aa98e43bf574"), new Guid("a166347c-7050-468a-b046-0bbc681d74ae") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_CoachCategoryId",
                table: "Coaches",
                column: "CoachCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_UserId",
                table: "Coaches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventClients_EventId",
                table: "EventClients",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ApplicationUserId",
                table: "Events",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CoachId",
                table: "Events",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventCategoryId",
                table: "Events",
                column: "EventCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EventClients");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "EventCategories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CoachCategories");
        }
    }
}
