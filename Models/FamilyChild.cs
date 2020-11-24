using System.Text.Json;

namespace Models
{
    public class FamilyChild
    {
        public int FamilyID { get; set; }
        public Family Family { get; set; }
        public int ChildID { get; set; }
        public Child Child { get; set; }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}