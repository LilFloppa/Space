using System;
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

	class Actor : IActor
	{
		public DrawComponent DC { get; set; } = null;
		public TransformComponent TC { get; set; } = null;
		public BoxComponent BC { get; set; } = null;
		public TextComponent Text { get; set; } = null;
		public Scene Scene { get; set; } = null;

		public Point Center => TC.Position;
		public Rect BoundingRect => BC != null ? BC.BoundingRect : throw new Exception("Actor has no BoxComponent!");
		public double RotationAngle { get; set; }
		public bool MustBeDestroyed { get; set; } = false;

		public Actor(Scene scene, TransformComponent tc, DrawComponent dc)
		{
			Scene = scene;
			TC = tc;
			DC = dc;
		}

		public virtual void OnDestroy()
		{

		}

		public virtual void OnUpdate(double dt)
		{

		}
	}

}
