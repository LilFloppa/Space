using System.Windows;

namespace Space.Actors
{
	struct LaserSpecs
	{
		public Point Direction;
		public double Velocity;
		public double LifeSpan;
	}

	class Laser : IActor
	{
		public DrawComponent DC { get; set; }
		public TransformComponent TC { get; set; }
		public BoxComponent BC { get; set; }
		public Scene Scene { get; set; }

		public Point Direction { get; set; }
		public double Velocity { get; set; }
		public double LifeSpan { get; set; }

		public Laser(Scene scene, TransformComponent tc, LaserSpecs specs)
		{
			Scene = scene;

			TC = tc;
			DC = new DrawComponent(Scene.Game.AM.GetTexture("Laser.png"), new Size(16.0, 16.0));
			Scene.Game.PM.CreateBoxComponent(new Size(16.0, 16.0), this);

			Direction = specs.Direction;
			Velocity = specs.Velocity;
			LifeSpan = specs.LifeSpan;
		}

		public void OnUpdate(double dt)
		{
			Point Offset = new Point(Direction.X * Velocity * dt, Direction.Y * Velocity * dt);
			TC.AddOffset(Offset);

			LifeSpan -= dt;

			if (LifeSpan <= 0.0)
				MustBeDestroyed = true;
		}

		public void OnDestroy() { }

		public Point Center => TC.Position;
		public Rect BoundingRect => BC.BoundingRect;
		public double RotationAngle { get => 0.0; set => throw new System.NotImplementedException(); }
		public bool MustBeDestroyed { get; set; }
	}
}
