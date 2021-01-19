using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Space.Managers
{
   class AssetManager
   {
      public Dictionary<string, ImageSource> Textures { get; set; } = new Dictionary<string, ImageSource>();

      public ImageSource GetTexture(string path)
      {
         if (Textures.ContainsKey(path))
            return Textures[path];
         else
            throw new Exception("Texture doesn't exist!");
      }

      public void LoadTextures()
      {
         var assembly = Assembly.GetExecutingAssembly();
         string[] resources = assembly.GetManifestResourceNames();
         foreach (string resource in resources)
         {
            Console.WriteLine(resource);
            if (resource.EndsWith(".png"))
            {
               Stream stream = assembly.GetManifestResourceStream(resource);

               PngBitmapDecoder decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
               ImageSource source = decoder.Frames[0];

               string[] tokens = resource.Split('.');
               string name = tokens[tokens.Length - 2];
               Textures.Add(name + ".png", source);
            }
         }
      }
   }
}
