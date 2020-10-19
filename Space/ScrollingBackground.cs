using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Space
{
    class ScrollingBackground
    {
        public double Velocity { get; private set; } = 0.0;
        public double Offset { get; private set; } = 0.0;

        public Image Background1 { get; private set; }
        public Image Background2 { get; private set; }

        public double Width { get; private set; }
        public double Height { get; private set; }
        
        public ScrollingBackground(Image background, double velocity)
        {
            Velocity = velocity;

            Background1 = new Image();
            Background1.Source = background.Source;

            Background2 = new Image();
            Background2.Source = background.Source;

            Width = background.Width;
            Height = background.Height;
        }

        public void Update(double dt)
        {
            Offset += Velocity * dt;

            if (Offset >= Height)
                Offset = 0.0;
        }

        public void Draw(Canvas scene)
        {
            Canvas.SetTop(Background1, Offset - Height + 1);
            Canvas.SetTop(Background2, Offset);

            scene.Children.Add(Background1);
            scene.Children.Add(Background2);
        }
    }
}
