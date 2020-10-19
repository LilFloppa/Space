using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Space
{
    class ScrollingBackground
    {
        public double Velocity { get; private set; } = 0.0;
        public double Offset { get; private set; } = 0.0;

        public Rect BGRect1 { get; set; }
        public Rect BGRect2 { get; set; }

        public BitmapImage Image { get; set; }

        public double Width { get; private set; }
        public double Height { get; private set; }
        
        public ScrollingBackground(BitmapImage background, double velocity)
        {
            Velocity = velocity;
            Image = background;

            Width = background.Width;
            Height = background.Height;

            BGRect1 = new Rect(0.0, 0.0, Width, Height);
            BGRect2 = new Rect(Height, 0.0, Width, Height);
        }

        public void Update(double dt)
        {
            Offset += Velocity * dt;

            if (Offset >= Height)
                Offset = 0.0;

            BGRect1 = new Rect(0.0, Offset - Height + 1, Width, Height);
            BGRect2 = new Rect(0.0, Offset, Width, Height);
        }
    }
}
