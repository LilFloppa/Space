using Space.Actors;
using System;

namespace Space.Managers
{
   class CollisionResolveManager
   {
      public void ResolveCollision(Collision collision)
      {
         IActor actor1 = collision.Actor1;
         IActor actor2 = collision.Actor2;

         if (actor1 is Laser)
            ResolveCollisionForLaser((Laser)actor1, actor2);
         else if (actor2 is Laser)
            ResolveCollisionForLaser((Laser)actor2, actor1);
         else if (actor1 is Ship)
            ResolveCollisionForShip((Ship)actor1, actor2);
         else if (actor2 is Ship)
            ResolveCollisionForShip((Ship)actor2, actor1);
      }

      void ResolveCollisionForShip(Ship ship, IActor actor)
      {
         if (actor is Asteroid)
         {
            (actor as Asteroid).MustBeDestroyed = true;
            ship.GetDamage(5);

            if (ship.Damage > 5)
               ship.Damage -= 5;

            if (ship.LazerCount > 1)
               ship.LazerCount--;
         }

         if (actor is Booster)
         {
            (actor as Booster).MustBeDestroyed = true;
            (actor as Booster).ApplyBooster(ship, actor as Booster);
         }
      }

      void ResolveCollisionForLaser(Laser laser, IActor actor)
      {
         if (actor is Asteroid)
         {
            (actor as Asteroid).GetDamage(laser.Damage);
            laser.MustBeDestroyed = true;
         }
      }
   }
}
