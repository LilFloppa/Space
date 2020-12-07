using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Space
{
	public partial class MainWindow : Window
	{
		public ImageSource canvasSource;
		public ImageSource CanvasSource
		{
			get => canvasSource;
			set
			{
				canvasSource = value;
				Canvas.Source = canvasSource;
			}
		}

		DateTime lastFrameTime = DateTime.Now;

		MainMenu menu = new MainMenu();

		Game game;

		ImageSource white;

		public MainWindow()
		{
			InitializeComponent();

			game = new Game(this);

			game.AM.LoadTextures();
			white = game.AM.GetTexture("White.png");

			menu.OnStartGameClicked += OnStartGameClicked;

			CompositionTarget.Rendering += GameFrame;

			Grid.Children.Add(menu);
		}

		private void GameFrame(object sender, EventArgs e)
		{
			DateTime currentFrameTime = DateTime.Now;
			TimeSpan dt = currentFrameTime - lastFrameTime;
			lastFrameTime = currentFrameTime;


			if (game.State == GameState.MainMenu)
			{

			}

			if (game.State == GameState.Start)
			{
				game.Start();
			}

			if (game.State == GameState.InProgress)
			{
				game.Update(dt.Milliseconds / 1000.0);
				Draw();
			}
		}

		private void Draw()
		{
			DrawingGroup group = new DrawingGroup();

			// Draw background
			group.Children.Add(new GeometryDrawing(new ImageBrush(game.SBG.Image), null, new RectangleGeometry(game.SBG.BGRect1)));
			group.Children.Add(new GeometryDrawing(new ImageBrush(game.SBG.Image), null, new RectangleGeometry(game.SBG.BGRect2)));

			// Draw Actors
			foreach (var actor in game.Scene.Actors)
			{
				Rect textureRect = new Rect(new Point(actor.Center.X - actor.DC.TexSize.Width / 2.0, actor.Center.Y - actor.DC.TexSize.Height / 2.0), actor.DC.TexSize);
				group.Children.Add(new GeometryDrawing(new ImageBrush(white), null, new RectangleGeometry(actor.BoundingRect)));

				ImageBrush brush = new ImageBrush(actor.DC.Texture);
				brush.Transform = new RotateTransform(actor.RotationAngle, actor.Center.X, actor.Center.Y);
				GeometryDrawing gd = new GeometryDrawing(brush, null, new EllipseGeometry(textureRect));		
				group.Children.Add(gd);

			}

			group.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, Width, Height));
			CanvasSource = new DrawingImage(group);
		}

		private void OnStartGameClicked(object sender, EventArgs e)
		{
			game.State = GameState.Start;
			Grid.Children.Remove(menu);
		}
	}
}
