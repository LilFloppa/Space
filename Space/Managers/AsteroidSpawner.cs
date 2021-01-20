using System;
using System.Windows;
using Space.Actors;

namespace Space.Managers
{

	class AsteroidSpawner
	{
      Game Game = null;
      public int MaxAsteroidCount = 0;
      public double Intensity = 0.0;
      public int MinAsteroidHealth = 0;
      public int MaxAsteroidHealth = 0;
      public int MinVelocity = 0;
      public int MaxVelocity = 0;
      public double SpawnCooldown = 0.0;

      int CurrentMaxAsteroidCount = 0;
      double CurrentSpawnCooldown = 0.0;
      int CurrentMinAsteroidHealth = 0;
      int CurrentMaxAsteroidHelth = 0;
      int CurrentMinVelocity = 0;
      int CurrentMaxVelocity = 0;

      public int AsteroidCount = 0;
      Random random = new Random();
      double CooldownValue = 0.0;

		public AsteroidSpawner(Game game, int maxAsteroidCount, int minAsteroidHealth, int maxAsteroidHealth, int minVelocity, int maxVelocity, double spawnCooldown)
		{
			Game = game;

			MaxAsteroidCount = maxAsteroidCount;
			MinAsteroidHealth = minAsteroidHealth;
			MaxAsteroidHealth = maxAsteroidHealth;
         MinVelocity = minVelocity;
         MaxVelocity = maxVelocity;
         SpawnCooldown = spawnCooldown;

         CalculateIntensity();

         CurrentMaxAsteroidCount = MaxAsteroidCount + (int)(20 * Intensity);
         CurrentSpawnCooldown = SpawnCooldown * (1.0 - Intensity);
         CurrentMinAsteroidHealth = MinAsteroidHealth + (int)(1500 * Intensity);
         CurrentMaxAsteroidHelth = MaxAsteroidHealth + (int)(1500 * Intensity);
         CurrentMinVelocity = MinVelocity + (int)(300 * Intensity);
         CurrentMaxVelocity = MaxVelocity + (int)(300 * Intensity);
      }

		public void Update(double dt)
		{
         if (AsteroidCount < CurrentMaxAsteroidCount)
         {
            if (CooldownValue < 0.0)
            {
               SpawnAsteroid();
               CooldownValue = CurrentSpawnCooldown;
            }
            else
            {
               CooldownValue -= dt;
            }
         }

         CalculateIntensity();
         CalculateSpawnerSpecs();
      }

		void SpawnAsteroid()
		{
         AsteroidSpecs specs = new AsteroidSpecs();
         specs.Direction = new Point(0.0, 1.0);
         specs.Velocity = random.Next(CurrentMinVelocity, CurrentMaxVelocity);
         specs.RotationVelocity = random.NextDouble() * random.Next(0, 100) + 20;
         specs.HP = random.Next(CurrentMinAsteroidHealth, CurrentMaxAsteroidHelth);

         int x = random.Next(0, (int)Game.Window.Width);
         int y = random.Next(-500, -100);

         TransformComponent TC = new TransformComponent(x, y);

         int size = random.Next(70, 110);

         Asteroid asteroid = new Asteroid(Game.Scene, new DrawComponent(Game.AM.GetTexture("Asteroid.png"), new Size(size, size)), TC, specs);

         Game.Scene.NewActors.Add(asteroid);
         Game.PM.CreateBoxComponent(new Size(size * 0.9, size * 0.9), asteroid);

         AsteroidCount++;
      }

      void CalculateIntensity()
		{
         Intensity = 0.1 + 0.7 * Game.Score / Game.MaxScore;
		}

      void CalculateSpawnerSpecs()
		{
         CurrentMaxAsteroidCount = MaxAsteroidCount + (int)(5 * Game.Level * Intensity);
         CurrentSpawnCooldown = SpawnCooldown * (1.0 - 0.5 * Intensity) / Game.Level;
         CurrentMinAsteroidHealth = MinAsteroidHealth + (int)(300 * Game.Level * Intensity);
         CurrentMaxAsteroidHelth = MaxAsteroidHealth + (int)(300 * Game.Level * Intensity);
         CurrentMinVelocity = MinVelocity + (int)(100 * Game.Level * Intensity);
         CurrentMaxVelocity = MaxVelocity + (int)(100 * Game.Level * Intensity);
      }
	}
}
