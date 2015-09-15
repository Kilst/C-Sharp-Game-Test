using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//..
using SpaceShipTest.logic;

namespace SpaceShipTest.service
{
    public class GameService
    {
        public SpaceShip ship { get; set; }
        public Platform platform1 { get; set; }
        public Platform platform2 { get; set; }
        public GameObjectList list { get; set; }

        public GameService()
        {
            this.ship = new SpaceShip();
            this.platform1 = new Platform(new Vector2(80, 100));
            this.platform2 = new Platform(new Vector2(210, 130));
            this.list = new GameObjectList();
            this.list.ObjectList.Add(platform1);
            this.list.ObjectList.Add(platform2);
        }

        public void Start()
        {
            ship.CheckVelocity();
            ship.CollisionCheck(list);
            ship.FrictionCheck();
            ship.GravityCheck();
            ship.Move();
        }

        // Should be polymorphic
        public int GetObjectPositionX(GameObject obj)
        {
            int x = (int)obj.Position.X;
            return x;
            //this.GetObjectPositionX(this.ship);
        }

        public int GetObjectPositionY(GameObject obj)
        {
            int y = (int)obj.Position.Y;
            return y;
        }
    }
}
