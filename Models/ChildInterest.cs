using System.Text.Json;

namespace Models
{
    public class ChildInterest
    {
        public int ChildID { get; set; }
        public Child Child { get; set; }

        public string InterestType { get; set; }
        
        public Interest Interest { get; set; }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}