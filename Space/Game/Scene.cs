﻿using System.Collections.Generic;

namespace Space
{
	class Scene
	{
		public List<IActor> Actors { get; private set; } = new List<IActor>();
		public List<IActor> NewActors { get; set; } = new List<IActor>();

		public Game Game { get; set; }

		List<IActor> ActorsToRemove = new List<IActor>();

		public Scene(Game game)
		{
			Game = game;
		}

		public void Update(double dt)
		{
			foreach (IActor actor in NewActors)
				Actors.Add(actor);

			NewActors.Clear();

			foreach (IActor actor in Actors)
			{
				actor.OnUpdate(dt);
				if (actor.MustBeDestroyed)
					ActorsToRemove.Add(actor);
			}

			foreach (IActor actor in ActorsToRemove)
			{
				actor.OnDestroy();
				Actors.Remove(actor);
			}

			ActorsToRemove.Clear();
		}

		public void Clear()
		{
			Actors.Clear();
		}
	}
}