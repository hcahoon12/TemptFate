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
	public class Screenmanager
	{
		public Levels FirstLevel;
		public Levels titleScreen;
		public Screenmanager()
		{
			titleScreen = new Levels();
			FirstLevel = new Levels();
			titleScreen.Bool = true;
			
		}
		public void Update(GameTime gameTime)
		{
			titleScreen.Update();
			FirstLevel.Update();
			var gst1 = GamePad.GetState(PlayerIndex.One);
			var KeyboardState = Keyboard.GetState();
			if (titleScreen.Bool == true)
			{
				if (gst1.IsButtonDown(Buttons.Start) || KeyboardState.IsKeyDown(Keys.Space))
				{
					titleScreen.Bool = false;
					FirstLevel.Bool = true;
				}
			}
		}
		public void LoadContent(ContentManager Content)
		{
			titleScreen.LoadContent(Content, "TitleSreen");
			FirstLevel.LoadContent(Content, "FirstLevel");
		}
		public void Draw(SpriteBatch spritebatch)
		{
			if (FirstLevel.Bool == true)
			{
				FirstLevel.Draw(spritebatch, new Rectangle(0, 0, 1000, 600));
			}
		}
	}

}
