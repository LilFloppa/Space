using System.Windows;
using System.Windows.Media;

namespace Space
{
	interface IComponent
	{
	}

	class DrawComponent : IComponent
	{ 
		public ImageSource Texture { get; set; }
		public Size TexSize { get; set; }

		public DrawComponent(ImageSource texture, Size size)
		{
			Texture = texture;
			TexSize = size;
		}
	}

	class TransformComponent : IComponent
	{
		public Point Position { get; set; }

		public TransformComponent(double x, double y) => Position = new Point(x, y);
		public TransformComponent(Point position) => Position = position;

		public void SetPosition(Point position) => Position = position;

		public void AddOffset(Point offset) => Position = new Point(Position.X + offset.X, Position.Y + offset.Y);
	}

	class BoxComponent : IComponent
	{
		public Rect BoundingRect { get; set; }
		public IActor Owner { get; private set; }

		public BoxComponent(Rect boundingRect, IActor owner)
		{
			BoundingRect = boundingRect;
			Owner = owner;
		}

		public void AddOffset(Point offset) => BoundingRect = new Rect(BoundingRect.X + offset.X, BoundingRect.Y + offset.Y, BoundingRect.Width, BoundingRect.Height);

		public void SetPosition(Point position) => BoundingRect = new Rect(position, BoundingRect.Size);
	}

	class TextComponent : IComponent
	{
		public string Text { get; set; }

		public TextComponent(string text) => Text = text;
	}
}
