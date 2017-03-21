using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wars
{
    class Wizard : MiddleEarthCitizen, IFirstStrike
    {
        public Wizard(String name, Horse horse) 
            : base(name, 20)
        {
            this.OwnHorse = horse;
        }

        public override int Power   //по условию(Power:20+Horse)
        {
            get
            {
                return (OwnHorse != null ? _power + OwnHorse.Power : _power); 
            }
            set { _power = value; }
        }

        protected Horse _horse;
        public Horse OwnHorse {
            get { return _horse; }
            set { _horse = value; }
        }

        public void FirstStrike()
        {
        }
    }
}
