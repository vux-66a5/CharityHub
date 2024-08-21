using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharityHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataChariry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminActions",
                keyColumn: "ActionId",
                keyValue: new Guid("4b71dea1-ad68-4952-91c5-0b311a8bda0e"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("21627fed-167d-4517-bb27-7535af62761d"), new Guid("ef136f30-c648-4e45-91bc-f01a0c152279") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d94b173d-0444-4597-b83e-002af6e86d48"), new Guid("f86bbd7e-bae7-4da7-8264-2f72b8a82c49") });

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "DonationId",
                keyValue: new Guid("3c3e45aa-4280-4aba-877e-e509dbb6fe29"));

            migrationBuilder.DeleteData(
                table: "UserFollows",
                keyColumns: new[] { "CampaignId", "UserId" },
                keyValues: new object[] { new Guid("8552c690-abcd-4b6f-ac2c-b950f867e508"), new Guid("f86bbd7e-bae7-4da7-8264-2f72b8a82c49") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("21627fed-167d-4517-bb27-7535af62761d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d94b173d-0444-4597-b83e-002af6e86d48"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ef136f30-c648-4e45-91bc-f01a0c152279"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f86bbd7e-bae7-4da7-8264-2f72b8a82c49"));

            migrationBuilder.DeleteData(
                table: "Campaigns",
                keyColumn: "CampaignId",
                keyValue: new Guid("8552c690-abcd-4b6f-ac2c-b950f867e508"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7c81c2a0-ef77-4ce6-bff0-4d92fdc19f43"), null, "Admin", null },
                    { new Guid("af3e6bc9-eae2-4c8f-af73-1b7107a3f2e8"), null, "User", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "Email", "EmailConfirmed", "IsActive", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("e3622868-08c1-487d-924d-5d184977979f"), 0, "52e01fd4-e40b-41f7-9299-c111f2e1d90c", new DateTime(2024, 8, 21, 11, 50, 57, 399, DateTimeKind.Local).AddTicks(7216), "Anv@gmail.com", false, true, new DateTime(2024, 8, 21, 11, 50, 57, 399, DateTimeKind.Local).AddTicks(7238), false, null, null, null, "AQAAAAIAAYagAAAAEHd+rS9Vn2X5rtUeNZFTs9bUKhzWq8c9Ty1a07/jHiZFpzYRbJRJZMLZFXo/h4/u5A==", "0987654321", false, null, false, "Anv@gmail.com" },
                    { new Guid("ec6ed7ea-7e70-4d45-8926-f4c97af48ee6"), 0, "fdf53da0-3971-4936-9384-dd46c5f16ef5", new DateTime(2024, 8, 21, 11, 50, 57, 344, DateTimeKind.Local).AddTicks(6971), "datdq@gmail.com", false, true, new DateTime(2024, 8, 21, 11, 50, 57, 344, DateTimeKind.Local).AddTicks(6984), false, null, null, null, "AQAAAAIAAYagAAAAEINtnyn8Tx9rU/buxARkmbHBm9DqSvP7DezQCtyrWW5UFHvl2hgwZXHQZgBbAen0YA==", "0123456789", false, null, false, "datdq@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "CampaignId", "CampaignCode", "CampaignDescription", "CampaignStatus", "CampaignThumbnail", "CampaignTitle", "CurrentAmount", "DateCreated", "EndDate", "PartnerLogo", "PartnerName", "PartnerNumber", "StartDate", "TargetAmount" },
                values: new object[] { new Guid("9153c19f-9369-421c-bf79-9e50eadc50ef"), 123, "This is a charity campaign.", "InProgress", "path/to/thumbnail.jpg", "Charity Campaign", 5000.00m, new DateTime(2024, 8, 21, 11, 50, 57, 454, DateTimeKind.Local).AddTicks(5380), new DateTime(2024, 9, 21, 11, 50, 57, 454, DateTimeKind.Local).AddTicks(5306), "path/to/logo.jpg", "Partner Organization", "0987654321", new DateTime(2024, 8, 21, 11, 50, 57, 454, DateTimeKind.Local).AddTicks(5283), 10000.00m });

            migrationBuilder.InsertData(
                table: "AdminActions",
                columns: new[] { "ActionId", "ActionType", "AdminId", "CampaignId", "CompletedAt", "TargetCampaignId", "TargetUserId" },
                values: new object[] { new Guid("fe17465a-d9ae-4e41-b33f-f7b7996ec5f2"), "BanUser", new Guid("e3622868-08c1-487d-924d-5d184977979f"), null, new DateTime(2024, 8, 21, 11, 50, 57, 454, DateTimeKind.Local).AddTicks(5384), new Guid("9153c19f-9369-421c-bf79-9e50eadc50ef"), new Guid("ec6ed7ea-7e70-4d45-8926-f4c97af48ee6") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("7c81c2a0-ef77-4ce6-bff0-4d92fdc19f43"), new Guid("e3622868-08c1-487d-924d-5d184977979f") },
                    { new Guid("af3e6bc9-eae2-4c8f-af73-1b7107a3f2e8"), new Guid("ec6ed7ea-7e70-4d45-8926-f4c97af48ee6") }
                });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "DonationId", "Amount", "CampaignId", "DateDonated", "IsConfirm", "PaymentMethod", "UserId" },
                values: new object[] { new Guid("d7fc5fd3-b68c-4bf9-9714-0f93d2b07d26"), 100.00m, new Guid("9153c19f-9369-421c-bf79-9e50eadc50ef"), new DateTime(2024, 8, 21, 11, 50, 57, 454, DateTimeKind.Local).AddTicks(5390), true, "Paypal", new Guid("ec6ed7ea-7e70-4d45-8926-f4c97af48ee6") });

            migrationBuilder.InsertData(
                table: "UserFollows",
                columns: new[] { "CampaignId", "UserId", "DateFollowed", "IsNotified" },
                values: new object[] { new Guid("9153c19f-9369-421c-bf79-9e50eadc50ef"), new Guid("ec6ed7ea-7e70-4d45-8926-f4c97af48ee6"), new DateTime(2024, 8, 21, 11, 50, 57, 454, DateTimeKind.Local).AddTicks(5392), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdminActions",
                keyColumn: "ActionId",
                keyValue: new Guid("fe17465a-d9ae-4e41-b33f-f7b7996ec5f2"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7c81c2a0-ef77-4ce6-bff0-4d92fdc19f43"), new Guid("e3622868-08c1-487d-924d-5d184977979f") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("af3e6bc9-eae2-4c8f-af73-1b7107a3f2e8"), new Guid("ec6ed7ea-7e70-4d45-8926-f4c97af48ee6") });

            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "DonationId",
                keyValue: new Guid("d7fc5fd3-b68c-4bf9-9714-0f93d2b07d26"));

            migrationBuilder.DeleteData(
                table: "UserFollows",
                keyColumns: new[] { "CampaignId", "UserId" },
                keyValues: new object[] { new Guid("9153c19f-9369-421c-bf79-9e50eadc50ef"), new Guid("ec6ed7ea-7e70-4d45-8926-f4c97af48ee6") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7c81c2a0-ef77-4ce6-bff0-4d92fdc19f43"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af3e6bc9-eae2-4c8f-af73-1b7107a3f2e8"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e3622868-08c1-487d-924d-5d184977979f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec6ed7ea-7e70-4d45-8926-f4c97af48ee6"));

            migrationBuilder.DeleteData(
                table: "Campaigns",
                keyColumn: "CampaignId",
                keyValue: new Guid("9153c19f-9369-421c-bf79-9e50eadc50ef"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("21627fed-167d-4517-bb27-7535af62761d"), null, "Admin", null },
                    { new Guid("d94b173d-0444-4597-b83e-002af6e86d48"), null, "User", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "Email", "EmailConfirmed", "IsActive", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("ef136f30-c648-4e45-91bc-f01a0c152279"), 0, "0acec9ed-cdc4-4266-be2e-b8610eb57ac7", new DateTime(2024, 8, 21, 11, 49, 44, 264, DateTimeKind.Local).AddTicks(4154), "Anv@gmail.com", false, true, new DateTime(2024, 8, 21, 11, 49, 44, 264, DateTimeKind.Local).AddTicks(4168), false, null, null, null, "AQAAAAIAAYagAAAAEIJTMNPEXTlLC6WJoI+cs0gxOOPwHr11YXFX2z1bko0CsvbPzfx8Dq2cGh6r2CSBfw==", "0987654321", false, null, false, "Anv@gmail.com" },
                    { new Guid("f86bbd7e-bae7-4da7-8264-2f72b8a82c49"), 0, "7a8a6f62-cc51-4a09-af6a-b315681307b9", new DateTime(2024, 8, 21, 11, 49, 44, 209, DateTimeKind.Local).AddTicks(1660), "datdq@gmail.com", false, true, new DateTime(2024, 8, 21, 11, 49, 44, 209, DateTimeKind.Local).AddTicks(1676), false, null, null, null, "AQAAAAIAAYagAAAAEGhYkLJWWaOE9ChZ9QRUZRxyy93YcKVECNs0WN/5QIF8FAYGv+hUe79XzE3N/w/SEA==", "0123456789", false, null, false, "datdq@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "CampaignId", "CampaignCode", "CampaignDescription", "CampaignStatus", "CampaignThumbnail", "CampaignTitle", "CurrentAmount", "DateCreated", "EndDate", "PartnerLogo", "PartnerName", "PartnerNumber", "StartDate", "TargetAmount" },
                values: new object[] { new Guid("8552c690-abcd-4b6f-ac2c-b950f867e508"), 123, "This is a charity campaign.", "InProgress", "path/to/thumbnail.jpg", "Charity Campaign", 5000.00m, new DateTime(2024, 8, 21, 11, 49, 44, 318, DateTimeKind.Local).AddTicks(6395), new DateTime(2024, 9, 21, 11, 49, 44, 318, DateTimeKind.Local).AddTicks(6319), "path/to/logo.jpg", "Partner Organization", "0987654321", new DateTime(2024, 8, 21, 11, 49, 44, 318, DateTimeKind.Local).AddTicks(6297), 10000.00m });

            migrationBuilder.InsertData(
                table: "AdminActions",
                columns: new[] { "ActionId", "ActionType", "AdminId", "CampaignId", "CompletedAt", "TargetCampaignId", "TargetUserId" },
                values: new object[] { new Guid("4b71dea1-ad68-4952-91c5-0b311a8bda0e"), "BanUser", new Guid("ef136f30-c648-4e45-91bc-f01a0c152279"), null, new DateTime(2024, 8, 21, 11, 49, 44, 318, DateTimeKind.Local).AddTicks(6399), new Guid("8552c690-abcd-4b6f-ac2c-b950f867e508"), new Guid("f86bbd7e-bae7-4da7-8264-2f72b8a82c49") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("21627fed-167d-4517-bb27-7535af62761d"), new Guid("ef136f30-c648-4e45-91bc-f01a0c152279") },
                    { new Guid("d94b173d-0444-4597-b83e-002af6e86d48"), new Guid("f86bbd7e-bae7-4da7-8264-2f72b8a82c49") }
                });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "DonationId", "Amount", "CampaignId", "DateDonated", "IsConfirm", "PaymentMethod", "UserId" },
                values: new object[] { new Guid("3c3e45aa-4280-4aba-877e-e509dbb6fe29"), 100.00m, new Guid("8552c690-abcd-4b6f-ac2c-b950f867e508"), new DateTime(2024, 8, 21, 11, 49, 44, 318, DateTimeKind.Local).AddTicks(6402), true, "Paypal", new Guid("f86bbd7e-bae7-4da7-8264-2f72b8a82c49") });

            migrationBuilder.InsertData(
                table: "UserFollows",
                columns: new[] { "CampaignId", "UserId", "DateFollowed", "IsNotified" },
                values: new object[] { new Guid("8552c690-abcd-4b6f-ac2c-b950f867e508"), new Guid("f86bbd7e-bae7-4da7-8264-2f72b8a82c49"), new DateTime(2024, 8, 21, 11, 49, 44, 318, DateTimeKind.Local).AddTicks(6407), true });
        }
    }
}
