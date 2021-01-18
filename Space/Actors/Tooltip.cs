using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Space.Actors
{
   struct TooltipSpecs
   {
      public Point Direction;
      public double Velocity;
      public double LifeSpan;
      public string Text;
   }

   class Tooltip : Actor
   {
      public Point Direction { get; set; }
      public double Velocity { get; set; }
      public double LifeSpan { get; set; }

      public Tooltip(Scene scene, TransformComponent tc, TooltipSpecs specs) : base(scene, tc, null)
      {
         DC = new DrawComponent(null, new Size(50.0, 8.0));

         Direction = specs.Direction;
         Velocity = specs.Velocity;
         LifeSpan = specs.LifeSpan;

         Text = new TextComponent(specs.Text);
      }

      public override void OnUpdate(double dt)
      {
         Point Offset = new Point(Direction.X * Velocity * dt, Direction.Y * Velocity * dt);
         TC.AddOffset(Offset);

         LifeSpan -= dt;

         if (LifeSpan <= 0.0)
            MustBeDestroyed = true;
      }
   }
}
