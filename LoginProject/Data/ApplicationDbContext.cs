using LoginProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginProject.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options) {
    public override DbSet<User> Users { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Entity<Post>().ToTable("Posts");
        builder.Entity<Post>().Property(g => g.CreatedAt).HasDefaultValue(DateTime.MinValue);

        builder.Entity<Post>().HasOne(p => p.Author).WithMany(u => u.Posts).HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Post>().HasOne(p => p.ParentPost).WithMany().HasForeignKey(p => p.ParentPostId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Post>().HasOne(p => p.RootPost).WithMany().HasForeignKey(p => p.RootPostId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<User>().Property(u => u.UserName).HasMaxLength(32);

        // User Seed Data

        Guid adminId = new("11111111-1111-1111-1111-111111111111");

        builder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid> {
            Id = adminId,
            Name = "admin",
            NormalizedName = "ADMIN",
        });

        Guid moderatorId = new("22222222-2222-2222-2222-222222222222");

        builder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid> {
            Id = moderatorId,
            Name = "moderator",
            NormalizedName = "MODERATOR",
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

        builder.Entity<User>(entity => {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            entity.HasData(new User {
                Id = moderatorId,
                FullName = "Moderator User",
                UserName = "moderator",
                NormalizedUserName = "MODERATOR",
                Email = "moderator@local.slhn.cz",
                NormalizedEmail = "moderator@LOCAL.SLHN.CZ",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(new User(), "moderator"),
                SecurityStamp = "ioosdgodof",
            });
        });

        Guid userId = Guid.NewGuid();

        builder.Entity<User>(entity => {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            entity.HasData(new User {
                Id = userId,
                FullName = "Basic User",
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@local.slhn.cz",
                NormalizedEmail = "USER@LOCAL.SLHN.CZ",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(new User(), "user"),
                SecurityStamp = "kjsdgjdgsg",
            });
        });

        builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid> {
            RoleId = adminId,
            UserId = adminId,
        });

        builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid> {
            RoleId = moderatorId,
            UserId = moderatorId,
        });

        // Post Seed Data

        Guid rootPostId1 = Guid.NewGuid();

        builder.Entity<Post>().HasData(new Post {
            PostId = rootPostId1,
            Content = "This is the first post by an admin",
            CreatedAt = DateTime.Now,
            AuthorId = adminId,
        });

        Guid rootPostId2 = Guid.NewGuid();

        builder.Entity<Post>().HasData(new Post {
            PostId = rootPostId2,
            Content = "This is the first post by a moderator",
            CreatedAt = DateTime.Now,
            AuthorId = moderatorId,
        });

        Guid postId1 = Guid.NewGuid();

        builder.Entity<Post>().HasData(new Post {
            PostId = postId1,
            Content = "This is a first reply to the first post by an admin",
            CreatedAt = DateTime.Now,
            AuthorId = userId,
            ParentPostId = rootPostId1,
            RootPostId = rootPostId1,
        });

        Guid postId2 = Guid.NewGuid();

        builder.Entity<Post>().HasData(new Post {
            PostId = postId2,
            Content = "This is a second reply to the first post by an admin",
            CreatedAt = DateTime.Now,
            AuthorId = userId,
            ParentPostId = rootPostId1,
            RootPostId = rootPostId1,
        });

        Guid postId3 = Guid.NewGuid();

        builder.Entity<Post>().HasData(new Post {
            PostId = postId3,
            Content = "This is a first reply to the first post by a moderator",
            CreatedAt = DateTime.Now,
            AuthorId = userId,
            ParentPostId = rootPostId2,
            RootPostId = rootPostId2,
        });

        Guid postId4 = Guid.NewGuid();

        builder.Entity<Post>().HasData(new Post {
            PostId = postId4,
            Content = "This is a second reply to the first post by a moderator",
            CreatedAt = DateTime.Now,
            AuthorId = userId,
            ParentPostId = rootPostId2,
            RootPostId = rootPostId2,
        });

        Guid postId5 = Guid.NewGuid();

        builder.Entity<Post>().HasData(new Post {
            PostId = postId5,
            Content = "This is a reply to the first reply to the first post by an admin",
            CreatedAt = DateTime.Now,
            AuthorId = userId,
            ParentPostId = postId1,
            RootPostId = rootPostId1,
        });

        // Real Post Seed Data

        Guid rRootPostId = Guid.NewGuid();
        Guid rR1L1V1PostId = Guid.NewGuid();
        Guid rR1L2V1PostId = Guid.NewGuid();
        Guid rR1L3V1PostId = Guid.NewGuid();
        Guid rR1L4V1PostId = Guid.NewGuid();
        Guid rR1L3V2PostId = Guid.NewGuid();
        Guid rR2L1V1PostId = Guid.NewGuid();
        Guid rR2L2V1PostId = Guid.NewGuid();

        builder.Entity<Post>().HasData(new Post {
            PostId = rRootPostId,
            Content = "Já jsem teď trochu v ráži, ale může mi někdo vysvětlit ten obrovskej rozdíl mezi čistou a hrubou mzdou? Jako za co reálně odvádim desetitisíce? Já mám pocit, že za to dostávám úplný h***.",
            CreatedAt = DateTime.Now,
            AuthorId = adminId,
        });

        builder.Entity<Post>().HasData(new Post {
            PostId = rR1L1V1PostId,
            Content = "Zajímavé jak takovýto příspěvek vyvolává další a další negativní reakce.\r\nJe to pochopitelné. Ideál, který by navíc vyhovoval všem, neexistuje.😉\r\nVždy je co zlepšovat.\r\nNicméně všem stěžovatelům bych vždy doporučil aby se alespoň porozhlédli a srovnali si stav věcí u nás a v jiných zemích.\r\nZřejmě by nakonec byli docela překvapení, jaká může být realita.\r\nTím neříkám, že mi nic nevadí, ale planě nadávat nikam prostě nevede.\r\nA neštěstí je, že většina stěžovatelů potom navíc věří populistům.\U0001f937",
            CreatedAt = DateTime.Now,
            AuthorId = userId,
            ParentPostId = rRootPostId,
            RootPostId = rRootPostId,
        });

        builder.Entity<Post>().HasData(new Post {
            PostId = rR1L2V1PostId,
            Content = "Já určitě nejsem za na všechno nadávat, na druhou stranu, tohle je “můj” prostor, kde si s dávkou nadsázky můžu ulevit a nevidim důvod proč ne.☺️",
            CreatedAt = DateTime.Now,
            AuthorId = adminId,
            ParentPostId = rR1L1V1PostId,
            RootPostId = rRootPostId,
        });

        builder.Entity<Post>().HasData(new Post {
            PostId = rR1L3V1PostId,
            Content = "Nic proti.\r\nMá poznámka byla k tomu, jaké reakce to následně vyvolává.\r\nNic míň, nic víc.\r\nAť se ti daří.😉",
            CreatedAt = DateTime.Now,
            AuthorId = userId,
            ParentPostId = rR1L2V1PostId,
            RootPostId = rRootPostId,
        });

        builder.Entity<Post>().HasData(new Post {
            PostId = rR1L4V1PostId,
            Content = "Já to ani neberu a nemyslim nijak zle. Ale nemyslím si, že těch 5 lidí, kteří reagovali je nějaké šíření negativity. ☺️",
            CreatedAt = DateTime.Now,
            AuthorId = adminId,
            ParentPostId = rR1L3V1PostId,
            RootPostId = rRootPostId,
        });

        builder.Entity<Post>().HasData(new Post {
            PostId = rR1L3V2PostId,
            Content = "T*l píšou ti tu boti 🤣🤣🤣🤣",
            CreatedAt = DateTime.Now,
            AuthorId = moderatorId,
            ParentPostId = rR1L2V1PostId,
            RootPostId = rRootPostId,
        });

        builder.Entity<Post>().HasData(new Post {
            PostId = rR2L1V1PostId,
            Content = "Použij google.",
            CreatedAt = DateTime.Now,
            AuthorId = moderatorId,
            ParentPostId = rRootPostId,
            RootPostId = rRootPostId,
        });

        builder.Entity<Post>().HasData(new Post {
            PostId = rR2L2V1PostId,
            Content = "Zkusila jsem, nepomohlo",
            CreatedAt = DateTime.Now,
            AuthorId = adminId,
            ParentPostId = rR2L1V1PostId,
            RootPostId = rRootPostId,
        });
    }
}
