using Space.Actors;
using System;
using System.Windows;

namespace Space
{
   public enum GameState
   {
      MainMenu,
      Start,
      InProgress,
      GameOver,
      Exit
   }

   class Game
   {
      public MainWindow Window { get; set; }
      public Scene Scene { get; set; }
      public GameState State { get; set; } = GameState.MainMenu;

      public ScrollingBackground SBG { get; set; } = null;

      public PhysicsManager PM { get; set; } = new PhysicsManager();
      public AssetManager AM { get; set; } = new AssetManager();
      public CollisionResolveManager CRM { get; set; } = new CollisionResolveManager();

      public Ship player { get; set; }

      public uint MaxAsteroidCount = 20;
      public uint AsteroidCount = 0;
      public double AsteroidCooldown = 0.0;

      public int Level { get; set; } = 1;
      public int Score { get; set; } = 0;
      public int MaxScore { get; set; } = 5000;

      public Game(MainWindow window)
      {
         Window = window;
         Scene = new Scene(this);
      }

      public void Update(double dt)
      {
         SBG.Update(dt);
         PM.CheckCollisions();

         Console.WriteLine(PM.BoxComponents.Count);
         foreach (var collision in PM.Collisions)
            CRM.ResolveCollision(collision);

         Scene.Update(dt);

         CreateAsteroid(dt);
      }

      public void Start()
      {
         Scene.endGame = false;

         // Background
         SBG = new ScrollingBackground(AM.GetTexture("Background.png"), 100.0);

         // Player
         ShipSpecs specs = new ShipSpecs();
         specs.Level = 1;
         specs.Velocity = 400.0;
         specs.HP = 5;
         specs.MaxHP = 50;
         specs.Damage = 25;
         specs.MaxDamage = 100;
         specs.LazerCount = 1;
         specs.Cooldown = 0.3;
         player = new Ship(Scene, new DrawComponent(AM.GetTexture("Ship.png"), new Size(64.0, 64.0)), new TransformComponent(new Point(200.0, 700.0)), specs);
         Scene.NewActors.Add(player);
         PM.CreateBoxComponent(new Size(64.0, 64.0), player);

         State = GameState.InProgress;

         Window.ScoreBar.Maximum = MaxScore;
      }

      public void GameOver(int score, bool success)
      {
         Window.gameOverMenu.GameOverLabel.Content = "Game Over! You " + (success ? "win!" : "lose!");
         Window.gameOverMenu.ResultLabel.Content = "Result: " + score;

         Score = 0;
         Window.Score.Content = "Score: 0";
         Window.ScoreBar.Value = 0;

         State = GameState.GameOver;
         Window.Grid.Children.Remove(Window.menu);
         Window.Grid.Children.Add(Window.gameOverMenu);

         if (success)
         {
            Level++;
            MaxScore = 5000 * Level;
            Window.ScoreBar.Maximum = MaxScore;
            Window.menu.LevelLabel.Content = "Level: " + Level;
            Window.menu.TargetLabel.Content = "Target: " + MaxScore;

            PM.BoxComponents.Clear();
            Scene.Actors.Clear();
            Scene.NewActors.Clear();
         }
      }

      void CreateAsteroid(double dt)
      {
         if (AsteroidCount < MaxAsteroidCount)
         {
            if (AsteroidCooldown < 0.0)
            {
               Random random = new Random();
               AsteroidSpecs specs = new AsteroidSpecs();
               specs.Direction = new Point(0.0, 1.0);
               specs.Velocity = random.Next(150, 200);
               specs.RotationVelocity = random.NextDouble() * 60 + 20;
               specs.HP = random.Next(300, 900);

               int x = random.Next(0, (int)Window.Width);
               int y = random.Next(-500, -100);

               TransformComponent TC = new TransformComponent((double)x, (double)y);

               int size = random.Next(70, 110);

               Asteroid asteroid = new Asteroid(Scene, new DrawComponent(AM.GetTexture("Asteroid.png"), new Size((double)size, (double)size)), TC, specs);

               Scene.NewActors.Add(asteroid);
               PM.CreateBoxComponent(new Size((double)size, (double)size), asteroid);

               AsteroidCount++;
               AsteroidCooldown = 1.5;
            }
            else
            {
               AsteroidCooldown -= dt;
            }
         }
      }

      public void ScoreChanged(int score)
      {
         Score += score;

         if (Score < MaxScore)
         {
            Window.ScoreBar.Value = Score;
            Window.Score.Content = "Score: " + Score;
         }
         else
            GameOver(Score, true);
      }
   }
}