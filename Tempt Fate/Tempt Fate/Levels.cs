using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Tempt_Fate
{
	public class Levels
	{
		public Texture2D Texture;
		public Rectangle location;
		public bool Bool;
		public Levels()
		{
			location = new Rectangle(0, 0, 1000, 600);
		}
		public void Update()
		{
			
		}
		public void LoadContent(ContentManager Content, string Ltexture)
		{
			Texture = Content.Load<Texture2D>(Ltexture);
		}
		public void Draw(SpriteBatch spritebatch, Rectangle location)
		{
			spritebatch.Draw(Texture, location, Color.White);
		}
	}
}

