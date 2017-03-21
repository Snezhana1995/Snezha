using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wars
{
    class Orc : MiddleEarthCitizen, IFirstStrike //строим зависимость по иерархии
    {
        public Wolf OwnWolf { get; set; } 
       
        protected Orc(String name, int power)
            : base(name, power)
        {
        }

        public Orc(String name, int power, Wolf wolf) 
            : base(name, power)
        {
            this.OwnWolf = wolf;
        }

        public class Wolf : MiddleEarthCitizen
        {
            public Wolf(String name, int power)
                : base(name, power)
            {
            }
        }

        public override int Power //переопределяем метод Power
        {
            get
            {
                return (OwnWolf != null ? _power + OwnWolf.Power : _power);
            }
            set { _power = value; }
        }

        public void FirstStrike()
        {
        }
    }
}
