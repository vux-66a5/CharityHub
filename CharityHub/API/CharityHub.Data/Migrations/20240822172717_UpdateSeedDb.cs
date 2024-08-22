using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharityHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a98900e2-232e-471c-94b3-b4b44367233c"), null, "Admin", null },
                    { new Guid("f6348b92-d1a3-484e-a2bf-58739f870075"), null, "User", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "Email", "EmailConfirmed", "IsActive", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("1cb61da8-24ac-4a36-b181-a19064f561d8"), 0, "2286d120-90aa-479b-9e17-0a2d0426b42c", new DateTime(2024, 8, 23, 0, 27, 17, 342, DateTimeKind.Local).AddTicks(7893), "Anv@gmail.com", false, true, new DateTime(2024, 8, 23, 0, 27, 17, 342, DateTimeKind.Local).AddTicks(7896), false, null, null, null, "AQAAAAIAAYagAAAAEH4PeZmo19RPRQLLs2v8szynOaInQ/tKwyUefiXlXGxSgI/PvPZnJa1Wy7HenjfMyg==", "0987654321", false, null, false, "Anv@gmail.com" },
                    { new Guid("75915256-3f7d-4028-96b2-f9b6f8820a8b"), 0, "14dad404-8666-4c16-99ac-5faf71b9e31f", new DateTime(2024, 8, 23, 0, 27, 17, 288, DateTimeKind.Local).AddTicks(4061), "datdq@gmail.com", false, true, new DateTime(2024, 8, 23, 0, 27, 17, 288, DateTimeKind.Local).AddTicks(4073), false, null, null, null, "AQAAAAIAAYagAAAAEKFTmujkJ8rNG1J4NfUONdAhY2zZFx/Z5p+lEFkdnzOSaxcBdVDbkWljJ+yh5SlzQw==", "0123456789", false, null, false, "datdq@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "CampaignId", "CampaignCode", "CampaignDescription", "CampaignStatus", "CampaignThumbnail", "CampaignTitle", "CurrentAmount", "DateCreated", "EndDate", "PartnerLogo", "PartnerName", "PartnerNumber", "StartDate", "TargetAmount" },
                values: new object[] { new Guid("21af526b-af55-4d76-bdfc-946690cfa2d6"), 123, "This is a charity campaign.", "InProgress", "path/to/thumbnail.jpg", "Charity Campaign", 5000.00m, new DateTime(2024, 8, 23, 0, 27, 17, 398, DateTimeKind.Local).AddTicks(378), new DateTime(2024, 9, 23, 0, 27, 17, 398, DateTimeKind.Local).AddTicks(329), "path/to/logo.jpg", "Partner Organization", "0987654321", new DateTime(2024, 8, 23, 0, 27, 17, 398, DateTimeKind.Local).AddTicks(318), 10000.00m });

            migrationBuilder.InsertData(
                table: "AdminActions",
                columns: new[] { "ActionId", "ActionType", "AdminId", "CompletedAt", "TargetCampaignId", "TargetUserId" },
                values: new object[] { new Guid("790e2a9a-1690-47e0-8d2e-200d4047bb85"), "BanUser", new Guid("1cb61da8-24ac-4a36-b181-a19064f561d8"), new DateTime(2024, 8, 23, 0, 27, 17, 398, DateTimeKind.Local).AddTicks(382), new Guid("21af526b-af55-4d76-bdfc-946690cfa2d6"), new Guid("75915256-3f7d-4028-96b2-f9b6f8820a8b") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("a98900e2-232e-471c-94b3-b4b44367233c"), new Guid("1cb61da8-24ac-4a36-b181-a19064f561d8") },
                    { new Guid("f6348b92-d1a3-484e-a2bf-58739f870075"), new Guid("75915256-3f7d-4028-96b2-f9b6f8820a8b") }
                });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "DonationId", "Amount", "CampaignId", "DateDonated", "IsConfirm", "PaymentMethod", "UserId" },
                values: new object[] { new Guid("778e22b9-df07-47e6-aa4d-4dc55d0b63c7"), 100.00m, new Guid("21af526b-af55-4d76-bdfc-946690cfa2d6"), new DateTime(2024, 8, 23, 0, 27, 17, 398, DateTimeKind.Local).AddTicks(385), true, "Paypal", new Guid("75915256-3f7d-4028-96b2-f9b6f8820a8b") });

            migrationBuilder.InsertData(
                table: "UserFollows",
                columns: new[] { "CampaignId", "UserId", "DateFollowed", "IsNotified" },
                values: new object[] { new Guid("21af526b-af55-4d76-bdfc-946690cfa2d6"), new Guid("75915256-3f7d-4028-96b2-f9b6f8820a8b"), new DateTime(2024, 8, 23, 0, 27, 17, 398, DateTimeKind.Local).AddTicks(387), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminActions",
                keyColumn: "ActionId",
                keyValue: new Guid("790e2a9a-1690-47e0-8d2e-200d4047bb85"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a98900e2-232e-471c-94b3-b4b44367233c"), new Guid("1cb61da8-24ac-4a36-b181-a19064f561d8") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f6348b92-d1a3-484e-a2bf-58739f870075"), new Guid("75915256-3f7d-4028-96b2-f9b6f8820a8b") });

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "DonationId",
                keyValue: new Guid("778e22b9-df07-47e6-aa4d-4dc55d0b63c7"));

            migrationBuilder.DeleteData(
                table: "UserFollows",
                keyColumns: new[] { "CampaignId", "UserId" },
                keyValues: new object[] { new Guid("21af526b-af55-4d76-bdfc-946690cfa2d6"), new Guid("75915256-3f7d-4028-96b2-f9b6f8820a8b") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a98900e2-232e-471c-94b3-b4b44367233c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f6348b92-d1a3-484e-a2bf-58739f870075"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1cb61da8-24ac-4a36-b181-a19064f561d8"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("75915256-3f7d-4028-96b2-f9b6f8820a8b"));

            migrationBuilder.DeleteData(
                table: "Campaigns",
                keyColumn: "CampaignId",
                keyValue: new Guid("21af526b-af55-4d76-bdfc-946690cfa2d6"));
        }
    }
}
