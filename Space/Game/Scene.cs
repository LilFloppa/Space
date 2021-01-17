using System.Collections.Generic;
using Space.Actors;

namespace Space
{
   class Scene
   {
      public List<IActor> Actors { get; private set; } = new List<IActor>();
      public List<IActor> NewActors { get; set; } = new List<IActor>();

      public Game Game { get; set; }
      public bool endGame;

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

            if (actor is Ship && actor.MustBeDestroyed)
            {
               endGame = true;
               Game.GameOver(Game.Score, false);
            }
         }

         if (endGame)
         {
            Game.PM.BoxComponents.Clear();
            Actors.Clear();
         }
         else
            foreach (IActor actor in ActorsToRemove)
            {
               actor.OnDestroy();

               if (actor is Asteroid)
                  Game.ScoreChanged((actor as Asteroid).HP);

               Game.PM.DeleteBoxComponent(actor.BC);
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
