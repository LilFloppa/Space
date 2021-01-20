using System.Windows;

namespace Space.Actors
{
	class CircularSaw : Actor
	{
		public Ship Ship = null;
		public int Damage = 0;
		public double RotationVelocity = 0.0;
		public double LifeSpan = 5.0;
		public double Cooldown = 0.2;

		public CircularSaw(Scene scene, TransformComponent tc, DrawComponent dc, Ship ship, int damage, double rotationVelocity) : base(scene, tc, dc)
		{
			Ship = ship;
			Damage = damage;
			RotationVelocity = rotationVelocity;

			ship.IsInvincible = true;
		}

		public override void OnUpdate(double dt)
		{
			LifeSpan -= dt;

			if (LifeSpan < 0.0)
				MustBeDestroyed = true;

			if (Cooldown < 0.0)
			{
				Damage = 1;
			}
			else
			{
				Cooldown -= dt;
				Damage = 0;
			}

			UpdateTransform(dt);
		}

		public void UpdateTransform(double dt)
		{
			RotationAngle += RotationVelocity * dt;
			TC.SetPosition(Ship.Center);
			BC.SetPosition(Ship.Center);

			Point offset = new Point(-BC.BoundingRect.Width / 2.0, -BC.BoundingRect.Height / 2.0);
			BC.AddOffset(offset);
		}

		public override void OnDestroy()
		{
			Ship.IsInvincible = false;
			Ship.CircularSaw = null;
		}
	}
}
