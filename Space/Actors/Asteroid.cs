﻿using System;
using System.Windows;

namespace Space.Actors
{
	struct AsteroidSpecs
	{
		public Point Direction;
		public double Velocity;
		public double RotationVelocity;
		public int HP;
	}

	class Asteroid : IActor
	{
		public Scene Scene { get; set; } = null;

		public DrawComponent DC { get; set; } = null;
		public TransformComponent TC { get; set; } = null;
		public BoxComponent BC { get; set; } = null;
		public TextComponent Text { get; set; }

		public Point Direction { get; set; }
		public double Velocity { get; set; }
		public double RotationVelocity { get; set; }
		public int HP { get; set; }

		public Asteroid(Scene scene, DrawComponent dc, TransformComponent tc, AsteroidSpecs specs)
		{
			Scene = scene;

			DC = dc;
			TC = tc;

			Text = new TextComponent(HP.ToString());

			Direction = specs.Direction;
			Velocity = specs.Velocity;
			RotationVelocity = specs.RotationVelocity;
			HP = specs.HP;
		}

		public void OnUpdate(double dt)
		{
			UpdateTransform(dt);
			Text.Text = HP.ToString();
		}

		void UpdateTransform(double dt)
		{
			Point Offset = new Point(Direction.X * Velocity * dt, Direction.Y * Velocity * dt);
			BC.AddOffset(Offset);
			TC.AddOffset(Offset);

			RotationAngle += RotationVelocity * dt;
		}

		public void GetDamage(int damage)
		{
			HP -= damage;
			if (HP <= 0.0)
				MustBeDestroyed = true;
		}

		public void OnDestroy()
		{
			Console.WriteLine("ASTEROID DESTROYED!");
			Scene.Game.PM.DeleteBoxComponent(BC);
		}

		public Point Center => TC.Position;
		public Rect BoundingRect => BC.BoundingRect;
		public double RotationAngle { get; set; }
		public bool MustBeDestroyed { get; set; } = false;
	}
}
