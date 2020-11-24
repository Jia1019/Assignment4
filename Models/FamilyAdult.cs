using System.Text.Json;

namespace Models
{
    public class FamilyAdult
    {
        public int FamilyID { get; set; }
        public Family Family { get; set; }
        public int AdultID { get; set; }
        public Adult Adult { get; set; }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}