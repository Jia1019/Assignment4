using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models {
public class Child : Person {
    public IList<ChildInterest> ChildInterests { get; set; }
    //public List<Interest> ChildInterests { get; set; }
    public ICollection<Pet> Pets { get; set; }
    public IList<FamilyChild> FamilyChildren { get; set; }
    
    public void Update(Child toUpdate) {
        base.Update(toUpdate);
        ChildInterests = toUpdate.ChildInterests;
        Pets = toUpdate.Pets;
    }

    public Child()
    {
        ChildInterests = new List<ChildInterest>();
        Pets = new List<Pet>();
    }

    public string interestList()
    {
        string list = "";
        for (int i = 0; i < ChildInterests.Count; i++)
        {
            list += ChildInterests[i].Interest.Type+" ";
        }

        return list;
    }

    //public string petList()
    //{
    //    string list = "";
    //    for (int i = 0; i < Pets.Count; i++)
    //    {
    //        list += Pets[i].Name+" ";
    //    }
//
    //    return list;
    //}
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }

}
}