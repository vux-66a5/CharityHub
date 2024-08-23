using CharityHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.Data.Data
{
    public class CharityHubDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<AdminAction> AdminActions { get; set; }
        public DbSet<UserFollows> UserFollows { get; set; }

        public CharityHubDbContext(DbContextOptions<CharityHubDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-GEIJRR8I;database=CharityAppDbContext;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber)
                .HasColumnType("varchar(10)");

            // fluent api
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.User)
                .WithMany(u => u.Donations)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Campaign)
                .WithMany(c => c.Donations)
                .HasForeignKey(d => d.CampaignId);

            modelBuilder.Entity<UserFollows>()
            .HasKey(uf => new { uf.UserId, uf.CampaignId });

            modelBuilder.Entity<UserFollows>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFollows)
                .HasForeignKey(uf => uf.UserId);

            modelBuilder.Entity<UserFollows>()
                .HasOne(uf => uf.Campaign)
                .WithMany(c => c.UserFollows)
                .HasForeignKey(uf => uf.CampaignId);

            modelBuilder.Entity<AdminAction>()
                .HasOne(aa => aa.Admin)
                .WithMany(u => u.AdminActions)
                .HasForeignKey(aa => aa.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AdminAction>()
                .HasOne(aa => aa.TargetUser)
                .WithMany()
                .HasForeignKey(aa => aa.TargetUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AdminAction>()
                .HasOne(aa => aa.TargetCampaign)
                .WithMany(c => c.AdminActions)
                .HasForeignKey(aa => aa.TargetCampaignId)
                .OnDelete(DeleteBehavior.Restrict);

            /*var userId = Guid.NewGuid();*/
            var adminId = Guid.NewGuid();
            var roleUserId = Guid.NewGuid();
            var roleAdminId = Guid.NewGuid();
            /*var campaignId = Guid.NewGuid();
            var actionId = Guid.NewGuid();
            var donationId = Guid.NewGuid();
            var followId = Guid.NewGuid();


            // Tạo dữ liệu cho user
            var user = new User
            {
                Id = userId,
                UserName = "datdq@gmail.com",
                Email = "datdq@gmail.com",
                PhoneNumber = "0123456789",
                DateCreated = DateTime.Now,
                IsActive = true,
                LastLoginDate = DateTime.Now,
                PasswordHash = "dat@123"
            };*/

            var passwordHasher = new PasswordHasher<User>();

            // Tạo dữ liệu cho admin
            var admin = new User
            {
                Id = adminId,
                UserName = "datdq@gmail.com",
                Email = "datdq@gmail.com",
                DisplayName = "Dao Quoc Dat",
                PhoneNumber = "0987654321",
                DateCreated = DateTime.Now,
                IsActive = true,
                LastLoginDate = DateTime.Now,
                SecurityStamp = adminId.ToString(),
                NormalizedUserName = "datdq@gmail.com".ToUpper(),
                NormalizedEmail = "datdq@gmail.com".ToUpper()
            };

            admin.PasswordHash = passwordHasher.HashPassword(admin, "datdq@123");

            // Seed Roles
            var roles = new List<Role>
            {
                new Role
                {
                    Id = roleUserId,
                    ConcurrencyStamp = roleUserId.ToString(),
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                new Role
                {
                    Id = roleAdminId,
                    ConcurrencyStamp = roleAdminId.ToString(),
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            };

            /*// Seed UserRoles
            var userRole = new IdentityUserRole<Guid>
            {
                UserId = userId,
                RoleId = roleUserId
            };*/

            var adminRole = new IdentityUserRole<Guid>
            {
                UserId = adminId,
                RoleId = roleAdminId
            };

            /*// Seed Campaigns
            var campaign = new Campaign
            {
                CampaignId = campaignId,
                CampaignCode = 123,
                CampaignTitle = "Charity Campaign",
                CampaignThumbnail = "path/to/thumbnail.jpg",
                CampaignDescription = "This is a charity campaign.",
                TargetAmount = 10000.00m,
                CurrentAmount = 5000.00m,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                PartnerName = "Partner Organization",
                PartnerLogo = "path/to/logo.jpg",
                CampaignStatus = "InProgress",
                PartnerNumber = "0987654321",
                DateCreated = DateTime.Now
            };

            // Seed AdminActions
            var adminAction = new AdminAction
            {
                ActionId = actionId,
                AdminId = adminId,
                ActionType = "BanUser",
                TargetUserId = userId,
                TargetCampaignId = campaignId,
                CompletedAt = DateTime.Now
            };

            // Seed Donations
            var donation = new Donation
            {
                DonationId = donationId,
                UserId = userId,
                CampaignId = campaignId,
                Amount = 100.00m,
                PaymentMethod = "Paypal",
                IsConfirm = true,
                DateDonated = DateTime.Now
            };

            // Seed UserFollows
            var userFollow = new UserFollows
            {
                UserId = userId,
                CampaignId = campaignId,
                IsNotified = true,
                DateFollowed = DateTime.Now
            };

            // Add Data Using HasData
            modelBuilder.Entity<User>().HasData(user, admin);*/
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<User>().HasData(admin);
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(adminRole);
            /*modelBuilder.Entity<Campaign>().HasData(campaign);
            modelBuilder.Entity<AdminAction>().HasData(adminAction);
            modelBuilder.Entity<Donation>().HasData(donation);
            modelBuilder.Entity<UserFollows>().HasData(userFollow);*/
        }
    }
}


// -------------------------------------- 3h51 - 24/8/2024 --------------------------------------------

////using CharityHub.Data.Models;
////using Microsoft.AspNetCore.Identity;
////using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
////using Microsoft.EntityFrameworkCore;

////namespace CharityHub.Data.Data
////{
////    public class CharityHubDbContext : IdentityDbContext<User, Role, Guid>
////    {
////        public DbSet<Donation> Donations { get; set; }
////        public DbSet<Campaign> Campaigns { get; set; }
////        public DbSet<AdminAction> AdminActions { get; set; }
////        public DbSet<UserFollows> UserFollows { get; set; }

////        //public CharityHubDbContext(DbContextOptions<CharityHubDbContext> options) : base(options)
////        //{
////        //}

////        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
////        {
////            if (!optionsBuilder.IsConfigured)
////            {
////                optionsBuilder.UseSqlServer("Server=DESKTOP-NDJ53P8\\SQLSERVER2022;Database=CharityAppDb;Trusted_Connection=True;TrustServerCertificate=True;");
////            }
////        }

////        protected override void OnModelCreating(ModelBuilder modelBuilder)
////        {
////            base.OnModelCreating(modelBuilder);

////            modelBuilder.Entity<User>()
////                .Property(u => u.UserName)
////                .HasMaxLength(256)
////                .IsRequired();

////            modelBuilder.Entity<User>()
////            .Property(u => u.Email)
////            .HasMaxLength(256)
////            .IsRequired();

////            modelBuilder.Entity<User>()
////                .Property(u => u.PasswordHash)
////                .HasColumnType("nvarchar(MAX)")
////                .IsRequired();

////            modelBuilder.Entity<User>()
////                .Property(u => u.PhoneNumber)
////                .HasColumnType("varchar(10)");

////            modelBuilder.Entity<Donation>()
////                .HasOne(d => d.User)
////                .WithMany(u => u.Donations)
////                .HasForeignKey(d => d.UserId);

////            modelBuilder.Entity<Donation>()
////                .HasOne(d => d.Campaign)
////                .WithMany(c => c.Donations)
////                .HasForeignKey(d => d.CampaignId);

////            modelBuilder.Entity<UserFollows>()
////            .HasKey(uf => new { uf.UserId, uf.CampaignId });

////            modelBuilder.Entity<UserFollows>()
////                .HasOne(uf => uf.User)
////                .WithMany(u => u.UserFollows)
////                .HasForeignKey(uf => uf.UserId);

////            modelBuilder.Entity<UserFollows>()
////                .HasOne(uf => uf.Campaign)
////                .WithMany(c => c.UserFollows)
////                .HasForeignKey(uf => uf.CampaignId);

////            modelBuilder.Entity<AdminAction>()
////                .HasOne(aa => aa.Admin)
////                .WithMany(u => u.AdminActions)
////                .HasForeignKey(aa => aa.AdminId)
////                .OnDelete(DeleteBehavior.Restrict);

////            modelBuilder.Entity<AdminAction>()
////                .HasOne(aa => aa.TargetUser)
////                .WithMany()
////                .HasForeignKey(aa => aa.TargetUserId)
////                .OnDelete(DeleteBehavior.Restrict);

////            modelBuilder.Entity<AdminAction>()
////                .HasOne(aa => aa.TargetCampaign)
////                .WithMany()
////                .HasForeignKey(aa => aa.TargetCampaignId)
////                .OnDelete(DeleteBehavior.Restrict);

////            var userId = Guid.NewGuid();
////            var adminId = Guid.NewGuid();
////            var roleUserId = Guid.NewGuid();
////            var roleAdminId = Guid.NewGuid();
////            var campaignId = Guid.NewGuid();
////            var actionId = Guid.NewGuid();
////            var donationId = Guid.NewGuid();
////            var followId = Guid.NewGuid();

////            var passwordHasher = new PasswordHasher<User>();

////            // Tạo dữ liệu cho user
////            var user = new User
////            {
////                Id = userId,
////                UserName = "datdq@gmail.com",
////                Email = "datdq@gmail.com",
////                PhoneNumber = "0123456789",
////                DateCreated = DateTime.Now,
////                IsActive = true,
////                LastLoginDate = DateTime.Now
////            };

////            user.PasswordHash = passwordHasher.HashPassword(user, "dat@123");

////            // Tạo dữ liệu cho admin
////            var admin = new User
////            {
////                Id = adminId,
////                UserName = "Anv@gmail.com",
////                Email = "Anv@gmail.com",
////                PhoneNumber = "0987654321",
////                DateCreated = DateTime.Now,
////                IsActive = true,
////                LastLoginDate = DateTime.Now
////            };
////            admin.PasswordHash = passwordHasher.HashPassword(admin, "Anv@123");

////            // Seed Roles
////            var roleUser = new Role
////            {
////                Id = roleUserId,
////                Name = "User"
////            };

////            var roleAdmin = new Role
////            {
////                Id = roleAdminId,
////                Name = "Admin"
////            };

////            // Seed UserRoles
////            var userRole = new IdentityUserRole<Guid>
////            {
////                UserId = userId,
////                RoleId = roleUserId
////            };

////            var adminRole = new IdentityUserRole<Guid>
////            {
////                UserId = adminId,
////                RoleId = roleAdminId
////            };

////            // Seed Campaigns
////            var campaign = new Campaign
////            {
////                CampaignId = campaignId,
////                CampaignCode = 123,
////                CampaignTitle = "Charity Campaign",
////                CampaignThumbnail = "path/to/thumbnail.jpg",
////                CampaignDescription = "This is a charity campaign.",
////                TargetAmount = 10000.00m,
////                CurrentAmount = 5000.00m,
////                StartDate = DateTime.Now,
////                EndDate = DateTime.Now.AddMonths(1),
////                PartnerName = "Partner Organization",
////                PartnerLogo = "path/to/logo.jpg",
////                CampaignStatus = "InProgress",
////                PartnerNumber = "0987654321",
////                DateCreated = DateTime.Now
////            };

////            // Seed AdminActions
////            var adminAction = new AdminAction
////            {
////                ActionId = actionId,
////                AdminId = adminId,
////                ActionType = "BanUser",
////                TargetUserId = userId,
////                TargetCampaignId = campaignId,
////                CompletedAt = DateTime.Now
////            };

////            // Seed Donations
////            var donation = new Donation
////            {
////                DonationId = donationId,
////                UserId = userId,
////                CampaignId = campaignId,
////                Amount = 100.00m,
////                PaymentMethod = "Paypal",
////                IsConfirm = true,
////                DateDonated = DateTime.Now
////            };

////            // Seed UserFollows
////            var userFollow = new UserFollows
////            {
////                UserId = userId,
////                CampaignId = campaignId,
////                IsNotified = true,
////                DateFollowed = DateTime.Now
////            };

////            // Add Data Using HasData
////            modelBuilder.Entity<User>().HasData(user, admin);
////            modelBuilder.Entity<Role>().HasData(roleUser, roleAdmin);
////            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(userRole, adminRole);
////            modelBuilder.Entity<Campaign>().HasData(campaign);
////            modelBuilder.Entity<AdminAction>().HasData(adminAction);
////            modelBuilder.Entity<Donation>().HasData(donation);
////            modelBuilder.Entity<UserFollows>().HasData(userFollow);
////        }
////    }
////}

//using CharityHub.Data.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace CharityHub.Data.Data
//{
//    public class CharityHubDbContext : IdentityDbContext<User, Role, Guid>
//    {
//        public DbSet<Donation> Donations { get; set; }
//        public DbSet<Campaign> Campaigns { get; set; }
//        public DbSet<AdminAction> AdminActions { get; set; }
//        public DbSet<UserFollows> UserFollows { get; set; }

//        public CharityHubDbContext(DbContextOptions<CharityHubDbContext> options) : base(options)
//        {
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlServer("Server=DESKTOP-NDJ53P8\\SQLSERVER2022;Database=CharityAppDb2;Trusted_Connection=True;TrustServerCertificate=True;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<User>()
//                .Property(u => u.UserName)
//                .HasMaxLength(256)
//                .IsRequired();

//            modelBuilder.Entity<User>()
//                .Property(u => u.Email)
//                .HasMaxLength(256)
//                .IsRequired();

//            modelBuilder.Entity<User>()
//                .Property(u => u.PasswordHash)
//                .HasColumnType("nvarchar(MAX)")
//                .IsRequired();

//            modelBuilder.Entity<User>()
//                .Property(u => u.PhoneNumber)
//                .HasColumnType("varchar(10)");

//            // fluent api
//            modelBuilder.Entity<Donation>()
//                .HasOne(d => d.User)
//                .WithMany(u => u.Donations)
//                .HasForeignKey(d => d.UserId);

//            modelBuilder.Entity<Donation>()
//                .HasOne(d => d.Campaign)
//                .WithMany(c => c.Donations)
//                .HasForeignKey(d => d.CampaignId);

//            modelBuilder.Entity<UserFollows>()
//            .HasKey(uf => new { uf.UserId, uf.CampaignId });

//            modelBuilder.Entity<UserFollows>()
//                .HasOne(uf => uf.User)
//                .WithMany(u => u.UserFollows)
//                .HasForeignKey(uf => uf.UserId);

//            modelBuilder.Entity<UserFollows>()
//                .HasOne(uf => uf.Campaign)
//                .WithMany(c => c.UserFollows)
//                .HasForeignKey(uf => uf.CampaignId);

//            modelBuilder.Entity<AdminAction>()
//                .HasOne(aa => aa.Admin)
//                .WithMany(u => u.AdminActions)
//                .HasForeignKey(aa => aa.AdminId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<AdminAction>()
//                .HasOne(aa => aa.TargetUser)
//                .WithMany()
//                .HasForeignKey(aa => aa.TargetUserId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<AdminAction>()
//                .HasOne(aa => aa.TargetCampaign)
//                .WithMany(c => c.AdminActions)
//                .HasForeignKey(aa => aa.TargetCampaignId)
//                .OnDelete(DeleteBehavior.Restrict);

//            var userId = Guid.NewGuid();
//            var adminId = Guid.NewGuid();
//            var roleUserId = Guid.NewGuid();
//            var roleAdminId = Guid.NewGuid();
//            var campaignId = Guid.NewGuid();
//            var actionId = Guid.NewGuid();
//            var donationId = Guid.NewGuid();
//            var followId = Guid.NewGuid();

//            var passwordHasher = new PasswordHasher<User>();
//            // Tạo dữ liệu cho user
//            var user = new User
//            {
//                Id = userId,
//                UserName = "datdq@gmail.com",
//                Email = "datdq@gmail.com",
//                PhoneNumber = "0123456789",
//                DateCreated = DateTime.Now,
//                IsActive = true,
//                LastLoginDate = DateTime.Now,
//                DisplayName = "Datdq"
//            };
//            user.PasswordHash = passwordHasher.HashPassword(user, "dat@123");


//            // Tạo dữ liệu cho admin
//            var admin = new User
//            {
//                Id = adminId,
//                UserName = "Anv@gmail.com",
//                Email = "Anv@gmail.com",
//                PhoneNumber = "0987654321",
//                DateCreated = DateTime.Now,
//                IsActive = true,
//                LastLoginDate = DateTime.Now,
//                DisplayName = "Anv"
//                //PasswordHash = "dat@123"
//            };
//            admin.PasswordHash = passwordHasher.HashPassword(admin, "dat@123");

//            // Seed Roles
//            var roles = new List<Role>
//            {
//                new Role
//                {
//                    Id = roleUserId,
//                    ConcurrencyStamp = roleAdminId.ToString(),
//                    Name = "User",
//                    NormalizedName = "User".ToUpper()
//                },
//                new Role
//                {
//                    Id = roleAdminId,
//                    ConcurrencyStamp = roleAdminId.ToString(),
//                    Name = "Admin",
//                    NormalizedName = "Admin".ToUpper()
//                }
//            };

//            // Seed UserRoles
//            var userRole = new IdentityUserRole<Guid>
//            {
//                UserId = userId,
//                RoleId = roleUserId
//            };

//            var adminRole = new IdentityUserRole<Guid>
//            {
//                UserId = adminId,
//                RoleId = roleAdminId
//            };

//            // Seed Campaigns
//            var campaign = new Campaign
//            {
//                CampaignId = campaignId,
//                CampaignCode = 123,
//                CampaignTitle = "Charity Campaign",
//                CampaignThumbnail = "path/to/thumbnail.jpg",
//                CampaignDescription = "This is a charity campaign.",
//                TargetAmount = 10000.00m,
//                CurrentAmount = 5000.00m,
//                StartDate = DateTime.Now,
//                EndDate = DateTime.Now.AddMonths(1),
//                PartnerName = "Partner Organization",
//                PartnerLogo = "path/to/logo.jpg",
//                CampaignStatus = "InProgress",
//                PartnerNumber = "0987654321",
//                DateCreated = DateTime.Now
//            };

//            // Seed AdminActions
//            var adminAction = new AdminAction
//            {
//                ActionId = actionId,
//                AdminId = adminId,
//                ActionType = "BanUser",
//                TargetUserId = userId,
//                TargetCampaignId = campaignId,
//                CompletedAt = DateTime.Now
//            };

//            // Seed Donations
//            var donation = new Donation
//            {
//                DonationId = donationId,
//                UserId = userId,
//                CampaignId = campaignId,
//                Amount = 100.00m,
//                PaymentMethod = "Paypal",
//                IsConfirm = true,
//                DateDonated = DateTime.Now
//            };

//            // Seed UserFollows
//            var userFollow = new UserFollows
//            {
//                UserId = userId,
//                CampaignId = campaignId,
//                IsNotified = true,
//                DateFollowed = DateTime.Now
//            };

//            // Add Data Using HasData
//            modelBuilder.Entity<User>().HasData(user, admin);
//            modelBuilder.Entity<Role>().HasData(roles);
//            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(userRole, adminRole);
//            modelBuilder.Entity<Campaign>().HasData(campaign);
//            modelBuilder.Entity<AdminAction>().HasData(adminAction);
//            modelBuilder.Entity<Donation>().HasData(donation);
//            modelBuilder.Entity<UserFollows>().HasData(userFollow);
//        }
//    }
//}
