using System.Collections.Generic;
using System.Windows;

namespace Space
{
	class Collision
	{
		IActor Actor1 { get; set; }
		IActor Actor2 { get; set; }

		public Collision(IActor actor1, IActor actor2)
		{
			Actor1 = actor1;
			Actor2 = actor2;
		}
	}

	class PhysicsManager
	{
		public List<BoxComponent> BoxComponents { get; private set; } = new List<BoxComponent>();
		public BoxComponent PlayerBoxComponent { get; private set; } = null;

		public List<Collision> Collisions { get; set; } = new List<Collision>();

		public void CreateBoxComponent(Size size, IActor owner)
		{
			Rect boundingRect = new Rect(
				owner.TC.Position.X - size.Width / 2.0,
				owner.TC.Position.Y - size.Height / 2.0,
				size.Width,
				size.Height);

			BoxComponent bc = new BoxComponent(boundingRect, owner);

			if (owner is Ship)
				PlayerBoxComponent = bc;
			else
				BoxComponents.Add(bc);

			owner.BC = bc;
		}

		public void CheckCollisions()
		{
			foreach (var bc in BoxComponents)
			{
				if (bc.BoundingRect.IntersectsWith(PlayerBoxComponent.BoundingRect))
				{
					// TODO: Check Collisions
				}
			}

			// TODO: Check Player and Walls collisions
		}
	}
}
