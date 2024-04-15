using LoginProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginProject.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options) {
    public override DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();

        Guid adminId = new("11111111-1111-1111-1111-111111111111");

        builder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid> {
            Id = adminId,
            Name = "admin",
            NormalizedName = "ADMIN",
        });

        builder.Entity<User>(entity => {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            entity.HasData(new User {
                Id = adminId,
                FullName = "Administrator User",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@local.slhn.cz",
                NormalizedEmail = "ADMIN@LOCAL.SLHN.CZ",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(new User(), "admin"),
                SecurityStamp = "Asdfiasjfisda",
            });
        });

        builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid> {
            RoleId = adminId,
            UserId = adminId,
        });
    }
}
