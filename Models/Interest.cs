using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models {
public class Interest {
    
    [ValidInterestType,Key]
    public string Type { get; set; }
    public IList<ChildInterest> ChildInterests { get; set; }
    //[NotMapped]
    //public List<string> validTypes { get; set; }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }

    //public Interest()
    //    {
    //        validTypes = new[]
    //        {
    //            "Soccer", "Drawing", "Kite Flying", "Roller Blades", "Board Games", "Ballet", "Hockey",
    //            "Gaming", "Lego", "Scout", "Gymnastics", "Harry Potter", "Frozen"
    //        }.ToList();
    //    }
    
}

    


    public class ValidInterestType : ValidationAttribute
    {
        private List<string> valid = new[]
                    {
                        "Soccer", "Drawing", "Kite Flying", "Roller Blades", "Board Games", "Ballet", "Hockey",
                        "Gaming", "Lego", "Scout", "Gymnastics", "Harry Potter", "Frozen"
                    }.ToList();
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (valid != null && valid.Contains(value.ToString()))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Valid interest types are: Soccer, Drawing, Kite Flying, Roller Blades," +
                                        "Board Games, Ballet, Hockey, Gaming, Lego, Scout, Gymnastics, Harry Potter, Frozen");
        }
    }

}
