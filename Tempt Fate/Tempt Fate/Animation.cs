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
	public class Animation
	{
		private Rectangle destRectangle;
		private Rectangle sourceRectangle;
		private Texture2D animation;
		private int rowindex;
		private int colsindex;
		private int cols = 8;
		private float elapsed;
		private float delay = 700f;
		private int rows;
		public Animation(Texture2D defultTexture, int cols, int rows, Rectangle rec)
		{
			animation = defultTexture;
			this.cols = cols;
			this.rows = rows;
			destRectangle = rec;
			sourceRectangle = new Rectangle(animation.Width / cols * colsindex, animation.Height / rows * rowindex, animation.Width / cols, animation.Height / rows);
		}

		public void LoadContent(ContentManager Content)
		{

		}
		//set texture is called in character to get a new texture and what row the pictuere is in
		public void SetTexture(Texture2D currentTexture, int row)
		{
			animation = currentTexture;
			if (row >= rows)
			{
				System.Diagnostics.Debug.Write("bad row index, for animated sprite");
			}
			rowindex = row;
		}
		//this is called in character class when buttons are not pressed the picture will reset back to the first frame
 		public void ResetFrames()
		{
			colsindex = 0;
			movetexture();
		}
		public void Update(GameTime gameTime, Rectangle drawRectangle)
		{
			destRectangle = drawRectangle;
			elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			if (elapsed >= delay)
			{
				colsindex++;
				if (colsindex >= cols)
				{
					colsindex = 0;
				}
				elapsed = 0;
			}
		}
		//This makes the persons frame actually cycle through 
		public void movetexture()
		{
			sourceRectangle = new Rectangle(animation.Width / cols * colsindex, animation.Height / rows * rowindex, animation.Width / cols, animation.Height / rows);
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(animation, destRectangle, sourceRectangle, Color.White);
		}
	}
}

