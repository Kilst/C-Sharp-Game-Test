using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipTest.logic
{
    public interface iPlayerPhysics
    {
        void CollisionCheck(GameObjectList list);
        void GravityCheck();
        void FrictionCheck();
        void TerminalVelocityCheck();
    }
}