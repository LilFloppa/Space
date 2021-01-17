using System;
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

   class Asteroid : Actor
   {
      public Point Direction { get; set; }
      public double Velocity { get; set; }
      public double RotationVelocity { get; set; }
      public int HP { get; set; }
      public int currentHP { get; set; }

      public Asteroid(Scene scene, DrawComponent dc, TransformComponent tc, AsteroidSpecs specs) : base(scene, tc, dc)
      {
         Text = new TextComponent(HP.ToString());

         Direction = specs.Direction;
         Velocity = specs.Velocity;
         RotationVelocity = specs.RotationVelocity;
         HP = specs.HP;
         currentHP = specs.HP;
      }

      public override void OnUpdate(double dt)
      {
         UpdateTransform(dt);
         Text.Text = currentHP.ToString();

         if (Center.Y > Scene.Game.Window.Height + 100)
            MustBeDestroyed = true;
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
         currentHP -= damage;
         if (currentHP <= 0.0)
            MustBeDestroyed = true;
      }

      public override void OnDestroy()
      {
         Console.WriteLine("ASTEROID DESTROYED!");
         Scene.Game.AsteroidCount--;
      }
   }
}
