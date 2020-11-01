using System.Windows;
using System.Windows.Input;

namespace Space
{
	class ShipController : IActorController
	{
		public IActor Owner { get; set; } = null;
		public Point Direction { get; private set; } = new Point(0.0, 0.0);

		public ShipController(Ship owner) => Owner = owner;
 
		public void Update(double dt)
		{
			Direction = new Point(0.0, 0.0);

			if (Keyboard.IsKeyDown(Key.W))
				Direction = new Point(Direction.X, Direction.Y - 1.0);

			if (Keyboard.IsKeyDown(Key.S))
				Direction = new Point(Direction.X, Direction.Y + 1.0);

			if (Keyboard.IsKeyDown(Key.A))
				Direction = new Point(Direction.X - 1.0, Direction.Y);

			if (Keyboard.IsKeyDown(Key.D))
				Direction = new Point(Direction.X + 1.0, Direction.Y);
		}
	}

	class Ship : IActor
	{
		public MainWindow Window { get; set; }

		public DrawComponent DC { get; set; } = null;
		public BoxComponent BC { get; set; } = null;
		public TransformComponent TC { get; set; } = null;

		public ShipController Controller { get; set; }

		public double Velocity { get; set; } = 0.0;

		public Ship(MainWindow window, double velocity, DrawComponent dc, TransformComponent tc)
		{
			Window = window;
			Velocity = velocity;

			DC = dc;
			TC = tc;

			Controller = new ShipController(this);
		}

		public void Update(double dt)
		{
			// Update Position
			Controller.Update(dt);
			Point Offset = new Point(Controller.Direction.X * Velocity * dt, Controller.Direction.Y * Velocity * dt);

			TC.AddOffset(Offset);
			BC.AddOffset(Offset);

			if (BC.BoundingRect.Top < 0.0)
				BC.SetPosition(new Point(BC.BoundingRect.X, 0.0));

			if (BC.BoundingRect.Bottom > Window.Height)
				BC.SetPosition(new Point(BC.BoundingRect.X, Window.Height - BC.BoundingRect.Height));

			if (BC.BoundingRect.Left < 0.0)
				BC.SetPosition(new Point(0.0, BC.BoundingRect.Y));

			if (BC.BoundingRect.Right > Window.Width)
				BC.SetPosition(new Point(Window.Width - BC.BoundingRect.Width, BC.BoundingRect.Y));

			TC.SetPosition(new Point(BC.BoundingRect.X + BC.BoundingRect.Width / 2.0, BC.BoundingRect.Y + BC.BoundingRect.Height / 2.0));
		}

		public Point Center => TC.Position;
		public Rect BoundingRect => BC.BoundingRect;
	}
}
