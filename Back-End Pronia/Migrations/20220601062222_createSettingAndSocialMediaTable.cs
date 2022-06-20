using Microsoft.EntityFrameworkCore.Migrations;

namespace Back_End_Pronia.Migrations
{
    public partial class createSettingAndSocialMediaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderLogo = table.Column<string>(nullable: true),
                    FooterLogo = table.Column<string>(nullable: true),
                    SearchIcon = table.Column<string>(nullable: true),
                    AccountIcon = table.Column<string>(nullable: true),
                    WishListIcon = table.Column<string>(nullable: true),
                    BsketIcon = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    AdvertisementImage = table.Column<string>(nullable: true),
                    SettingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_SettingId",
                table: "Settings",
                column: "SettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
