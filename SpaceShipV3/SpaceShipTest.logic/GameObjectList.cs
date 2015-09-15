using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipTest.logic
{
    public class GameObjectList
    {
        public List<GameObject> ObjectList { get; set; }

        public GameObjectList()
        {
            ObjectList = new List<GameObject>();
        }
    }
}
