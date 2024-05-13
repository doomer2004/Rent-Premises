using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rent_Premises.DAL.Entities;
using Rent_Premises.DAL.Entities.Base;

namespace Rent_Premises.DAL.Context;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public virtual DbSet<Lessor> Lessors { get; set; }
    public virtual DbSet<Renter> Renters { get; set; }
    public virtual DbSet<Premises> Premises { get; set; }
    public virtual DbSet<TypeOfPremises> TypeOfPremises { get; set; }
    
}