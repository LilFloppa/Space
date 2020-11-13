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

			if (actor1 is Ship)
			{
				ResolveCollisionForShip((Ship)actor1, actor2);
			}

			if (actor2 is Ship)
			{
				ResolveCollisionForShip((Ship)actor2, actor1);
			}
		}

		void ResolveCollisionForShip(Ship ship, IActor actor)
		{
			if (actor is Asteroid)
				(actor as Asteroid).GetDamage(15);
		}
	}
}
