using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipTest.logic
{
    public interface iPlayerPhysics
    {
        void Collisions(List<Vector2> obj, string vector);
        void CollisionCheck(List<GameObject> list);
        void GravityCheck();
        void FrictionCheck();
        void CheckVelocity();
    }
}