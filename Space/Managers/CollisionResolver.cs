using Space.Actors;
using System;

namespace Space
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
				
		}

		void ResolveCollisionForShip(Ship ship, IActor actor)
		{
			if (actor is Asteroid)
				(actor as Asteroid).GetDamage(15);
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
