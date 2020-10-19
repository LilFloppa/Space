using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Space
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Image image = new Image();

        ScrollingBackground sbg;
       

        bool isRunning = true;

        DateTime lastFrameTime = DateTime.Now;

        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/SeamlessBackground.png"));
            image.Source = bitmap;

            image.Height = bitmap.Height;
            image.Width = bitmap.Width;
            sbg = new ScrollingBackground(image, 400.0);

            CompositionTarget.Rendering += GameFrame;
        }

        private void GameFrame(object sender, EventArgs e)
        {
            DateTime currentFrameTime = DateTime.Now;
            TimeSpan dt = currentFrameTime - lastFrameTime;
            lastFrameTime = currentFrameTime;

            if (isRunning)
            {
                Input();
                Update(dt.Milliseconds / 1000.0);
                Draw();
            }
        }

        private void Input()
        {
        }

        private void Update(double dt)
        {
            sbg.Update(dt);
        }

        private void Draw()
        {
            Scene.Children.Clear();
            sbg.Draw(Scene);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isRunning = false;
        }
    }
}
