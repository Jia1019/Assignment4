﻿using System.Collections.Generic;
 using System.Threading.Tasks;
 using Models;

namespace Assignment.Data
{
    public interface IFamilyService
    {
        Task<IList<Family>> GetFamilies();
        Task<IList<Adult>> GetAdults();
        Task<IList<Child>> GetChildren();
        Task<Adult> getAdultById(int Id);
        Task<Child> getChildById(int Id);
        Task AddFamily(Family family);
        Task<string> AddAdult(Adult adult);
        Task<string> AddChildren(Child child);
        Task RemoveFamily(int familyId);
        Task RemoveAdult(int adultId);
        Task RemoveChild(int childId);
        Task UpdateFamily(Family family);
        Task UpdateAdult(Adult adult);
        Task UpdateChild(Child child);
        public Task<string> getFamilyError();
        Task<IList<User>> GetUsers();
    }
}