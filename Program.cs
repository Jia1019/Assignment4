using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using WebApplication.Persistence;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (FamilyContext familyContext = new FamilyContext())
            {
                if (!familyContext.Users.Any()||!familyContext.Interests.Any())
                {
                    Seed(familyContext);
                }
            }
            CreateHostBuilder(args).Build().Run();
        }
        
         private static void Seed(FamilyContext familyContext)
         {
             try
             {
                 User u1 = new User
                 {
                     UserName = "Jia",
                     Password = "123456"
                 };
                 User u2 = new User
                 {
                     UserName = "Alice",
                     Password = "654321"
                 };
                 familyContext.Users.Add(u1);
                 familyContext.SaveChangesAsync();
                 familyContext.Users.Add(u2);
                 familyContext.SaveChangesAsync();
        
                 List<Interest> interests = new[]
                 {
                     new Interest{ChildInterests = null, Type = "Soccer"},
                     new Interest{ChildInterests = null,Type ="Drawing"},
                     new Interest{ChildInterests = null,Type ="Kite Flying"},
                     new Interest{ChildInterests = null,Type ="Roller Blades"},
                     new Interest{ChildInterests = null,Type ="Board Games"},
                     new Interest{ChildInterests = null,Type ="Ballet"},
                     new Interest{ChildInterests = null,Type ="Hockey"},
                     new Interest{ChildInterests = null,Type ="Gaming"},
                     new Interest{ChildInterests = null,Type ="Lego"},
                     new Interest{ChildInterests = null,Type ="Scout"},
                     new Interest{ChildInterests = null,Type ="Gymnastics"},
                     new Interest{ChildInterests = null,Type ="Harry Potter"},
                     new Interest{ChildInterests = null,Type ="Frozen"}
                 }.ToList();
                 foreach (var interest in interests)
                 {
                     familyContext.Interests.Add(interest);
                     familyContext.SaveChangesAsync();
                 }
             }
             catch (Exception e)
             {
                 Console.WriteLine(e);
                 throw;
             }
             
        }
         
         

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}