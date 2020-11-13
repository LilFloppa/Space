using System.Collections.Generic;

namespace Space
{
	class Scene
	{
		public List<IActor> Actors { get; set; } = new List<IActor>();
		public Game Game { get; set; }

		public Scene(Game game)
		{
			Game = game;
		}

		public void Update(double dt)
		{
			foreach (IActor actor in Actors)
				actor.Update(dt);

			Actors.RemoveAll((IActor actor) => actor.MustBeDestroyed);
		}

		public void Clear()
		{
			Actors.Clear();
		}
	}
}
