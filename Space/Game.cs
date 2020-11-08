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

		public Game(MainWindow window)
		{
			Window = window;
			Scene = new Scene(this);
		}

		public void Update(double dt)
		{
			SBG.Update(dt);
			Scene.Update(dt);
		}

		public void Start()
		{
			SBG = new ScrollingBackground(AM.GetTexture("Assets/Background.jpg"), 100.0);
			Ship player = new Ship(Scene, 400.0, new DrawComponent(AM.GetTexture("Assets/Ship.png"), new Size(64.0, 64.0)), new TransformComponent(new Point(100.0, 100.0)));
			Scene.Actors.Add(player);
			PM.CreateBoxComponent(new Size(64.0, 64.0), player);

			State = GameState.InProgress;
		}
	}
}
