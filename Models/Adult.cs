using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models
{
    public class Adult : Person
    {
        [ValidJobTitle] public string JobTitle { get; set; }
       
        public IList<FamilyAdult> FamilyAdults { get; set; }

        //public override string ToString()
        //{
        //    return JsonSerializer.Serialize(this);
        //}

        //public override bool isAdult()
        //{
        //    return true;
        //}
        
        public void Update(Adult toUpdate)
        {
            JobTitle = toUpdate.JobTitle;
            base.Update(toUpdate);
        }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }

        public class ValidJobTitle : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                List<string> valid = new[]
                {
                    "teacher", "engineer", "garbage collector", "shepherd", "pilot",
                    "police officer", "pirate", "fireman", "astronaut", "captain", "soldier", "pizza chef",
                    "janitor", "factory worker", "chauffeur", "waitress", "ninja", "doctor", "nurse", "chemist",
                    "caretaker", "gardener", "mascot", "athlete"
                }.ToList();
                if (valid != null && valid.Contains(((string)value).ToLower()))
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Valid job titles are: Teacher, Engineer, Garbage Collector, Shepherd" +
                                            ", Pilot, PoliceOfficer, Pirate, Fireman, Astronaut, Captain, Soldier, Pizza Chef" +
                                            ", Janitor, Factory Worker, Chauffeur, Waitress, Ninja, Doctor, Nurse, Chemist" +
                                            ", Caretaker, Gardener, Mascot, Athlete");
            }

        }
    }
}