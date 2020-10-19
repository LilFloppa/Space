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

        Point MousePos;
        public MainWindow()
        {
            InitializeComponent();

            BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/SeamlessBackground.png"));
            image.Source = bitmap;

            image.Height = bitmap.Height;
            image.Width = bitmap.Width;
            sbg = new ScrollingBackground(image, 4.0);

            Thread gameThread = new Thread(MainLoop);
            gameThread.Start();
        }

        private void MainLoop()
        {
            bool running = false;
            Dispatcher.Invoke(() => running = isRunning);

            while (running)
            {
                Input();
                Update();
                Draw();
                Dispatcher.Invoke(() => running = isRunning);
            }

            Dispatcher.Invoke(() => Close());
        }

        private void Input()
        {
        }

        private void Update()
        {
            // Полностью обновлять состояние игры
            sbg.Update(0.01);
        }

        private void Draw()
        {
            // Отрисовывать все на канвас
            Dispatcher.Invoke(() =>
            {
                Scene.Children.Clear();
                sbg.Draw(Scene);

            });
        }

        private void CanvasOnMouseMove(object sender, MouseEventArgs e)
        {
            //MousePos = e.GetPosition(this);
           // Console.WriteLine("{0} {1}" , MousePos.X.ToString(), MousePos.Y.ToString());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isRunning = false;
        }
    }
}
