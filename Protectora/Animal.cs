using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora
{
    public class Animal
    {
        public string Name { set; get; }
        public string Sex { set; get; }
        public string Breed { set; get; }
        public int Size { set; get; }
        public int Weight { set; get; }
        public int Age { set; get; }
        public int Chip { set; get; }
        public List<string> Pictures { set; get; }
        public Sponsor Sponsor { set; get; }
        public Uri Video { set; get; }
        public string Sterilized { set; get; }
        public string SociableChildren { set; get; }
        public string SociableDogs { set; get; }
        public string Ppp { set; get; }

        public Animal(string name, string sex, string breed, int size, int weight, int age, int chip, List<string> pictures, 
                    Sponsor sponsor, Uri video, string sterilized, string sociableChildren, string sociableDogs, string ppp) 
        {
            Name = name;
            Sex = sex;
            Breed = breed;
            Size = size;
            Weight = weight;
            Age = age;
            Chip = chip;
            Pictures = pictures;
            Sponsor = sponsor;
            Video = video;
            Sterilized = sterilized;
            SociableChildren = sociableChildren;
            SociableDogs = sociableDogs;
            Ppp = ppp;
        }
    }
}
