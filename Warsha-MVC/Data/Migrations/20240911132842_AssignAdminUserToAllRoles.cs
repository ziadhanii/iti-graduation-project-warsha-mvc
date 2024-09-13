using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warsha_MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssignAdminUserToAllRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[UserRoles] (UserId, RoleId) SELECT '6eda76f5-6200-4208-9830-3a6b0d22d8fa', Id FROM [security].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[UserRoles] WHERE UserId = '6eda76f5-6200-4208-9830-3a6b0d22d8fa'");
        }
    }
}
