using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EuroLeaguesScore.Data.Migrations
{
    /// <inheritdoc />
    public partial class RebootOfDbBecauseOfApplicationUser : Migration
    {
        /// <inheritdoc />
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
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
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
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false),
                    City = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    Draws = table.Column<int>(type: "int", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Goals = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTeams",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeamId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeams", x => new { x.UserId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_UserTeams_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTeams_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeams_Teams_TeamId1",
                        column: x => x.TeamId1,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPlayers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlayerId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlayers", x => new { x.UserId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_UserPlayers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPlayers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPlayers_Players_PlayerId1",
                        column: x => x.PlayerId1,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Premier League" },
                    { 2, "La Liga" },
                    { 3, "Bundesliga" },
                    { 4, "Serie A" },
                    { 5, "Ligue 1" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Age", "Name", "TeamId" },
                values: new object[,]
                {
                    { 1, 55, "Pep Guardiola", null },
                    { 2, 44, "Mikel Arteta", null },
                    { 3, 47, "Arne Slot", null },
                    { 4, 44, "Michael Carrick", null },
                    { 5, 41, "Liam Rosenior", null },
                    { 6, 43, "Álvaro Arbeloa", null },
                    { 7, 61, "Hansi Flick", null },
                    { 8, 54, "Diego Simeone", null },
                    { 9, 59, "Quique Sanchez Flores", null },
                    { 10, 52, "Imanol Alguacil", null },
                    { 11, 40, "Vincent Kompany", null },
                    { 12, 54, "Niko Kovač", null },
                    { 13, 49, "Marco Rose", null },
                    { 14, 54, "Kasper Hjulmand", null },
                    { 15, 61, "Dieter Hecking", null },
                    { 16, 67, "Luciano Spalletti", null },
                    { 17, 58, "Massimiliano Allegri", null },
                    { 18, 62, "Guillermo Hoyos", null },
                    { 19, 56, "Antonio Conte", null },
                    { 20, 68, "Gian Piero Gasperini", null },
                    { 21, 54, "Luis Enrique", null },
                    { 22, 48, "Habib Beye", null },
                    { 23, 53, "Paulo Fonseca", null },
                    { 24, 38, "Sébastien Pocognoli", null },
                    { 25, 59, "Bruno Génésio", null }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "City", "Country", "Draws", "LeagueId", "Losses", "ManagerId", "Name", "Wins" },
                values: new object[,]
                {
                    { 1, "Manchester", "England", 0, 1, 0, 1, "Manchester City", 0 },
                    { 2, "London", "England", 0, 1, 0, 2, "Arsenal", 0 },
                    { 3, "Liverpool", "England", 0, 1, 0, 3, "Liverpool", 0 },
                    { 4, "Manchester", "England", 0, 1, 0, 4, "Manchester United", 0 },
                    { 5, "London", "England", 0, 1, 0, 5, "Chelsea", 0 },
                    { 6, "Madrid", "Spain", 0, 2, 0, 6, "Real Madrid", 0 },
                    { 7, "Barcelona", "Spain", 0, 2, 0, 7, "Barcelona", 0 },
                    { 8, "Madrid", "Spain", 0, 2, 0, 8, "Atletico Madrid", 0 },
                    { 9, "Seville", "Spain", 0, 2, 0, 9, "Sevilla", 0 },
                    { 10, "San Sebastian", "Spain", 0, 2, 0, 10, "Real Sociedad", 0 },
                    { 11, "Munich", "Germany", 0, 3, 0, 11, "Bayern Munich", 0 },
                    { 12, "Dortmund", "Germany", 0, 3, 0, 12, "Borussia Dortmund", 0 },
                    { 13, "Leipzig", "Germany", 0, 3, 0, 13, "RB Leipzig", 0 },
                    { 14, "Leverkusen", "Germany", 0, 3, 0, 14, "Bayer Leverkusen", 0 },
                    { 15, "Wolfsburg", "Germany", 0, 3, 0, 15, "Wolfsburg", 0 },
                    { 16, "Turin", "Italy", 0, 4, 0, 16, "Juventus", 0 },
                    { 17, "Milan", "Italy", 0, 4, 0, 17, "AC Milan", 0 },
                    { 18, "Milan", "Italy", 0, 4, 0, 18, "Inter Milan", 0 },
                    { 19, "Naples", "Italy", 0, 4, 0, 19, "Napoli", 0 },
                    { 20, "Rome", "Italy", 0, 4, 0, 20, "Roma", 0 },
                    { 21, "Paris", "France", 0, 5, 0, 21, "Paris Saint-Germain", 0 },
                    { 22, "Marseille", "France", 0, 5, 0, 22, "Marseille", 0 },
                    { 23, "Lyon", "France", 0, 5, 0, 23, "Lyon", 0 },
                    { 24, "Monaco", "Monaco", 0, 5, 0, 24, "Monaco", 0 },
                    { 25, "Lille", "France", 0, 5, 0, 25, "Lille", 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "Assists", "Goals", "Name", "Position", "TeamId" },
                values: new object[,]
                {
                    { 1, 24, 0, 0, "Erling Haaland", 4, 1 },
                    { 2, 21, 0, 0, "Ryan Cherki", 3, 1 },
                    { 3, 25, 0, 0, "Phil Foden", 3, 1 },
                    { 4, 24, 0, 0, "Bukayo Saka", 4, 2 },
                    { 5, 27, 0, 0, "Martin Odegaard", 3, 2 },
                    { 6, 29, 0, 0, "Gabriel Jesus", 4, 2 },
                    { 7, 33, 0, 0, "Mohamed Salah", 4, 3 },
                    { 8, 23, 0, 0, "Hugo Ekitike", 4, 3 },
                    { 9, 33, 0, 0, "Virgil van Dijk", 2, 3 },
                    { 10, 23, 0, 0, "Jude Bellingham", 3, 6 },
                    { 11, 25, 0, 0, "Vinicius Jr", 4, 6 },
                    { 12, 25, 0, 0, "Rodrygo", 4, 6 },
                    { 13, 37, 0, 0, "Robert Lewandowski", 4, 7 },
                    { 14, 23, 0, 0, "Pedri", 3, 7 },
                    { 15, 21, 0, 0, "Gavi", 3, 7 },
                    { 16, 32, 0, 0, "Harry Kane", 4, 11 },
                    { 17, 23, 0, 0, "Jamal Musiala", 3, 11 },
                    { 18, 30, 0, 0, "Serge Gnabry", 4, 11 },
                    { 19, 34, 0, 0, "Marco Reus", 3, 12 },
                    { 20, 24, 0, 0, "Karim Adeyemi", 4, 12 },
                    { 21, 29, 0, 0, "Julian Brandt", 3, 12 },
                    { 22, 26, 0, 0, "Dusan Vlahovic", 4, 16 },
                    { 23, 21, 0, 0, "Kenan Yildiz", 4, 16 },
                    { 24, 26, 0, 0, "Jonathan David", 4, 16 },
                    { 25, 28, 0, 0, "Lautaro Martinez", 4, 18 },
                    { 26, 29, 0, 0, "Nicolo Barella", 3, 18 },
                    { 27, 28, 0, 0, "Marcus Thuram", 4, 18 },
                    { 28, 21, 0, 0, "Desire Doue", 4, 21 },
                    { 29, 27, 0, 0, "Ousmane Dembele", 4, 21 },
                    { 30, 24, 0, 0, "Vitinha", 3, 21 }
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
                name: "IX_Managers_TeamId",
                table: "Managers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayers_ApplicationUserId",
                table: "UserPlayers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayers_PlayerId",
                table: "UserPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayers_PlayerId1",
                table: "UserPlayers",
                column: "PlayerId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeams_ApplicationUserId",
                table: "UserTeams",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeams_TeamId",
                table: "UserTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeams_TeamId1",
                table: "UserTeams",
                column: "TeamId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Teams_TeamId",
                table: "Managers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Teams_TeamId",
                table: "Managers");

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
                name: "UserPlayers");

            migrationBuilder.DropTable(
                name: "UserTeams");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Managers");
        }
    }
}
