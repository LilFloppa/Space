using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Space
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Image image = new Image();

		ScrollingBackground sbg;

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

		int X = 20;
		int Y = 20;
		bool isRunning = true;

		DateTime lastFrameTime = DateTime.Now;

		public List<Point> Poses = new List<Point>();
		public List<Size> Sizes = new List<Size>();

		BitmapImage bitmap;

		public MainWindow()
		{
			InitializeComponent();

			bitmap = new BitmapImage(new Uri("pack://application:,,,/SeamlessBackground.png"));
			sbg = new ScrollingBackground(bitmap, 400.0);

			for (int i = 0; i < X; i++)
			{
				for (int j = 0; j < Y; j++)
				{
					Poses.Add(new Point(i * 10.0, j * 10.0));
					Sizes.Add(new Size(9.0, 9.0));
				}
			}

			CompositionTarget.Rendering += GameFrame;
		}

		private void GameFrame(object sender, EventArgs e)
		{
			DateTime currentFrameTime = DateTime.Now;
			TimeSpan dt = currentFrameTime - lastFrameTime;
			lastFrameTime = currentFrameTime;

			if (isRunning)
			{
				Input();
				Update(dt.Milliseconds / 1000.0);
				Draw();
			}
		}

		private void Input()
		{
		}

		private void Update(double dt)
		{
			sbg.Update(dt);
			Console.WriteLine(dt);
		}

		private void Draw()
		{
			DrawingGroup group = new DrawingGroup();

			group.Children.Add(new GeometryDrawing(new ImageBrush(sbg.Image), null, new RectangleGeometry(sbg.BGRect1)));
			group.Children.Add(new GeometryDrawing(new ImageBrush(sbg.Image), null, new RectangleGeometry(sbg.BGRect2)));

			for (int i = 0; i < X * Y; i++)
				group.Children.Add(new GeometryDrawing(Brushes.Black, null, new RectangleGeometry(new Rect(Poses[i], Sizes[i]))));

			group.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, Width, Height));
			CanvasSource = new DrawingImage(group);
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			isRunning = false;
		}
	}
}
