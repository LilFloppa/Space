using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Space
{
	class AssetManager
	{
		public Dictionary<string, ImageSource> Textures { get; set; } = new Dictionary<string, ImageSource>();
		
		public ImageSource GetTexture(string path)
		{
			if (Textures.ContainsKey(path))
			{
				return Textures[path];
			}
			else
			{
				
				ImageSource source = new BitmapImage(new Uri("pack://application:,,,/" + path));
				Textures.Add(path, source);
				return source;
			}
		}
	}
}
