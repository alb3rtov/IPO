using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protectora
{
    class Animal
    {
        public string Name { set; get; }
        public string Sex { set; get; }
        public string Breed { set; get; }
        public int Size { set; get; }
        public int Weight { set; get; }
        public int Age { set; get; }
        public int Chip { set; get; }
        public string Picture { set; get; }

        public Animal(string name, string sex, string breed, int size, int weight, int age, int chip, string picture)
        {
            Name = name;
            Sex = sex;
            Breed = breed;
            Size = size;
            Weight = weight;
            Age = age;
            Chip = chip;
            Picture = picture;
        }
    }
}
