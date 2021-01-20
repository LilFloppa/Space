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
      public int MaxHP;
      public int Damage;
      public int MaxDamage;
      public int LazerCount;
      public double Velocity;
      public double Cooldown;
   }

   class Ship : Actor
   {
      public ShipController Controller { get; set; } = null;

      public int Level { get; set; }
      public int HP { get; set; }
      public int MaxHP { get; set; }
      public int Damage { get; set; }
      public int MaxDamage { get; set; }
      public int LazerCount { get; set; }
      public double Velocity { get; set; }
      public double Cooldown { get; set; }

      public bool IsInvincible { get; set; } = false;
      public CircularSaw CircularSaw { get; set; } = null;
      public Shield Shield { get; set; } = null;

      public Ship(Scene scene, DrawComponent dc, TransformComponent tc, ShipSpecs specs) : base(scene, tc, dc)
      {
         Controller = new ShipController(this);
         Text = new TextComponent(HP.ToString());

         Level = specs.Level;
         Velocity = specs.Velocity;
         HP = specs.HP;
         MaxHP = specs.MaxHP;
         Damage = specs.Damage;
         MaxDamage = specs.MaxDamage;
         LazerCount = specs.LazerCount;
         Cooldown = specs.Cooldown;
      }

      public override void OnUpdate(double dt)
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

      void Attack(double dt)
      {
         if (Cooldown <= 0.0)
         {
            LaserSpecs specs = new LaserSpecs();
            specs.Direction = new Point(0.0, -1.0);
            specs.Damage = Damage;
            specs.LifeSpan = 2.0;
            specs.Velocity = 500.0;

            if (LazerCount == 1)
            {
               Laser laser = new Laser(Scene, new TransformComponent(TC.Position), specs);

               Scene.NewActors.Add(laser);
            }
            else if (LazerCount == 2)
            {
               Laser laser1 = new Laser(Scene, new TransformComponent(TC.Position.X - 18, TC.Position.Y), specs);
               Laser laser2 = new Laser(Scene, new TransformComponent(TC.Position.X + 18, TC.Position.Y), specs);

               Scene.NewActors.Add(laser1);
               Scene.NewActors.Add(laser2);
            }
            else if (LazerCount == 3)
            {
               Laser laser1 = new Laser(Scene, new TransformComponent(TC.Position.X - 18, TC.Position.Y), specs);
               Laser laser = new Laser(Scene, new TransformComponent(TC.Position), specs);
               Laser laser3 = new Laser(Scene, new TransformComponent(TC.Position.X + 18, TC.Position.Y), specs);

               Scene.NewActors.Add(laser1);
               Scene.NewActors.Add(laser);
               Scene.NewActors.Add(laser3);
            }

            Cooldown = 0.05;
         }
         else
         {
            Cooldown -= dt;
         }
      }

      public void GetDamage(int damage)
      {
         HP -= damage;
         if (HP <= 0.0)
            MustBeDestroyed = true;
      }

		public override void OnDestroy()
		{
         Scene.Game.GameOver();
      }

      public void CreateCircularSaw()
		{
         if (CircularSaw != null)
         {
            CircularSaw.LifeSpan = 5.0;
         }
         else
         {
            TransformComponent tc = new TransformComponent(Center);
            Size size = new Size(DC.TexSize.Width * 1.5, DC.TexSize.Height * 1.5);
            DrawComponent dc = new DrawComponent(Scene.Game.AM.GetTexture("Saw.png"), size);
            CircularSaw circular = new CircularSaw(Scene, tc, dc, this, 1, 500);
            Scene.Game.PM.CreateBoxComponent(size, circular);

            CircularSaw = circular;
            Scene.NewActors.Add(circular);
         }
      }

      public void CreateShield()
		{
         if (Shield != null)
         {
            Shield.LifeSpan = 5.0;
         }
         else
         {
            TransformComponent tc = new TransformComponent(Center);
            Size size = new Size(DC.TexSize.Width * 1.5, DC.TexSize.Height * 1.5);
            DrawComponent dc = new DrawComponent(Scene.Game.AM.GetTexture("Circle.png"), size);
            Shield shield = new Shield(Scene, tc, dc, this);
            Scene.Game.PM.CreateBoxComponent(size, shield);

            Shield = shield;
            Scene.NewActors.Add(shield);
         }
      }
	}
}
