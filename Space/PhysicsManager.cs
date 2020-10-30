using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Space
{
	class Collision
	{
		IActor Player { get; set; }
		IActor Actor { get; set; }

		public Collision(IActor player, IActor actor)
		{
			Player = player;
			Actor = actor;
		}
	}

	class PhysicsManager
	{
		public List<BoxComponent> BoxComponents { get; private set; } = new List<BoxComponent>();
		public BoxComponent PlayerBoxComponent { get; private set; } = null;

		public List<Collision> Collisions { get; set; } = new List<Collision>();

		public void CreateBoxComponent(Size size, Point relativePosition, IActor owner)
		{
			Rect boundingRect = new Rect(
				owner.Center.X - size.Width / 2.0 + relativePosition.X,
				owner.Center.Y - size.Height / 2.0 + relativePosition.Y,
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
