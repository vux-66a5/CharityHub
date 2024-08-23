using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharityHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminActions",
                keyColumn: "ActionId",
                keyValue: new Guid("2c389c5f-fbc1-44ed-a15c-122cfeb0d769"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b0305779-a87a-43cc-8155-fc07690954c5"), new Guid("49c15139-1efb-4ec2-9fca-1a02504c6d57") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("79413d4d-0eab-4a66-820f-4272b1365554"), new Guid("b4014e2b-f9fe-4b82-b016-8293bac5bece") });

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "DonationId",
                keyValue: new Guid("e03f640a-dacc-4378-a4e9-de2d4daf4061"));

            migrationBuilder.DeleteData(
                table: "UserFollows",
                keyColumns: new[] { "CampaignId", "UserId" },
                keyValues: new object[] { new Guid("072a81b2-0aaf-43ba-8f74-124aef206643"), new Guid("b4014e2b-f9fe-4b82-b016-8293bac5bece") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("79413d4d-0eab-4a66-820f-4272b1365554"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b0305779-a87a-43cc-8155-fc07690954c5"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("49c15139-1efb-4ec2-9fca-1a02504c6d57"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b4014e2b-f9fe-4b82-b016-8293bac5bece"));

            migrationBuilder.DeleteData(
                table: "Campaigns",
                keyColumn: "CampaignId",
                keyValue: new Guid("072a81b2-0aaf-43ba-8f74-124aef206643"));

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5248787c-15cf-4ba5-ba51-de21b6960234"), "6ba99203-aeea-4676-b9b7-204bd186e8c9", "User", "USER" },
                    { new Guid("6ba99203-aeea-4676-b9b7-204bd186e8c9"), "6ba99203-aeea-4676-b9b7-204bd186e8c9", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "DisplayName", "Email", "EmailConfirmed", "IsActive", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("c86199fc-84e3-423c-9c2b-4853899a5c2a"), 0, "ec639297-3809-4e07-adb6-c3d8788cf1c5", new DateTime(2024, 8, 23, 20, 17, 14, 832, DateTimeKind.Local).AddTicks(1485), "Anv", "Anv@gmail.com", false, true, new DateTime(2024, 8, 23, 20, 17, 14, 832, DateTimeKind.Local).AddTicks(1506), false, null, null, null, "AQAAAAIAAYagAAAAEGKTNdQluM8RNFz2fbC6juYOW0ipiU7QS77aX+OaNa9JPodWbbWALfIC+CBbILIefg==", "0987654321", false, null, false, "Anv@gmail.com" },
                    { new Guid("d5d2828e-6d1a-4848-9212-076a1c335bbd"), 0, "46647781-482e-4e66-98aa-a4e3b28e079b", new DateTime(2024, 8, 23, 20, 17, 14, 597, DateTimeKind.Local).AddTicks(8700), "Datdq", "datdq@gmail.com", false, true, new DateTime(2024, 8, 23, 20, 17, 14, 597, DateTimeKind.Local).AddTicks(8720), false, null, null, null, "AQAAAAIAAYagAAAAEPHZopHzB3wHn1eluZPDLycTM2FPlfGpeNclaG+ZgHl+TCZ51l8yjY/xNOBG04ENSg==", "0123456789", false, null, false, "datdq@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "CampaignId", "CampaignCode", "CampaignDescription", "CampaignStatus", "CampaignThumbnail", "CampaignTitle", "CurrentAmount", "DateCreated", "EndDate", "PartnerLogo", "PartnerName", "PartnerNumber", "StartDate", "TargetAmount" },
                values: new object[] { new Guid("f98c0fc4-5861-44bb-9ff0-f3537ddde8dd"), 123, "This is a charity campaign.", "InProgress", "path/to/thumbnail.jpg", "Charity Campaign", 5000.00m, new DateTime(2024, 8, 23, 20, 17, 15, 30, DateTimeKind.Local).AddTicks(1624), new DateTime(2024, 9, 23, 20, 17, 15, 30, DateTimeKind.Local).AddTicks(1592), "path/to/logo.jpg", "Partner Organization", "0987654321", new DateTime(2024, 8, 23, 20, 17, 15, 30, DateTimeKind.Local).AddTicks(1576), 10000.00m });

            migrationBuilder.InsertData(
                table: "AdminActions",
                columns: new[] { "ActionId", "ActionType", "AdminId", "CompletedAt", "TargetCampaignId", "TargetUserId" },
                values: new object[] { new Guid("8328c0a5-d539-4fac-a859-c204f671599a"), "BanUser", new Guid("c86199fc-84e3-423c-9c2b-4853899a5c2a"), new DateTime(2024, 8, 23, 20, 17, 15, 30, DateTimeKind.Local).AddTicks(1631), new Guid("f98c0fc4-5861-44bb-9ff0-f3537ddde8dd"), new Guid("d5d2828e-6d1a-4848-9212-076a1c335bbd") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("6ba99203-aeea-4676-b9b7-204bd186e8c9"), new Guid("c86199fc-84e3-423c-9c2b-4853899a5c2a") },
                    { new Guid("5248787c-15cf-4ba5-ba51-de21b6960234"), new Guid("d5d2828e-6d1a-4848-9212-076a1c335bbd") }
                });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "DonationId", "Amount", "CampaignId", "DateDonated", "IsConfirm", "PaymentMethod", "UserId" },
                values: new object[] { new Guid("57d2bc11-6602-4189-bcbb-02d8bfddc264"), 100.00m, new Guid("f98c0fc4-5861-44bb-9ff0-f3537ddde8dd"), new DateTime(2024, 8, 23, 20, 17, 15, 30, DateTimeKind.Local).AddTicks(1636), true, "Paypal", new Guid("d5d2828e-6d1a-4848-9212-076a1c335bbd") });

            migrationBuilder.InsertData(
                table: "UserFollows",
                columns: new[] { "CampaignId", "UserId", "DateFollowed", "IsNotified" },
                values: new object[] { new Guid("f98c0fc4-5861-44bb-9ff0-f3537ddde8dd"), new Guid("d5d2828e-6d1a-4848-9212-076a1c335bbd"), new DateTime(2024, 8, 23, 20, 17, 15, 30, DateTimeKind.Local).AddTicks(1642), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminActions",
                keyColumn: "ActionId",
                keyValue: new Guid("8328c0a5-d539-4fac-a859-c204f671599a"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6ba99203-aeea-4676-b9b7-204bd186e8c9"), new Guid("c86199fc-84e3-423c-9c2b-4853899a5c2a") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5248787c-15cf-4ba5-ba51-de21b6960234"), new Guid("d5d2828e-6d1a-4848-9212-076a1c335bbd") });

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "DonationId",
                keyValue: new Guid("57d2bc11-6602-4189-bcbb-02d8bfddc264"));

            migrationBuilder.DeleteData(
                table: "UserFollows",
                keyColumns: new[] { "CampaignId", "UserId" },
                keyValues: new object[] { new Guid("f98c0fc4-5861-44bb-9ff0-f3537ddde8dd"), new Guid("d5d2828e-6d1a-4848-9212-076a1c335bbd") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5248787c-15cf-4ba5-ba51-de21b6960234"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6ba99203-aeea-4676-b9b7-204bd186e8c9"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c86199fc-84e3-423c-9c2b-4853899a5c2a"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5d2828e-6d1a-4848-9212-076a1c335bbd"));

            migrationBuilder.DeleteData(
                table: "Campaigns",
                keyColumn: "CampaignId",
                keyValue: new Guid("f98c0fc4-5861-44bb-9ff0-f3537ddde8dd"));

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("79413d4d-0eab-4a66-820f-4272b1365554"), "b0305779-a87a-43cc-8155-fc07690954c5", "User", "USER" },
                    { new Guid("b0305779-a87a-43cc-8155-fc07690954c5"), "b0305779-a87a-43cc-8155-fc07690954c5", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "Email", "EmailConfirmed", "IsActive", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("49c15139-1efb-4ec2-9fca-1a02504c6d57"), 0, "f316d406-15e7-48e3-a87f-6e6a07cb9b97", new DateTime(2024, 8, 23, 14, 9, 3, 217, DateTimeKind.Local).AddTicks(3243), "Anv@gmail.com", false, true, new DateTime(2024, 8, 23, 14, 9, 3, 217, DateTimeKind.Local).AddTicks(3245), false, null, null, null, "dat@123", "0987654321", false, null, false, "Anv@gmail.com" },
                    { new Guid("b4014e2b-f9fe-4b82-b016-8293bac5bece"), 0, "64a5313c-3dbb-4380-9173-272507c41387", new DateTime(2024, 8, 23, 14, 9, 3, 217, DateTimeKind.Local).AddTicks(3220), "datdq@gmail.com", false, true, new DateTime(2024, 8, 23, 14, 9, 3, 217, DateTimeKind.Local).AddTicks(3234), false, null, null, null, "dat@123", "0123456789", false, null, false, "datdq@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "CampaignId", "CampaignCode", "CampaignDescription", "CampaignStatus", "CampaignThumbnail", "CampaignTitle", "CurrentAmount", "DateCreated", "EndDate", "PartnerLogo", "PartnerName", "PartnerNumber", "StartDate", "TargetAmount" },
                values: new object[] { new Guid("072a81b2-0aaf-43ba-8f74-124aef206643"), 123, "This is a charity campaign.", "InProgress", "path/to/thumbnail.jpg", "Charity Campaign", 5000.00m, new DateTime(2024, 8, 23, 14, 9, 3, 217, DateTimeKind.Local).AddTicks(3360), new DateTime(2024, 9, 23, 14, 9, 3, 217, DateTimeKind.Local).AddTicks(3319), "path/to/logo.jpg", "Partner Organization", "0987654321", new DateTime(2024, 8, 23, 14, 9, 3, 217, DateTimeKind.Local).AddTicks(3317), 10000.00m });

            migrationBuilder.InsertData(
                table: "AdminActions",
                columns: new[] { "ActionId", "ActionType", "AdminId", "CompletedAt", "TargetCampaignId", "TargetUserId" },
                values: new object[] { new Guid("2c389c5f-fbc1-44ed-a15c-122cfeb0d769"), "BanUser", new Guid("49c15139-1efb-4ec2-9fca-1a02504c6d57"), new DateTime(2024, 8, 23, 14, 9, 3, 217, DateTimeKind.Local).AddTicks(3367), new Guid("072a81b2-0aaf-43ba-8f74-124aef206643"), new Guid("b4014e2b-f9fe-4b82-b016-8293bac5bece") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("b0305779-a87a-43cc-8155-fc07690954c5"), new Guid("49c15139-1efb-4ec2-9fca-1a02504c6d57") },
                    { new Guid("79413d4d-0eab-4a66-820f-4272b1365554"), new Guid("b4014e2b-f9fe-4b82-b016-8293bac5bece") }
                });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "DonationId", "Amount", "CampaignId", "DateDonated", "IsConfirm", "PaymentMethod", "UserId" },
                values: new object[] { new Guid("e03f640a-dacc-4378-a4e9-de2d4daf4061"), 100.00m, new Guid("072a81b2-0aaf-43ba-8f74-124aef206643"), new DateTime(2024, 8, 23, 14, 9, 3, 217, DateTimeKind.Local).AddTicks(3374), true, "Paypal", new Guid("b4014e2b-f9fe-4b82-b016-8293bac5bece") });

            migrationBuilder.InsertData(
                table: "UserFollows",
                columns: new[] { "CampaignId", "UserId", "DateFollowed", "IsNotified" },
                values: new object[] { new Guid("072a81b2-0aaf-43ba-8f74-124aef206643"), new Guid("b4014e2b-f9fe-4b82-b016-8293bac5bece"), new DateTime(2024, 8, 23, 14, 9, 3, 217, DateTimeKind.Local).AddTicks(3474), true });
        }
    }
}
