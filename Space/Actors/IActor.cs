using System.Windows;

namespace Space
{
	interface IActor
	{
		public DrawComponent DC { get; set; }
		public TransformComponent TC { get; set; }
		public BoxComponent BC { get; set; }
		
		public Scene Scene { get; set; }

		public Point Center { get; }
		public Rect BoundingRect { get; }

		public bool MustBeDestroyed { get; set; }

		public void Update(double dt);
	}

	interface IActorController
	{
		public IActor Owner { get; set; }
		public Point Direction { get; set; }
		public void Update(double dt);
	}
}
