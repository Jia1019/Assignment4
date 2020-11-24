using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using WebApplication.Persistence;

namespace Assignment.Data
{
    public class SqliteFamilyService : IFamilyService
    {
        private FamilyContext ctx;
        private string FamilyError;

        public SqliteFamilyService()
        {
            ctx = new FamilyContext();
            FamilyError = null;
        }
        public async Task<IList<Family>> GetFamilies()
        {
            var families = await ctx.Families.ToListAsync();
            foreach (var family in families)
            {
                family.Adults = await GetFamilyAdultByFamilyId(family.Id);
                family.Children = await GetFamilyChildByFamilyId(family.Id);
                family.Pets = await GetFamilyPetByFamilyId(family.Id);
            }
            return families;
        }

        private async Task<List<FamilyAdult>> GetFamilyAdultByFamilyId(int id)
        {
            var familyAdults = ctx.Families.SelectMany(f => f.Adults).Where(a => a.FamilyID == id).ToList();
            foreach (var familyAdult in familyAdults)
            {
                familyAdult.Adult = await getAdultById(familyAdult.AdultID);
            }
            Console.WriteLine(JsonSerializer.Serialize(familyAdults));
            if (familyAdults.Count==0)
            {
                return new List<FamilyAdult>();
            }
            else
            {
                return new List<FamilyAdult>(familyAdults);
            }
        }
        
        private async Task<List<FamilyChild>> GetFamilyChildByFamilyId(int id)
        {
            var familyChildren = ctx.Families.SelectMany(f => f.Children).Where(a => a.FamilyID == id).ToList();
            foreach (var familyChild in familyChildren)
            {
                familyChild.Child = await getChildById(familyChild.ChildID);
            }

            return familyChildren;
        }
        
        private async Task<List<Pet>> GetFamilyPetByFamilyId(int id)
        {
            var familyPets = ctx.Families.Where(f=>f.Id == id).SelectMany(f=>f.Pets).ToList();
            for (int i = 0; i < familyPets.Count; i++)
            {
                familyPets[i] = await GetPetById(familyPets[i].Id);
            }

            return familyPets;
        }

        private async Task<Pet> GetPetById(int id)
        {
            return await ctx.Pets.FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public async Task<IList<Adult>> GetAdults()
        {
            return await ctx.Adults.ToListAsync();
        }

        public async Task<IList<Child>> GetChildren()
        {
            var children = await ctx.Children.ToListAsync();
            foreach (var child in children)
            {
                child.ChildInterests = await GetChildInterestByChildId(child.Id);
            }
            return children;
        }

        private async Task<IList<ChildInterest>> GetChildInterestByChildId(int id)
        {
            var childIntersts = ctx.Children.SelectMany(f => f.ChildInterests).Where(a => a.ChildID == id).ToList();
            foreach (var familyChild in childIntersts)
            {
                familyChild.Interest = await GetInterestByType(familyChild.InterestType);
            }

            return childIntersts;
        }

        private async Task<Interest> GetInterestByType(string type)
        {
            return (await ctx.Interests.FirstOrDefaultAsync(i => i.Type == type));
        }

        public async Task<Adult> getAdultById(int Id)
        {
            return (await ctx.Adults.FirstOrDefaultAsync(a => a.Id == Id));
        }

        public async Task<Child> getChildById(int Id)
        {
            var child = await ctx.Children.FirstOrDefaultAsync(c => c.Id == Id);
            child.ChildInterests = await GetChildInterestByChildId(Id);
            return child;
        }

        public async Task AddFamily(Family family)
        {
            FamilyError = null;
            IList<Family> families = await ctx.Families.ToListAsync();
            foreach (var item in families)
            {
                if (item.HouseNumber == family.HouseNumber && item.StreetName == family.StreetName)
                {
                    FamilyError = "The house already has a family";
                    break;
                }
            }
            if (FamilyError == null)
            {
                if (families.Count==0)
                {
                    family.Id = 1;
                    EntityEntry<Family> newlyAdded = await ctx.Families.AddAsync(family);
                    await ctx.SaveChangesAsync();
                }
                else
                {
                    int max = families.Max(family => family.Id);
                    family.Id = (++max);
                    EntityEntry<Family> newlyAdded = await ctx.Families.AddAsync(family);
                    await ctx.SaveChangesAsync();
                    
                }
            }
            
        }

        public async Task<string> AddAdult(Adult adult)
        {
           // var a = (await ctx.Adults.FirstOrDefaultAsync(a => a.Id == adult.Id));
            if ((await ctx.Adults.FirstOrDefaultAsync(a => a.Id == adult.Id)) == null 
                && (await ctx.Children.FirstOrDefaultAsync(c => c.Id == adult.Id))==null)
            {
                EntityEntry<Adult> newlyAdded = await ctx.Adults.AddAsync(adult);
                await ctx.SaveChangesAsync();
                return null;
            }
            else
            {
                return "The person(id) has already existed";
            }
        }

        public async Task<string> AddChildren(Child child)
        {
            if ((await ctx.Adults.FirstOrDefaultAsync(a => a.Id == child.Id)) == null &&
                (await ctx.Children.FirstOrDefaultAsync(c => c.Id == child.Id)) == null)
            {
                EntityEntry<Child> newlyAdded = await ctx.Children.AddAsync(child);
                await ctx.SaveChangesAsync();
                return null;
            }
            else
            {
                return "The person(id) has already existed";
            }
        }

        public async Task RemoveFamily(int familyId)
        {
            Family toDelete = await ctx.Families.FirstOrDefaultAsync(f => f.Id== familyId);
            if (toDelete != null)
            {
                ctx.Families.Remove(toDelete);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task RemoveAdult(int adultId)
        {
            Adult toDelete = await ctx.Adults.FirstOrDefaultAsync(a => a.Id== adultId);
            if (toDelete != null)
            {
                ctx.Adults.Remove(toDelete);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task RemoveChild(int childId)
        {
            Child toDelete = await ctx.Children.FirstOrDefaultAsync(c => c.Id== childId);
            if (toDelete != null)
            {
                ctx.Children.Remove(toDelete);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task UpdateFamily(Family family)
        {
            try
            {
                Family toUpdate = await ctx.Families.FirstAsync(t => t.Id == family.Id);
                toUpdate.Id = family.Id;
                toUpdate.StreetName = family.StreetName;
                toUpdate.HouseNumber = family.HouseNumber;
                toUpdate.Adults = family.Adults;
                toUpdate.Children = family.Children;
                toUpdate.Pets = family.Pets;
                ctx.Update(toUpdate);
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Did not find family with id{family.Id}");
            }
        }

        public async Task UpdateAdult(Adult adult)
        {
            try
            {
                Adult toUpdate = await ctx.Adults.FirstAsync(t => t.Id == adult.Id);
                toUpdate.Id = adult.Id;
                toUpdate.Age = adult.Age;
                toUpdate.Height = adult.Height;
                toUpdate.Sex = adult.Sex;
                toUpdate.Weight = adult.Weight;
                toUpdate.EyeColor = adult.EyeColor;
                toUpdate.FirstName = adult.FirstName;
                toUpdate.LastName = adult.LastName;
                toUpdate.JobTitle = adult.JobTitle;
                toUpdate.HairColor = adult.HairColor;
                ctx.Update(toUpdate);
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Did not find adult with id{adult.Id}");
            }
        }

        public async Task UpdateChild(Child child)
        {
            try
            {
                Child toUpdate = await ctx.Children.FirstAsync(t => t.Id == child.Id);
                toUpdate.Id = child.Id;
                toUpdate.Age = child.Age;
                toUpdate.Height = child.Height;
                toUpdate.Sex = child.Sex;
                toUpdate.Weight = child.Weight;
                toUpdate.EyeColor = child.EyeColor;
                toUpdate.FirstName = child.FirstName;
                toUpdate.LastName = child.LastName;
                toUpdate.Pets = child.Pets;
                toUpdate.ChildInterests = child.ChildInterests;
                toUpdate.HairColor = child.HairColor;
                ctx.Update(toUpdate);
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Did not find child with id{child.Id}");
            }
        }

        public async Task<string> getFamilyError()
        {
            return FamilyError;
        }

        public async Task<IList<User>> GetUsers()
        {
            return await ctx.Users.ToListAsync();
        }
    }
}