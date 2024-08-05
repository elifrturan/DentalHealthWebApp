using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRecommendation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRecommendations",
                columns: table => new
                {
                    UserRecommendationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RecommendationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecommendations", x => x.UserRecommendationID);
                    table.ForeignKey(
                        name: "FK_UserRecommendations_Recommendations_RecommendationID",
                        column: x => x.RecommendationID,
                        principalTable: "Recommendations",
                        principalColumn: "RecommendationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRecommendations_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRecommendations_RecommendationID",
                table: "UserRecommendations",
                column: "RecommendationID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRecommendations_UserID",
                table: "UserRecommendations",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRecommendations");
        }
    }
}
