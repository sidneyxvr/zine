using Argon.Zine.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Argon.Zine.App.Api.Tests.Setup;

public static class IdentitySetup
{
    public static IServiceProvider CreateIdentityDatabase(this IServiceProvider services)
    {
        var identityContext = services.GetRequiredService<IdentityContext>();
        identityContext.Database.EnsureDeleted();
        identityContext.Database.EnsureCreated();

        SeedIdentityDatabase(identityContext);

        return services;
    }

    public static void SeedIdentityDatabase(IdentityContext context)
    {
        context.Database.ExecuteSqlRaw("INSERT [dbo].[User] ([Id], [FirstName], [Surname], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0a118de1-08b3-4eac-8204-08da07c1a929', N'Test', N'Test', 1, N'non_confirmed_email@email.com', N'NON_CONFIRMED_EMAIL@EMAIL.COM', N'non_confirmed_email@email.com', N'NON_CONFIRMED_EMAIL@EMAIL.COM', 0, N'AQAAAAEAACcQAAAAENixlQac04QxJMXa9qikfrpwViRd3Hr2bWbGlKcUwiYzqiCVG1rnb1o/RZR21tuJxA==', N'DZDX7LKONL75NQYG5JHQFN257HUBLGRA', N'a89fd611-13b6-4a78-b35f-2941e9af9c22', NULL, 0, 0, NULL, 1, 0)");
        context.Database.ExecuteSqlRaw("INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'0a118de1-08b3-4eac-8204-08da07c1a929', N'3215ca3d-ec71-4df4-bf41-555ffcc04f22')");
        
        context.Database.ExecuteSqlRaw("INSERT [dbo].[User] ([Id], [FirstName], [Surname], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7df063d1-52b3-48ce-4dec-08da07c9ece1', N'Test', N'Test', 1, N'confirmed_email@email.com', N'CONFIRMED_EMAIL@EMAIL.COM', N'confirmed_email@email.com', N'CONFIRMED_EMAIL@EMAIL.COM', 1, N'AQAAAAEAACcQAAAAECr7D2IV1KVp8xOv73ptfMUyllE37sGEu5lHK9ZAOth6/0qc1i0nChL/pMiZYpeKVA==', N'ZB6KHDEMZK2GESLLXTTYJBKKEOHXG22T', N'b05de469-2d4c-4d81-a391-b991a45fe6c6', NULL, 0, 0, NULL, 1, 0)");
        context.Database.ExecuteSqlRaw("INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'7df063d1-52b3-48ce-4dec-08da07c9ece1', N'07e778b6-7c1d-4852-b1f4-63661e4bb08a')");

        context.Database.ExecuteSqlRaw("INSERT [dbo].[User] ([Id], [FirstName], [Surname], [IsActive], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fd24e959-7ad6-4922-919f-08da0f55564e', N'User', N'Test', 1, N'user-test@email.com', N'USER-TEST@EMAIL.COM', N'user-test@email.com', N'USER-TEST@EMAIL.COM', 1, N'AQAAAAEAACcQAAAAEPmxEMcV0DXtFT7P/ckSMGJpg+OJT2bB9pClP9G4qdVDg4BGFq3YNKxDISbCGHxIIQ==', N'S333QKQ4L5TEREKV5HW2BEEOAYPUUTWS', N'cb07715d-ea9b-4a71-a235-0524eacdb0fe', NULL, 0, 0, NULL, 1, 0)");
        context.Database.ExecuteSqlRaw("INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'fd24e959-7ad6-4922-919f-08da0f55564e', N'78DF47E2-8160-4678-99B0-2E80492268DD')");
    }
}