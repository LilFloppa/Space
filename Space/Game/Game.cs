using Space.Actors;
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

		public int Score { get; set; }

		public Game(MainWindow window)
		{
			Window = window;
			Scene = new Scene(this);
		}

		public void Update(double dt)
		{
			SBG.Update(dt);
			PM.CheckCollisions();

			foreach (var collision in PM.Collisions)
				CRM.ResolveCollision(collision);

			Scene.Update(dt);
		}

		public void Start()
		{
			// Background
			SBG = new ScrollingBackground(AM.GetTexture("Background.png"), 100.0);

			// Player
			ShipSpecs specs = new ShipSpecs();
			specs.Level = 1;
			specs.Velocity = 400.0;
			specs.HP = 5.0;
			specs.MaxHP = 50.0;
			specs.Damage = 15.0;
			specs.Cooldown = 0.3;
			Ship player = new Ship(Scene, new DrawComponent(AM.GetTexture("Ship.png"), new Size(64.0, 64.0)), new TransformComponent(new Point(100.0, 100.0)), specs);
			Scene.NewActors.Add(player);
			PM.CreateBoxComponent(new Size(64.0, 64.0), player);

			CreateAsteroid();
			State = GameState.InProgress;
		}

		void CreateAsteroid()
		{
			AsteroidSpecs specs = new AsteroidSpecs();
			specs.Direction = new Point(0.0, 1.0);
			specs.Velocity = 50.0;
			specs.RotationVelocity = 20.0;
			specs.HP = 100;
			Asteroid asteroid = new Asteroid(Scene, new DrawComponent(AM.GetTexture("Asteroid.png"), new Size(100.0, 100.0)), new TransformComponent(new Point(Window.Width / 2.0, 50.0)), specs);

			Scene.NewActors.Add(asteroid);
			PM.CreateBoxComponent(new Size(100.0, 100.0), asteroid);
		}
	}
}
