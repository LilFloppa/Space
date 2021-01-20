using System;
using System.Windows;
using Space.Managers;

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
      public AsteroidSpawner AS { get; set; } = null;

      public Ship player { get; set; }

      public int Level { get; set; } = 1;
      public int Score { get; set; } = 0;
      public int MaxScore { get; set; } = 5000;

      public int ScorePerAsteroid { get; set; } = 150;

      public Game(MainWindow window)
      {
         Window = window;
         Scene = new Scene(this);

         AS = new AsteroidSpawner(this, 30, 400, 500, 100, 120, 1.7);
      }

      public void Update(double dt)
      {
         SBG.Update(dt);
         PM.CheckCollisions();

         Console.WriteLine(PM.BoxComponents.Count);
         foreach (var collision in PM.Collisions)
            CRM.ResolveCollision(collision);

         AS.Update(dt);
         Scene.Update(dt);
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

      public void GameOver()
      {
         bool success = Score >= MaxScore;

         Window.gameOverMenu.GameOverLabel.Content = "Game Over! You " + (success ? "win!" : "lose!");
         Window.gameOverMenu.ResultLabel.Content = "Result: " + Score;

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
         }

         Scene.Clear();
         PM.BoxComponents.Clear();
         AS.AsteroidCount = 0;
      }

      public void AddScore()
      {
         Score += ScorePerAsteroid;

         if (Score < MaxScore)
         {
            Window.ScoreBar.Value = Score;
            Window.Score.Content = "Score: " + Score;
         }
         else
            GameOver();
      }
   }
}