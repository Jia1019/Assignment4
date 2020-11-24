using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Models {
public class Pet {
    public int Id { get; set; }
    [Required]
    [ValidSpecies]
    public string Species { get; set; }
    [Required]
    [NotNull]
    public string Name { get; set; }
    [Required]
    public int Age { get; set; }
    
    public class ValidSpecies : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            List<string> valid = new[] {"hamster","bunny", "frog", "budgerigar", "owl",
                "snake", "dog", "cat"}.ToList();
            if (valid != null && valid.Contains(value.ToString().ToLower())) {
                return ValidationResult.Success;
            }
            return new ValidationResult("Valid pet species are: Hamster, Bunny, Frog, Budgerigar, Owl, Snake, Dog, Cat");
        }
    }
}
}