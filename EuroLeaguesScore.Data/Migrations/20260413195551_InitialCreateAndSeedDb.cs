using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EuroLeaguesScore.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAndSeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeTeamGoals = table.Column<int>(type: "int", nullable: false),
                    AwayTeamGoals = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                table: "Teams",
                columns: new[] { "Id", "City", "Country", "Draws", "LeagueId", "Losses", "ManagerId", "Name", "Wins" },
                values: new object[,]
                {
                    { 1, "Manchester", "England", 0, 1, 0, null, "Manchester City", 0 },
                    { 2, "London", "England", 0, 1, 0, null, "Arsenal", 0 },
                    { 3, "Liverpool", "England", 0, 1, 0, null, "Liverpool", 0 },
                    { 4, "Manchester", "England", 0, 1, 0, null, "Manchester United", 0 },
                    { 5, "London", "England", 0, 1, 0, null, "Chelsea", 0 },
                    { 6, "Madrid", "Spain", 0, 2, 0, null, "Real Madrid", 0 },
                    { 7, "Barcelona", "Spain", 0, 2, 0, null, "Barcelona", 0 },
                    { 8, "Madrid", "Spain", 0, 2, 0, null, "Atletico Madrid", 0 },
                    { 9, "Seville", "Spain", 0, 2, 0, null, "Sevilla", 0 },
                    { 10, "San Sebastian", "Spain", 0, 2, 0, null, "Real Sociedad", 0 },
                    { 11, "Munich", "Germany", 0, 3, 0, null, "Bayern Munich", 0 },
                    { 12, "Dortmund", "Germany", 0, 3, 0, null, "Borussia Dortmund", 0 },
                    { 13, "Leipzig", "Germany", 0, 3, 0, null, "RB Leipzig", 0 },
                    { 14, "Leverkusen", "Germany", 0, 3, 0, null, "Bayer Leverkusen", 0 },
                    { 15, "Wolfsburg", "Germany", 0, 3, 0, null, "Wolfsburg", 0 },
                    { 16, "Turin", "Italy", 0, 4, 0, null, "Juventus", 0 },
                    { 17, "Milan", "Italy", 0, 4, 0, null, "AC Milan", 0 },
                    { 18, "Milan", "Italy", 0, 4, 0, null, "Inter Milan", 0 },
                    { 19, "Naples", "Italy", 0, 4, 0, null, "Napoli", 0 },
                    { 20, "Rome", "Italy", 0, 4, 0, null, "Roma", 0 },
                    { 21, "Paris", "France", 0, 5, 0, null, "Paris Saint-Germain", 0 },
                    { 22, "Marseille", "France", 0, 5, 0, null, "Marseille", 0 },
                    { 23, "Lyon", "France", 0, 5, 0, null, "Lyon", 0 },
                    { 24, "Monaco", "Monaco", 0, 5, 0, null, "Monaco", 0 },
                    { 25, "Lille", "France", 0, 5, 0, null, "Lille", 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "Assists", "Goals", "ManagerId", "Name", "Position", "TeamId" },
                values: new object[,]
                {
                    { 1, 24, 0, 0, null, "Erling Haaland", 4, 1 },
                    { 2, 32, 0, 0, null, "Kevin De Bruyne", 3, 1 },
                    { 3, 23, 0, 0, null, "Phil Foden", 3, 1 },
                    { 4, 22, 0, 0, null, "Bukayo Saka", 4, 2 },
                    { 5, 25, 0, 0, null, "Martin Odegaard", 3, 2 },
                    { 6, 27, 0, 0, null, "Gabriel Jesus", 4, 2 },
                    { 7, 31, 0, 0, null, "Mohamed Salah", 4, 3 },
                    { 8, 24, 0, 0, null, "Darwin Nunez", 4, 3 },
                    { 9, 25, 0, 0, null, "Trent Alexander-Arnold", 2, 3 },
                    { 10, 21, 0, 0, null, "Jude Bellingham", 3, 6 },
                    { 11, 23, 0, 0, null, "Vinicius Jr", 4, 6 },
                    { 12, 23, 0, 0, null, "Rodrygo", 4, 6 },
                    { 13, 35, 0, 0, null, "Robert Lewandowski", 4, 7 },
                    { 14, 21, 0, 0, null, "Pedri", 3, 7 },
                    { 15, 19, 0, 0, null, "Gavi", 3, 7 },
                    { 16, 30, 0, 0, null, "Harry Kane", 4, 11 },
                    { 17, 21, 0, 0, null, "Jamal Musiala", 3, 11 },
                    { 18, 28, 0, 0, null, "Leroy Sane", 4, 11 },
                    { 19, 34, 0, 0, null, "Marco Reus", 3, 12 },
                    { 20, 22, 0, 0, null, "Karim Adeyemi", 4, 12 },
                    { 21, 27, 0, 0, null, "Julian Brandt", 3, 12 },
                    { 22, 24, 0, 0, null, "Dusan Vlahovic", 4, 16 },
                    { 23, 26, 0, 0, null, "Federico Chiesa", 4, 16 },
                    { 24, 29, 0, 0, null, "Adrien Rabiot", 3, 16 },
                    { 25, 26, 0, 0, null, "Lautaro Martinez", 4, 18 },
                    { 26, 27, 0, 0, null, "Nicolo Barella", 3, 18 },
                    { 27, 26, 0, 0, null, "Marcus Thuram", 4, 18 },
                    { 28, 25, 0, 0, null, "Kylian Mbappe", 4, 21 },
                    { 29, 27, 0, 0, null, "Ousmane Dembele", 4, 21 },
                    { 30, 24, 0, 0, null, "Vitinha", 3, 21 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_TeamId",
                table: "Managers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                table: "Managers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_ManagerId",
                table: "Players",
                column: "ManagerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Teams_TeamId",
                table: "Managers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Teams_TeamId",
                table: "Managers");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Managers");
        }
    }
}
