using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Space
{
   public partial class MainWindow : Window
   {
      public ImageSource canvasSource;
      public ImageSource CanvasSource
      {
         get => canvasSource;
         set
         {
            canvasSource = value;
            Canvas.Source = canvasSource;
         }
      }

      DateTime lastFrameTime = DateTime.Now;

      public MainMenu menu = new MainMenu();
      public GameOverMenu gameOverMenu = new GameOverMenu();

      Game game;

      ImageSource white;

      public MainWindow()
      {
         InitializeComponent();

         game = new Game(this);

         game.AM.LoadTextures();
         white = game.AM.GetTexture("White.png");

         menu.OnStartGameClicked += OnStartGameClicked;
         gameOverMenu.OnMainMenuClicked += OnMainMenuClicked;

         CompositionTarget.Rendering += GameFrame;

         menu.LevelLabel.Content = "Level: " + game.Level;
         menu.TargetLabel.Content = "Target: " + game.MaxScore;

         Grid.Children.Add(menu);
      }

      private void GameFrame(object sender, EventArgs e)
      {
         DateTime currentFrameTime = DateTime.Now;
         TimeSpan dt = currentFrameTime - lastFrameTime;
         lastFrameTime = currentFrameTime;

         if (game.State == GameState.MainMenu)
         {

         }

         if (game.State == GameState.Start)
         {
            game.Start();
         }

         if (game.State == GameState.InProgress)
         {
            game.Update(dt.Milliseconds / 1000.0);
            Draw();
         }

         if (game.State == GameState.GameOver)
         {
         }
      }

      private GeometryDrawing DrawText(string text, Point origin, double angle)
      {
         Typeface typeface = new Typeface(new FontFamily("Miramonte Bold"), FontStyles.Normal, FontWeights.UltraBold, FontStretches.Normal);
         FormattedText formattedText = new FormattedText(
            text,
            CultureInfo.GetCultureInfo("en-us"),
            FlowDirection.LeftToRight,
            typeface,
            27, Brushes.Black,
            VisualTreeHelper.GetDpi(this).PixelsPerDip);

         Geometry geometry = formattedText.BuildGeometry(origin);
         TransformGroup tc = new TransformGroup();
         Rect newRect = new Rect(origin.X - geometry.Bounds.Width / 2.0, origin.Y - geometry.Bounds.Height / 2.0, geometry.Bounds.Width, geometry.Bounds.Height);

         tc.Children.Add(new TranslateTransform(-(geometry.Bounds.X - newRect.X), -(geometry.Bounds.Y - newRect.Y)));
         tc.Children.Add(new RotateTransform(angle, origin.X, origin.Y));
         geometry.Transform = tc;

         return new GeometryDrawing(Brushes.White, new Pen(Brushes.Black, 1.0), geometry);
      }

      private GeometryDrawing DrawTexture(Rect rect, ImageSource texture, Point origin, double angle)
      {
         ImageBrush brush = new ImageBrush(texture);
         brush.Transform = new RotateTransform(angle, origin.X, origin.Y);
         return new GeometryDrawing(brush, null, new EllipseGeometry(rect));
      }

      private void Draw()
      {
         DrawingGroup group = new DrawingGroup();

         // Draw background
         group.Children.Add(new GeometryDrawing(new ImageBrush(game.SBG.Image), null, new RectangleGeometry(game.SBG.BGRect1)));
         group.Children.Add(new GeometryDrawing(new ImageBrush(game.SBG.Image), null, new RectangleGeometry(game.SBG.BGRect2)));

         // Draw Actors
         foreach (var actor in game.Scene.Actors)
         {
            // Draw Texture
            Rect textureRect = new Rect(new Point(actor.Center.X - actor.DC.TexSize.Width / 2.0, actor.Center.Y - actor.DC.TexSize.Height / 2.0), actor.DC.TexSize);
            //if (actor.BC != null)
            //{
            //   group.Children.Add(new GeometryDrawing(new ImageBrush(white), null, new RectangleGeometry(actor.BoundingRect)));
           // }
            group.Children.Add(DrawTexture(textureRect, actor.DC.Texture, actor.Center, actor.RotationAngle));

            // Draw Text
            if (actor.Text != null)
               group.Children.Add(DrawText(actor.Text.Text, actor.Center, actor.RotationAngle));
         }

         group.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, Width, Height));
         CanvasSource = new DrawingImage(group);
      }

      private void OnStartGameClicked(object sender, EventArgs e)
      {
         game.State = GameState.Start;
         Grid.Children.Remove(menu);
      }

      private void OnMainMenuClicked(object sender, EventArgs e)
      {
         game.State = GameState.MainMenu;
         Grid.Children.Remove(gameOverMenu);
         Grid.Children.Add(menu);
      }
   }
}
