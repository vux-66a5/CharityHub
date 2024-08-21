using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharityHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0be6e455-f445-4bb0-b7d5-5adc8e84722f"), null, "Admin", null },
                    { new Guid("c1690b49-4678-4ef4-98d8-3092defa7acb"), null, "User", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "Email", "EmailConfirmed", "IsActive", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("6e168ab2-f753-42e3-a7fa-52c1389503bf"), 0, "c4290150-a6a3-43c5-be93-6d2a267a020c", new DateTime(2024, 8, 21, 16, 6, 37, 483, DateTimeKind.Local).AddTicks(662), "Anv@gmail.com", false, true, new DateTime(2024, 8, 21, 16, 6, 37, 483, DateTimeKind.Local).AddTicks(678), false, null, null, null, "AQAAAAIAAYagAAAAEP3bCVIDpZsmtTd2rw6zfCb20GXnSsc4+F2MPGT7ury0zjKHpvzkdGeOY5sY6gi69Q==", "0987654321", false, null, false, "Anv@gmail.com" },
                    { new Guid("e42e1e51-1b1c-4a09-864c-6675b4ff119c"), 0, "044b4d8f-3a04-4387-a8f1-8068272c8f1a", new DateTime(2024, 8, 21, 16, 6, 37, 427, DateTimeKind.Local).AddTicks(5459), "datdq@gmail.com", false, true, new DateTime(2024, 8, 21, 16, 6, 37, 427, DateTimeKind.Local).AddTicks(5471), false, null, null, null, "AQAAAAIAAYagAAAAEBQKxM2Y6d38j0Zbjf+VVAXGhZSR5x2nJit2RfPqDPkEiJsFDJqr2VsYj2E1rR35xQ==", "0123456789", false, null, false, "datdq@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "CampaignId", "CampaignCode", "CampaignDescription", "CampaignStatus", "CampaignThumbnail", "CampaignTitle", "CurrentAmount", "DateCreated", "EndDate", "PartnerLogo", "PartnerName", "PartnerNumber", "StartDate", "TargetAmount" },
                values: new object[] { new Guid("a90c326f-edd2-462e-8080-2b97fd932021"), 123, "This is a charity campaign.", "InProgress", "path/to/thumbnail.jpg", "Charity Campaign", 5000.00m, new DateTime(2024, 8, 21, 16, 6, 37, 537, DateTimeKind.Local).AddTicks(4328), new DateTime(2024, 9, 21, 16, 6, 37, 537, DateTimeKind.Local).AddTicks(4279), "path/to/logo.jpg", "Partner Organization", "0987654321", new DateTime(2024, 8, 21, 16, 6, 37, 537, DateTimeKind.Local).AddTicks(4267), 10000.00m });

            migrationBuilder.InsertData(
                table: "AdminActions",
                columns: new[] { "ActionId", "ActionType", "AdminId", "CompletedAt", "TargetCampaignId", "TargetUserId" },
                values: new object[] { new Guid("5c197a0c-d433-4718-89a9-7bbb8c4c8c5a"), "BanUser", new Guid("6e168ab2-f753-42e3-a7fa-52c1389503bf"), new DateTime(2024, 8, 21, 16, 6, 37, 537, DateTimeKind.Local).AddTicks(4329), new Guid("a90c326f-edd2-462e-8080-2b97fd932021"), new Guid("e42e1e51-1b1c-4a09-864c-6675b4ff119c") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0be6e455-f445-4bb0-b7d5-5adc8e84722f"), new Guid("6e168ab2-f753-42e3-a7fa-52c1389503bf") },
                    { new Guid("c1690b49-4678-4ef4-98d8-3092defa7acb"), new Guid("e42e1e51-1b1c-4a09-864c-6675b4ff119c") }
                });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "DonationId", "Amount", "CampaignId", "DateDonated", "IsConfirm", "PaymentMethod", "UserId" },
                values: new object[] { new Guid("619e3f80-bafb-48f9-9fc8-6656f4eab17e"), 100.00m, new Guid("a90c326f-edd2-462e-8080-2b97fd932021"), new DateTime(2024, 8, 21, 16, 6, 37, 537, DateTimeKind.Local).AddTicks(4332), true, "Paypal", new Guid("e42e1e51-1b1c-4a09-864c-6675b4ff119c") });

            migrationBuilder.InsertData(
                table: "UserFollows",
                columns: new[] { "CampaignId", "UserId", "DateFollowed", "IsNotified" },
                values: new object[] { new Guid("a90c326f-edd2-462e-8080-2b97fd932021"), new Guid("e42e1e51-1b1c-4a09-864c-6675b4ff119c"), new DateTime(2024, 8, 21, 16, 6, 37, 537, DateTimeKind.Local).AddTicks(4334), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminActions",
                keyColumn: "ActionId",
                keyValue: new Guid("5c197a0c-d433-4718-89a9-7bbb8c4c8c5a"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("0be6e455-f445-4bb0-b7d5-5adc8e84722f"), new Guid("6e168ab2-f753-42e3-a7fa-52c1389503bf") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c1690b49-4678-4ef4-98d8-3092defa7acb"), new Guid("e42e1e51-1b1c-4a09-864c-6675b4ff119c") });

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "DonationId",
                keyValue: new Guid("619e3f80-bafb-48f9-9fc8-6656f4eab17e"));

            migrationBuilder.DeleteData(
                table: "UserFollows",
                keyColumns: new[] { "CampaignId", "UserId" },
                keyValues: new object[] { new Guid("a90c326f-edd2-462e-8080-2b97fd932021"), new Guid("e42e1e51-1b1c-4a09-864c-6675b4ff119c") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0be6e455-f445-4bb0-b7d5-5adc8e84722f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c1690b49-4678-4ef4-98d8-3092defa7acb"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6e168ab2-f753-42e3-a7fa-52c1389503bf"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e42e1e51-1b1c-4a09-864c-6675b4ff119c"));

            migrationBuilder.DeleteData(
                table: "Campaigns",
                keyColumn: "CampaignId",
                keyValue: new Guid("a90c326f-edd2-462e-8080-2b97fd932021"));
        }
    }
}
