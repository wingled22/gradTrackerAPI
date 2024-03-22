

# .NET 7 Minimal API with Identity and Custom Columns

## Step 1: Install Required NuGet Packages

```bash
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

## Step 2: Configure Database Connection

Add a connection string to your `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YourDatabaseName;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  // other configurations...
}
```

## Step 3: Configure Identity and Authentication in `Program.cs`

Update `Program.cs` to include identity and authentication:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "your-issuer",
        ValidAudience = "your-audience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
    };
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Map controllers, configure routes, etc.

app.Run();
```

## Step 4: Create Database Context

Create a class for your `DbContext`. Add a file, e.g., `ApplicationDbContext.cs`:

```csharp
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityUser<Guid>>().ToTable("Users").HasKey(u => u.Id);
        builder.Entity<IdentityRole<Guid>>().ToTable("Roles").HasKey(r => r.Id);
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(ur => new { ur.UserId, ur.RoleId });
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims").HasKey(uc => uc.Id);
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims").HasKey(rc => rc.Id);
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
    }
}
```

## Step 5: Apply Migrations

Run the following commands:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Step 6: Use Custom Columns in `ApplicationUser`

Create a custom `ApplicationUser` class:

```csharp
using Microsoft.AspNetCore.Identity;
using System;

public class ApplicationUser : IdentityUser<Guid>
{
    // Add your custom properties here
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

Update `ApplicationDbContext` to use `ApplicationUser`:

```csharp
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    // ... existing code ...
}
```

Update `Startup.cs` to use `ApplicationUser`:

```csharp
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
```

## Step 7: Add and Update Custom Columns

After migrating, add and update your custom columns in the `ApplicationUser` class. For example, to add a new property `PhoneNumber`:

```csharp
public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}
```

Run migrations again:

```bash
dotnet ef migrations add UpdateCustomColumns
dotnet ef database update
```

Now, your `IdentityUser` has custom columns, and you can use these properties in your application as needed.
```

Feel free to copy and paste this into a Markdown file or your documentation. If you have any further questions or need clarification on any steps, feel free to ask!