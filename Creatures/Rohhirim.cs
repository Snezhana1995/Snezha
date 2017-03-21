using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wars
{
    class Rohhirim : Human, IFirstStrike
    {
        public Rohhirim(String name, int power, Horse horse) 
            : base(name, power)
        {
            this.OwnHorse = horse;
        }

        public override int Power
        {
            get
            {
                return (OwnHorse != null ? _power + OwnHorse.Power : _power);
            }
            set { _power = value; }
        }

        protected Horse _horse;
        public Horse OwnHorse
        {
            get { return _horse; }
            set { _horse = value; }
        }

        public void FirstStrike()
        {
        }
    }
}
