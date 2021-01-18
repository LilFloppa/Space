using System;
using System.Windows;
using System.Windows.Controls;

namespace Space
{
   public partial class MainMenu : UserControl
   {
      public event EventHandler OnStartGameClicked;

      public MainMenu()
      {
         InitializeComponent();
      }

      private void StartGameClick(object sender, RoutedEventArgs e)
      {
         OnStartGameClicked(sender, e);
      }

      private void ExitGameClick(object sender, RoutedEventArgs e)
      {
         Environment.Exit(0);
      }
   }
}
