using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Models
{
    public class User
    {
        [NotNull,Key]
        public string UserName { get; set; }
        [NotNull]
        public string Password { get; set; }
        
    }
}