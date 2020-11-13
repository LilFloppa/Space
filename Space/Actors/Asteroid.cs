using System.Windows;

namespace Space.Actors
{
	class AsteroidController : IActorController
	{
		public IActor Owner { get; set; }
		public Point Direction { get; set; }

		public AsteroidController(IActor owner) => Owner = owner;

		public void Update(double dt) { }
	}

	class Asteroid : IActor
	{
		public Scene Scene { get; set; } = null;

		public DrawComponent DC { get; set; } = null;
		public TransformComponent TC { get; set; } = null;
		public BoxComponent BC { get; set; } = null;

		public AsteroidController Controller { get; set; } = null;

		public double Velocity { get; set; } = 0.0;
		public int HP { get; set; } = 100;

		public bool MustBeDestroyed { get; set; } = false;
		public double InvulnerabilityTime { get; set; } = 0.0;

		public Asteroid(Scene scene, Point direction, double velocity, DrawComponent dc, TransformComponent tc)
		{
			Scene = scene;

			Controller = new AsteroidController(this);
			Controller.Direction = direction;
			Velocity = velocity;

			DC = dc;
			TC = tc;
		}

		public void Update(double dt)
		{
			UpdateTransform(dt);

			if (InvulnerabilityTime > 0)
				InvulnerabilityTime -= dt;

			if (BC.BoundingRect.Top > Scene.Game.Window.Height + 100)
				MustBeDestroyed = true;

			if (HP <= 0)
				MustBeDestroyed = true;
		}

		void UpdateTransform(double dt)
		{
			Controller.Update(dt);
			Point Offset = new Point(Controller.Direction.X * Velocity * dt, Controller.Direction.Y * Velocity * dt);
			BC.AddOffset(Offset);
			TC.AddOffset(Offset);
		}

		public void GetDamage(int damage)
		{
			if (InvulnerabilityTime <= 0)
			{
				HP -= damage;
				InvulnerabilityTime = 1.0;
				DC.TexSize = new Size(DC.TexSize.Width / 1.1, DC.TexSize.Height / 1.1);
			}
		}

		public Point Center => TC.Position;
		public Rect BoundingRect => BC.BoundingRect;
	}
}
