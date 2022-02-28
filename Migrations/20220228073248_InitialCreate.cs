using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieCharacterAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CharacterPicture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Franchises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ReleaseYear = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Director = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MoviePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trailer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FranchiseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Franchises_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                columns: table => new
                {
                    MoviesId = table.Column<int>(type: "int", nullable: false),
                    CharactersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.MoviesId, x.CharactersId });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Alias", "CharacterPicture", "FullName", "Gender" },
                values: new object[,]
                {
                    { 1, "Iron man", "https://th.bing.com/th/id/R.a1493b8983c19f3158df354f1c0fe054?rik=t6iG5uRzMhaPjw&pid=ImgRaw&r=0", "Robert Downey Jr", "Male" },
                    { 2, "Frodo Baggins", "https://i.pinimg.com/736x/1b/93/84/1b9384b6de87ab45a1391d454bd695c5.jpg", "Elijah Wood", "Male" },
                    { 3, "James Bond", "https://assets.mi6-hq.com/images/features/magazine-special4a.jpg", "Roger Moore", "Male" }
                });

            migrationBuilder.InsertData(
                table: "Franchises",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "The films based on characters that appear in comic books published by Marvel Comics", "Marvel cinematic universe" },
                    { 2, "Fantasy films based on the novel written by J.R.R. Tolkien", "The lord of the rings" },
                    { 3, "A british secret agent working for MI6 under the codename 007", "James Bond" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "MoviePicture", "MovieTitle", "ReleaseYear", "Trailer" },
                values: new object[,]
                {
                    { 1, "Jon Favreau", 1, "Action", "https://th.bing.com/th/id/R.dc6072bd82f5c534f7f7583f451a5534?rik=GqOVQyAWzMtbcw&pid=ImgRaw&r=0", "Iron man", "2008", "https://www.youtube.com/watch?v=8ugaeA-nMTc" },
                    { 2, "Peter Jackson", 2, "Fantasy", "https://th.bing.com/th/id/OIP.iu0nj0wNpcI0N-Pss_ihwQHaKw?pid=ImgDet&rs=1", "The fellowship of the ring", "2001", "https://www.youtube.com/watch?v=V75dMMIW2B4" },
                    { 3, "Lewis Gilbert", 3, "Action", "https://th.bing.com/th/id/R.769e22f0f07bc67a3a5430a88d10dbd6?rik=eZJtqpB1%2b3MaJA&pid=ImgRaw&r=0", "Moonraker", "1979", "https://www.youtube.com/watch?v=KFOOjYU16KE" },
                    { 4, "John Glen", 3, "Action", "https://i.pinimg.com/originals/79/b4/9b/79b49b9b10c0cf4c472167c60d65cd43.jpg", "Octopussy", "1983", "https://www.youtube.com/watch?v=q1hLWZzgZvU" }
                });

            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharactersId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_CharactersId",
                table: "CharacterMovie",
                column: "CharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FranchiseId",
                table: "Movies",
                column: "FranchiseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Franchises");
        }
    }
}
