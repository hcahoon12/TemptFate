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
		private Texture2D tt;
		public Rectangle hitbox;
		private int speed;
		public bool isvisible;
		private int xOffset;
		private int yOffset;
		private int distravled;
		public Shot(Texture2D ShotTexture, int X, int Y, int direction)
		{
			distravled = 0;
			speed = 10 * direction;
			//decided based on direction which way the character will shoot and where the bullet will first be created
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
			tt = ShotTexture;
		}

		public void Update()
		{
			hitbox.X += speed;
			distravled += Math.Abs(speed);
			//if the bullet travels more than 600 and if it isnt bullet will be active still
			if (distravled > 600)
			{
				isvisible = false;
			}
			else { isvisible = true; }
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(tt, hitbox, Color.White);
		}

	}
}
