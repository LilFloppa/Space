using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Space
{
	class ScrollingBackground
	{
		public double Velocity { get; private set; } = 0.0;
		public double Offset { get; private set; } = 0.0;

		public Rect BGRect1 { get; private set; }
		public Rect BGRect2 { get; private set; }

		public ImageSource Image { get; set; }

		public double ImageWidth { get; private set; }
		public double ImageHeight { get; private set; }

		public ScrollingBackground(ImageSource image, double velocity)
		{
			Velocity = velocity;
			Image = image;

			ImageWidth = image.Width;
			ImageHeight = image.Height;

			BGRect1 = new Rect(0.0, 0.0, ImageWidth, ImageHeight);
			BGRect2 = new Rect(ImageHeight, 0.0, ImageWidth, ImageHeight);
		}

		public void Update(double dt)
		{
			Offset += Velocity * dt;

			if (Offset >= ImageHeight)
				Offset = 0.0;

			BGRect1 = new Rect(0.0, Offset - ImageHeight + 1, ImageWidth, ImageHeight);
			BGRect2 = new Rect(0.0, Offset, ImageWidth, ImageHeight);
		}
	}
}
