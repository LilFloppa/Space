using System.Windows;

namespace Space
{
	interface IActor
	{
		public DrawComponent DC { get; set; }
		public TransformComponent TC { get; set; }
		public BoxComponent BC { get; set; }
		public TextComponent Text { get; set; }
		
		public Scene Scene { get; set; }

		public Point Center { get; }
		public Rect BoundingRect { get; }
		public double RotationAngle { get; set; }

		public bool MustBeDestroyed { get; set; }

		public void OnUpdate(double dt);
		public void OnDestroy();
	}

	interface IActorController
	{
		public IActor Owner { get; set; }
		public Point Direction { get; set; }
		public void OnUpdate(double dt);
	}
}
