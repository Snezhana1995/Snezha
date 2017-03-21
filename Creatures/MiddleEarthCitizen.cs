using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wars
{
    class MiddleEarthCitizen
    {
        protected int _power; //инициализируем характеристики 
        protected String _name;
        public virtual int Power
        {
            get { return _power; }
            set { _power = value; }
        }
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public static Dictionary<String, Tuple<int, int>> Powers = new Dictionary<String, Tuple<int, int>>();
        
        public static void getPowers()
        {
            Powers.Add("Human", new Tuple<int, int>(7, 8)); //обозначаем границы сил 
            Powers.Add("Wizard", new Tuple<int, int>(20, 20));
            Powers.Add("Horse", new Tuple<int, int>(4, 5));
            Powers.Add("Rohhirim", new Tuple<int, int>(7, 8));
            Powers.Add("Elf", new Tuple<int, int>(4, 7));
            Powers.Add("WoodenElf", new Tuple<int, int>(6, 6));
            Powers.Add("Wolf", new Tuple<int, int>(4, 7));
            Powers.Add("Orc", new Tuple<int, int>(8, 10));
            Powers.Add("UrukHai", new Tuple<int, int>(10, 12));
            Powers.Add("Troll", new Tuple<int, int>(11, 15));
            Powers.Add("Goblin", new Tuple<int, int>(2, 5));
        }

        public MiddleEarthCitizen(String name, int power)
        {
            _name = name;
        
            int minPower = Powers[this.GetType().Name].Item1,
                maxPower = Powers[this.GetType().Name].Item2;

            try                                             //обрабатываем исключительную ситуацию
            {
                if ((power >= minPower) && (power <= maxPower))
                {
                    Power = power;
                }
                else
                {
                    throw new PowerException($"Power should be from {minPower} to {maxPower}");
                }
            }
            catch (PowerException e)
            {
                Console.WriteLine(e.Message);
                Power = minPower;
            }
        }

        public override String ToString()
        {
            return $"Name: {Name}, Power: {Power}";
        }   
    }
}
