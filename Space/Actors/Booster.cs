using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Space.Actors
{
   public enum BoosterType
   {
      AddHP,
      AddDamage,
      AddLazer,
      Bomb,
      Shield,
      ChainsawShield
   }

   struct BoosterSpecs
   {
      public Point Direction;
      public double Velocity;
      public double LifeSpan;
      public BoosterType Type;
   }

   class Booster : Actor
   {
      public Point Direction { get; set; }
      public double Velocity { get; set; }
      public double LifeSpan { get; set; }
      public BoosterType Type { get; set; }

      public Booster(Scene scene, DrawComponent dc, TransformComponent tc, BoosterSpecs specs) : base(scene, tc, dc)
      {
         Scene.Game.PM.CreateBoxComponent(new Size(32.0, 32.0), this);

         Direction = specs.Direction;
         Velocity = specs.Velocity;
         LifeSpan = specs.LifeSpan;
         Type = specs.Type;
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

      public void CreateTooltip()
      {
         TooltipSpecs specs = new TooltipSpecs();
         specs.Direction = new Point(0.0, -1.0);
         specs.LifeSpan = 0.5;
         specs.Velocity = 100.0;
         specs.Text = "";

         switch (Type)
         {
            case BoosterType.AddHP:
               if (Scene.Game.player.HP + 5 <= Scene.Game.player.MaxHP)
                  specs.Text = "+5 HP";
               else specs.Text = "Max HP";
               break;
            case BoosterType.AddDamage:
               if (Scene.Game.player.Damage + 5 <= Scene.Game.player.MaxDamage)
                  specs.Text = "+5 Damage";
               else specs.Text = "Max Damage";
               break;
            case BoosterType.AddLazer:
               if (Scene.Game.player.LazerCount < 3)
                  specs.Text = "+1 Lazer";
               else specs.Text = "Max weapon";
               break;
            case BoosterType.Bomb:
               specs.Text = "+1 Bomb";
               break;
            case BoosterType.Shield:
               specs.Text = "+5s Shield";
               break;
            case BoosterType.ChainsawShield:
               specs.Text = "+5s Shield";
               break;
            default:
               break;
         }

         Tooltip tooltip = new Tooltip(Scene, new TransformComponent(TC.Position), specs);
         Scene.NewActors.Add(tooltip);
      }

      public void ApplyBooster(Ship ship, Booster booster)
      {
         booster.CreateTooltip();

         switch (booster.Type)
         {
            case BoosterType.AddHP:
               if (ship.HP + 5 <= ship.MaxHP)
                  ship.HP += 5;
               break;
            case BoosterType.AddDamage:
               if (ship.Damage + 5 <= ship.MaxDamage)
                  ship.Damage += 5;
               break;
            case BoosterType.AddLazer:
               if (ship.LazerCount < 3)
                  ship.LazerCount++;
               break;
            case BoosterType.Bomb:
               foreach (IActor actor in Scene.Actors)
                  if (actor is Asteroid)
                  {
                     (actor as Asteroid).destroyedByBomb = true;
                     actor.MustBeDestroyed = true;
                  }

               break;
            case BoosterType.Shield:
               ship.CreateShield();
               break;
            case BoosterType.ChainsawShield:
               ship.CreateCircularSaw();
               break;
            default:
               break;
         }
      }
   }
}