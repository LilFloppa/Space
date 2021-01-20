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

      public bool destroyedByBomb = false;

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

      void CreateBooster()
      {
         Random random = new Random();
         int key = random.Next(0, 100);

         if (key > 76 && key <= 100)
         {
            BoosterType bt = BoosterType.AddHP;
            BoosterSpecs specs = new BoosterSpecs();
            specs.Direction = new Point(0.0, 1.0);
            specs.LifeSpan = 4;
            specs.Velocity = 300.0;

            if (key > 76 && key <= 80)
               bt = BoosterType.AddHP;
            else if (key > 80 && key <= 84)
               bt = BoosterType.AddDamage;
            else if (key > 84 && key <= 88)
               bt = BoosterType.AddLazer;
            else if (key > 88 && key <= 92)
               bt = BoosterType.Bomb;
            else if (key > 92 && key <= 96)
               bt = BoosterType.Shield;
            else if (key > 96 && key <= 100)
               bt = BoosterType.ChainsawShield;

            specs.Type = bt;

            Booster booster = new Booster(Scene, new DrawComponent(Scene.Game.AM.GetTexture(bt + ".png"), new Size(32.0, 32.0)), new TransformComponent(TC.Position), specs);
            Scene.NewActors.Add(booster);
         }
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
         {
            MustBeDestroyed = true;
            Scene.Game.AddScore();
         }
      }

      public override void OnDestroy()
      {
         Scene.Game.AS.AsteroidCount--;
         if (!destroyedByBomb && Scene.Game.State != GameState.GameOver)
            CreateBooster();
      }
   }
}
