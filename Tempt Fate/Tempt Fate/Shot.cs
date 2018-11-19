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
	public class Shot
	{
		Texture2D startexture;
		public Rectangle hitbox;
		public int speed;
		public bool isvisible;
		int xOffset;
		int yOffset;
		int distravled;
		public Shot(Texture2D StarTexture, int X, int Y, int direction)
		{
			distravled = 0;
			speed = 10 * direction;
			if (direction < 0)
			{
				xOffset = 0;
				yOffset = 20;
				//left
			}
			else
			{
				xOffset = 45;
				yOffset = 20;
				//right
			}
			hitbox = new Rectangle(X + xOffset, Y + yOffset, 20, 20);
			isvisible = true;
			startexture = StarTexture;
		}

		public void Update()
		{
			hitbox.X += speed;
			distravled += Math.Abs(speed);

			if (distravled > 500)
			{
				isvisible = false;
			}
			else { isvisible = true; }
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(startexture, hitbox, Color.White);
		}

	}
}
