﻿﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.Json;
using Models;
using System.ComponentModel.DataAnnotations;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Mvc.Diagnostics;


namespace Assignment.Data
{
    public class FamilyService : IFamilyService
    {
        private string familyFile = "families.json";
        private string adultFile = "adult.json";
        private string childFile = "child.json";
        private string userFile = "users.json";
        private IList<Family> families;
        private IList<Adult> adults;
        private IList<Child> children;
        private IList<User> users;
        private string FamilyError;

        public FamilyService()
        {
            families = new List<Family>();
            adults = new List<Adult>();
            children = new List<Child>();
            FamilyError = null;
            if (!File.Exists(familyFile))
            {
                WriteFamiliesToFile();
            }
            else
            {
                string content = File.ReadAllText(familyFile);
                families = JsonSerializer.Deserialize<List<Family>>(content);
            }

            if (!File.Exists(adultFile))
            {
                WriteAdultToFile();
            }
            else
            {
                string content = File.ReadAllText(adultFile);
                adults = JsonSerializer.Deserialize<List<Adult>>(content);
            }

            if (!File.Exists(childFile))
            {
                WriteChildToFile();
            }
            else
            {
                string content = File.ReadAllText(childFile);
                children = JsonSerializer.Deserialize<List<Child>>(content);
            }

            if (!File.Exists(userFile))
            {
                Seed();
                string productsAsJson = JsonSerializer.Serialize(users);
                File.WriteAllText(userFile, productsAsJson);
            }
            else
            {
                string content = File.ReadAllText(userFile);
                users = JsonSerializer.Deserialize<List<User>>(content);
            }
        }

        public async Task<string> getFamilyError()
        {
            return FamilyError;
        }

        private void WriteChildToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(children);
            File.WriteAllText(childFile, productsAsJson);
        }

        private void WriteAdultToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(adults);
            File.WriteAllText(adultFile, productsAsJson);
        }

        private void WriteFamiliesToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(families);
            File.WriteAllText(familyFile, productsAsJson);
        }

        public async Task<IList<Family>> GetFamilies()
        {
            List<Family> family = new List<Family>(families);
            return family;
        }

        public async Task<IList<User>> GetUsers()
        {
            List<User> user = new List<User>(users);
            return user;
        }

        public async Task<IList<Adult>> GetAdults()
        {
            List<Adult> adult = new List<Adult>(adults);
            return adult;
        }

        public async Task<IList<Child>> GetChildren()
        {
            List<Child> child = new List<Child>(children);
            return child;
        }

        public async Task AddFamily(Family family)
        {
            FamilyError = null;
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
                    families.Add(family);
                    WriteFamiliesToFile();
                }
                else
                {
                    int max = families.Max(family => family.Id);
                                    family.Id = (++max);
                                    families.Add(family);
                                    WriteFamiliesToFile();
                }
                
            }
        }

        public async Task<string> AddAdult(Adult adult)
        {
            if (getAdultById(adult.Id) == null && getChildById(adult.Id) == null)
            {
                adults.Add(adult);
                WriteAdultToFile();
                return null;
            }
            else
            {
                return "The person(id) has already existed";
            }
        }

        public async Task<Adult> getAdultById(int Id)
        {
            for (int i = 0; i < adults.Count; i++)
            {
                if (adults[i].Id == Id)
                {
                    return adults[i];
                }
            }

            return null;
        }

        public async Task<string> AddChildren(Child child)
        {
            for (int i = 0; i < adults.Count; i++)
            {
                if (adults[i].Id!=child.Id)
                {
                    for (int j = 0; j < children.Count; j++)
                    {
                        if (children[j].Id!=child.Id)
                        {
                            children.Add(child);
                            WriteChildToFile();
                            return null;
                        }
                        else
                        {
                            return "The person(id) has already existed...";
                        }
                    }
                }
                else
                {
                    return "The person(id) has already existed...";
                }
            }

            return null;
        }

        public async Task<Child> getChildById(int Id)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].Id == Id)
                {
                    return children[i];
                }
            }

            return null;
        }

        public async Task RemoveFamily(int familyId)
        {
            Family toRemove = families.First(t => t.Id == familyId);
            families.Remove(toRemove);
            WriteFamiliesToFile();
        }

        public async Task RemoveAdult(int adultId)
        {
            foreach (var adult in adults)
            {
                if (adult.Id == adultId)
                {
                    adults.Remove(adult);
                    break;
                }
            }

            WriteAdultToFile();
        }

        public async Task RemoveChild(int childId)
        {
            Console.WriteLine(childId);
            foreach (var child in children)
            {
                if (child.Id == childId)
                {
                    children.Remove(child);
                    break;
                }
            }

            WriteChildToFile();
        }

        public async Task UpdateFamily(Family family)
        {
            Family toUpdate = families.First(t => t.Id == family.Id);
            toUpdate.Id = family.Id;
            toUpdate.StreetName = family.StreetName;
            toUpdate.HouseNumber = family.HouseNumber;
            toUpdate.Adults = family.Adults;
            toUpdate.Children = family.Children;
            toUpdate.Pets = family.Pets;
            WriteFamiliesToFile();
        }

        public async Task UpdateAdult(Adult adult)
        {
            Adult toUpdate = adults.First(t => t.Id == adult.Id);
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
            WriteAdultToFile();
        }

        public async Task UpdateChild(Child child)
        {
            Child toUpdate = children.First(t => t.Id == child.Id);
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
            WriteChildToFile();
        }

        private void Seed()
        {
            users = new[]
            {
                new User
                {
                    UserName = "Jia",
                    Password = "123456"
                },
                new User
                {
                    UserName = "Alice",
                    Password = "654321"
                }
            }.ToList();
        }
        
    }
    
}