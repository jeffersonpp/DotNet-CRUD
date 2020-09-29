using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Client.Data;
using System;
using System.Linq;

namespace Client.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ClientContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ClientContext>>()))
            {
                // Look for any client.
                if (context.Clients.Any())
                {
                    return;   // DB has been seeded
                }

                context.Clients.AddRange(
                    new mClient
                    {
                        Name = "Jefferson Lopes de Sousa",
                        Address = "4001 221st Pl Sw, Bothell - WA",
                        Phone = "856 345 4979",
                        Email = "jeffersonpp@gmail.com"
                    },

                    new mClient
                    {
                        Name = "Delete Me",
                        Address = "Any One",
                        Phone = " 123 456 7890",
                        Email = "jeffersonsousa0707@gmail.com"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}