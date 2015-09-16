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
        public SpaceShip Ship { get; set; }
        public Platform Platform1 { get; set; }
        public Platform Platform2 { get; set; }
        public Platform Square { get; set; }
        public List<GameObject> List { get; set; }

        public GameService()
        {
            Ship = new SpaceShip();
            Platform1 = new Platform(new Vector2(80, 100));
            Platform2 = new Platform(new Vector2(210, 130));
            Square = new Platform(new Vector2(220, 30), 50, 50);
            List = new List<GameObject>();
            List.Add(Platform1);
            List.Add(Platform2);
            List.Add(Square);
        }

        public void Start()
        {
            Ship.CheckVelocity();
            Ship.CollisionCheck(List);
            Ship.FrictionCheck();
            Ship.GravityCheck();
            Ship.Move();
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
