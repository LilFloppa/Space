using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Space.Actors
{
	class Shield : Actor
	{
		public Ship Ship = null;
		public double LifeSpan = 5.0;

		public Shield(Scene scene, TransformComponent tc, DrawComponent dc, Ship ship) : base(scene, tc, dc)
		{
			Ship = ship;
			ship.IsInvincible = true;
		}

		public override void OnUpdate(double dt)
		{
			LifeSpan -= dt;

			if (LifeSpan < 0.0)
				MustBeDestroyed = true;

			UpdateTransform(dt);
		}

		public void UpdateTransform(double dt)
		{
			TC.SetPosition(Ship.Center);
			BC.SetPosition(Ship.Center);

			Point offset = new Point(-BC.BoundingRect.Width / 2.0, -BC.BoundingRect.Height / 2.0);
			BC.AddOffset(offset);
		}

		public override void OnDestroy()
		{
			Ship.IsInvincible = false;
			Ship.Shield = null;
		}
	}
}
