using API.Models;
using API.Utility;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts;

public class BookingManagementDbContext : DbContext
{
    public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options)
    {

    }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<University> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder Builder)
    {
        base.OnModelCreating(Builder);

        Builder.Entity<Role>().HasData(
        new Role
        {
            Guid = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Name = nameof(RoleLevel.User),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        },
        new Role
        {
            Guid = Guid.Parse("158f7caf-f4d2-45ad-4c30-08db58db1641"),
            Name = nameof(RoleLevel.Manager),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        },
        new Role
        {
            Guid = Guid.Parse("6f0cab9c-77ee-4720-fbe7-08db584bbbc0"),
            Name = nameof(RoleLevel.Admin),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        });

        Builder.Entity<Employee>().HasIndex(e => new
        {
            e.Nik,
            e.Email,
            e.PhoneNumber
        }).IsUnique();

        // Relasi dari Education dan university one to many

        Builder.Entity<Education>().HasOne(u => u.University)
            .WithMany(e => e.Educations)
            .HasForeignKey(e => e.UniversityGuid);

        // Relasi dari Education Guid dan Employee one to one
        Builder.Entity<Education>().HasOne(e => e.Employee)
            .WithOne(e => e.Education)
            .HasForeignKey<Education>(e => e.Guid);

        Builder.Entity<Account>().HasOne(e => e.Employee)
            .WithOne(a => a.Account)
            .HasForeignKey<Account>(a => a.Guid);

        Builder.Entity<AccountRole>().HasOne(a => a.Account)
            .WithMany(a => a.AccountRoles)
            .HasForeignKey(a => a.AccountGuid);

        Builder.Entity<AccountRole>().HasOne(r => r.Role)
            .WithMany(a => a.AccountRoles)
            .HasForeignKey(a => a.RoleGuid);

        Builder.Entity<Booking>().HasOne(r => r.Room)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.RoomGuid);

        Builder.Entity<Booking>().HasOne(e => e.Employee)
            .WithMany(b => b.Bookings)
            .HasForeignKey(b => b.EmployeeGuid);


    }

    
}
