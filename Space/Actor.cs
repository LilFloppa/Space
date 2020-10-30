using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Space
{
	interface IActor
	{
		public DrawComponent DC { get; set; }
		public TransformComponent TC { get; set; }
		public BoxComponent BC { get; set; }
		
		public MainWindow Window { get; set; }

		public Point Center { get; }
		public Rect BoundingRect { get; }

		public void Update(double dt);
	}

	interface IActorController
	{
		public IActor Owner { get; set; }
		public void Update(double dt);
	}
}
