using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip.logic
{
    public class PlatformsList
    {
        public List<Platform> Platforms { get; set; }

        public PlatformsList()
        {
            this.Platforms = new List<Platform>();
        }
    }
}
