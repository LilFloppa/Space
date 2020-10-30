using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Space
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ScrollingBackground sbg = null;

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

		bool isRunning = true;

		DateTime lastFrameTime = DateTime.Now;

		List<IActor> actors = new List<IActor>();

		PhysicsManager PM = new PhysicsManager();

		ImageSource white;

		public MainWindow()
		{
			InitializeComponent();

			ImageSource bgTexture = new BitmapImage(new Uri("pack://application:,,,/Assets/Background.jpg"));
			ImageSource playerTexture = new BitmapImage(new Uri("pack://application:,,,/Assets/Ship.png"));
			white = new BitmapImage(new Uri("pack://application:,,,/Assets/White.png"));

			sbg = new ScrollingBackground(bgTexture, 100.0);

			Ship player = new Ship(this, 400.0, new DrawComponent(playerTexture, new Size(64, 64)), new TransformComponent(new Point(100, 100)));

			PM.CreateBoxComponent(new Size(100.0, 100.0), new Point(0.0, 0.0), player);

			actors.Add(player);

			CompositionTarget.Rendering += GameFrame;
		}

		private void GameFrame(object sender, EventArgs e)
		{
			DateTime currentFrameTime = DateTime.Now;
			TimeSpan dt = currentFrameTime - lastFrameTime;
			lastFrameTime = currentFrameTime;

			if (isRunning)
			{
				Update(dt.Milliseconds / 1000.0);
				Draw();
			}
		}

		private void Update(double dt)
		{
			sbg.Update(dt);
			
			foreach (var actor in actors)
				actor.Update(dt);
		}

		private void Draw()
		{
			DrawingGroup group = new DrawingGroup();

			// Draw background
			group.Children.Add(new GeometryDrawing(new ImageBrush(sbg.Image), null, new RectangleGeometry(sbg.BGRect1)));
			group.Children.Add(new GeometryDrawing(new ImageBrush(sbg.Image), null, new RectangleGeometry(sbg.BGRect2)));

			// Draw Actors
			foreach (var actor in actors)
			{
				group.Children.Add(new GeometryDrawing(new ImageBrush(white), null, new RectangleGeometry(actor.BC.BoundingRect)));
				group.Children.Add(new GeometryDrawing(new ImageBrush(actor.DC.Texture), null, new RectangleGeometry(actor.BoundingRect)));
			}

			group.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, Width, Height));
			CanvasSource = new DrawingImage(group);
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			isRunning = false;
		}
	}
}
