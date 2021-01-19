using System.Collections.Generic;
using System.Windows;

namespace Space.Managers
{
	class Collision
	{
		public IActor Actor1 { get; set; }
		public IActor Actor2 { get; set; }

		public Collision(IActor actor1, IActor actor2)
		{
			Actor1 = actor1;
			Actor2 = actor2;
		}
	}

	class PhysicsManager
	{
		public List<BoxComponent> BoxComponents { get; private set; } = new List<BoxComponent>();
		public List<Collision> Collisions { get; set; } = new List<Collision>();

		public void CreateBoxComponent(Size size, IActor owner)
		{
			Rect boundingRect = new Rect(
				owner.TC.Position.X - size.Width / 2.0,
				owner.TC.Position.Y - size.Height / 2.0,
				size.Width,
				size.Height);

			BoxComponent bc = new BoxComponent(boundingRect, owner);
			BoxComponents.Add(bc);
			owner.BC = bc;
		}

		public void CheckCollisions()
		{
			Collisions.Clear();

			for (int i = 0; i < BoxComponents.Count; i++)
				for (int j = i + 1; j < BoxComponents.Count; j++)
				{
					BoxComponent bc1 = BoxComponents[i];
					BoxComponent bc2 = BoxComponents[j];

					if (bc1.BoundingRect.IntersectsWith(bc2.BoundingRect))
						Collisions.Add(new Collision(bc1.Owner, bc2.Owner));
				}
		}

		public void DeleteBoxComponent(BoxComponent bc) => BoxComponents.Remove(bc);
	}
}
