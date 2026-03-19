using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QualityWebSystem.Migrations
{
    /// <inheritdoc />
    public partial class Added_IsReplied_field_on_Table_reviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<bool>(
            //    name: "isReplied",
            //    table: "Reviews",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);
            //isReplied field is alter by db scripts manually not by migration
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isReplied",
                table: "Reviews");
        }
    }
}
