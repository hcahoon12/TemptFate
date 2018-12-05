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
	public class Line
	{
		public Texture2D texture;
		public Vector2 position;
		public Rectangle rectangle;
		//creates a new line where later on can be given a texture position and size
		public Line(Texture2D newTexture, Vector2 newPosition, int Width, int Height)
		{
			texture = newTexture;
			position = newPosition;
			rectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
		}
		public void Draw(SpriteBatch spritebatch)
		{
			spritebatch.Draw(texture, rectangle, Color.White);
		}
		
	}

}
