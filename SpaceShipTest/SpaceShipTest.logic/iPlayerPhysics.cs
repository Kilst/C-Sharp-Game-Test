using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipTest.logic
{
    public interface iPlayerPhysics
    {
        //void CollisionCheckY(GameObjectList list);
        //void CollisionCheckX(GameObjectList list);
        void GravityCheck();
        void FrictionCheck();
        void TerminalVelocityCheck();
    }
}