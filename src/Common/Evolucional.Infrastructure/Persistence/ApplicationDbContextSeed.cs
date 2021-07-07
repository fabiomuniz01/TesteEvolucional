using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Evolucional.Infrastructure.Identity;
using Evolucional.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Evolucional.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var defaultUser = new ApplicationUser { UserName = "candidato-evolucional", Email = "candidato-evolucional" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "123456");
                await userManager.AddToRolesAsync(defaultUser, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedDisciplinaDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Disciplinas.Any())
            {
                await context.Disciplinas.AddAsync(new Disciplina(){Nome = "Matemática"});
                await context.Disciplinas.AddAsync(new Disciplina(){Nome = "Português"});
                await context.Disciplinas.AddAsync(new Disciplina(){Nome = "História"});
                await context.Disciplinas.AddAsync(new Disciplina(){Nome = "Geografia"});
                await context.Disciplinas.AddAsync(new Disciplina(){Nome = "Inglês"});
                await context.Disciplinas.AddAsync(new Disciplina(){Nome = "Biologia"});
                await context.Disciplinas.AddAsync(new Disciplina(){Nome = "Filosofia"});
                await context.Disciplinas.AddAsync(new Disciplina(){Nome = "Física"});
                await context.Disciplinas.AddAsync(new Disciplina(){Nome = "Química"});

                await context.SaveChangesAsync();
            }
        }
    }
}
