using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warsha_MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES (N'6eda76f5-6200-4208-9830-3a6b0d22d8fa', N'ziadhany400', N'ZIADHANY400', N'ziadhany400@yahoo.com', N'ZIADHANY400@YAHOO.COM', 0, N'AQAAAAIAAYagAAAAEJR8BDwoLy4vaazLOx4YQTEV+pQYEGnY/xTyPARa04zmcoryGX9OPY7KSc+WAdcrWw==', N'6Y7IVI6NI55KURDRHN22HWWYZTMLJUFT', N'4dfbb9db-c18d-4c39-be77-93a323865cd7', NULL, 0, 0, NULL, 1, 0, N'Ziad', N'hany', null)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE Id = '6eda76f5-6200-4208-9830-3a6b0d22d8fa'");

        }
    }
}
