using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Space
{
    public class  VusialHost : FrameworkElement
    {
        private readonly VisualCollection _children;

        public VusialHost() =>_children = new VisualCollection(this);

        public void DrawQuad(Rect rect, Brush brush)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawRectangle(brush, null, rect);
            drawingContext.Close();

            _children.Add(drawingVisual);
        }

        public void DrawQuad(Rect rect, BitmapImage image)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawRectangle(new ImageBrush(image), null, rect);
            drawingContext.Close();

            _children.Add(drawingVisual);
        }

        public void Clear() => _children.Clear();

        protected override int VisualChildrenCount => _children.Count;

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
                throw new ArgumentOutOfRangeException();

            return _children[index];
        }
    }
}
