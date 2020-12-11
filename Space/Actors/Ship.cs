using Space.Actors;
using System;
using System.Windows;
using System.Windows.Input;

namespace Space
{
	class ShipController : IActorController
	{
		public IActor Owner { get; set; } = null;
		public Point Direction { get; set; } = new Point(0.0, 0.0);

		public ShipController(Ship owner) => Owner = owner;
 
		public void OnUpdate(double dt)
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

	struct ShipSpecs
	{
		public int Level;
		public int HP;
		public int Damage;
		public int MaxHP;
		public double Velocity;
		public double Cooldown;
	}

	class Ship : IActor
	{
		public Scene Scene { get; set; } = null;

		public DrawComponent DC { get; set; } = null;
		public BoxComponent BC { get; set; } = null;
		public TransformComponent TC { get; set; } = null;
		public TextComponent Text { get; set; }

		public ShipController Controller { get; set; } = null;

		public int Level { get; set; }
		public int HP { get; set; }
		public int Damage { get; set; }
		public int MaxHP { get; set; }
		public double Velocity { get; set; }
		public double Cooldown { get; set; }

		public Ship(Scene scene, DrawComponent dc, TransformComponent tc, ShipSpecs specs)
		{
			Scene = scene;

			DC = dc;
			TC = tc;
			Text = new TextComponent(HP.ToString());

			Controller = new ShipController(this);

			Level = specs.Level;
			Velocity = specs.Velocity;
			HP = specs.HP;
			Damage = specs.Damage;
			MaxHP = specs.MaxHP;
			Cooldown = specs.Cooldown;
		}

		public void OnUpdate(double dt)
		{
			UpdateTransform(dt);
			Attack(dt);
			Text.Text = HP.ToString();
		}

		void UpdateTransform(double dt)
		{
			Controller.OnUpdate(dt);
			Point Offset = new Point(Controller.Direction.X * Velocity * dt, Controller.Direction.Y * Velocity * dt);

			BC.AddOffset(Offset);

			if (BC.BoundingRect.Top < 0.0)
				BC.SetPosition(new Point(BC.BoundingRect.X, 0.0));

			if (BC.BoundingRect.Bottom > Scene.Game.Window.Height)
				BC.SetPosition(new Point(BC.BoundingRect.X, Scene.Game.Window.Height - BC.BoundingRect.Height));

			if (BC.BoundingRect.Left < 0.0)
				BC.SetPosition(new Point(0.0, BC.BoundingRect.Y));

			if (BC.BoundingRect.Right > Scene.Game.Window.Width)
				BC.SetPosition(new Point(Scene.Game.Window.Width - BC.BoundingRect.Width, BC.BoundingRect.Y));

			TC.SetPosition(new Point(BC.BoundingRect.X + BC.BoundingRect.Width / 2.0, BC.BoundingRect.Y + BC.BoundingRect.Height / 2.0));
		}

		public void OnDestroy() { }

		void Attack(double dt)
		{
			if (Cooldown <= 0.0)
			{
				LaserSpecs specs = new LaserSpecs();
				specs.Direction = new Point(0.0, -1.0);
				specs.Damage = Damage;
				specs.LifeSpan = 2.0;
				specs.Velocity = 500.0;
				Laser laser1 = new Laser(Scene, new TransformComponent(TC.Position.X - 18, TC.Position.Y), specs);
				Laser laser2 = new Laser(Scene, new TransformComponent(TC.Position), specs);
				Laser laser3 = new Laser(Scene, new TransformComponent(TC.Position.X + 18, TC.Position.Y), specs);
				Scene.NewActors.Add(laser1);
				Scene.NewActors.Add(laser2);
				Scene.NewActors.Add(laser3);
				Cooldown = 0.05;
			}
			else
			{
				Cooldown -= dt;
			}			
		}

		public Point Center => TC.Position;
		public Rect BoundingRect => BC.BoundingRect;
		public double RotationAngle { get => 0.0; set => throw new System.NotImplementedException(); }
		public bool MustBeDestroyed { get; set; } = false;
	}
}
