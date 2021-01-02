using System.Windows;

namespace Space.Actors
{
	struct LaserSpecs
	{
		public Point Direction;
		public double Velocity;
		public int Damage;
		public double LifeSpan;
	}

	class Laser : Actor
	{
		public Point Direction { get; set; }
		public double Velocity { get; set; }
		public int Damage { get; set; }
		public double LifeSpan { get; set; }

		public Laser(Scene scene, TransformComponent tc, LaserSpecs specs) : base(scene, tc, null)
		{
			DC = new DrawComponent(Scene.Game.AM.GetTexture("Laser.png"), new Size(16.0, 16.0));
			Scene.Game.PM.CreateBoxComponent(new Size(16.0, 16.0), this);

			Direction = specs.Direction;
			Velocity = specs.Velocity;
			Damage = specs.Damage;
			LifeSpan = specs.LifeSpan;
		}

		public override void OnUpdate(double dt)
		{
			Point Offset = new Point(Direction.X * Velocity * dt, Direction.Y * Velocity * dt);
			TC.AddOffset(Offset);
			BC.AddOffset(Offset);

			LifeSpan -= dt;

			if (LifeSpan <= 0.0)
				MustBeDestroyed = true;
		}
	}
}
