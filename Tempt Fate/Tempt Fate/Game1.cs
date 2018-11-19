using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Tempt_Fate
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont Fontone;
		Screenmanager Screen;
		Character playerone;
		Character playertwo;
		List<Line> Lines = new List<Line>();
		Texture2D mysticTexture;
		Texture2D titanTexture;
		Texture2D firstlevelTexture;
		Song song;

		public Game1()
		{
			Screen = new Screenmanager();
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 960;
			graphics.PreferredBackBufferHeight = 600;
			graphics.IsFullScreen = false;
			Content.RootDirectory = "Content";
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			Screen.LoadContent(Content);
			mysticTexture = Content.Load<Texture2D>("TestMystic");
			titanTexture = Content.Load<Texture2D>("ss");
			firstlevelTexture = Content.Load<Texture2D>("FirstLevel");
			Fontone = Content.Load<SpriteFont>("Font");
			song = Content.Load<Song>("Beat");
			MediaPlayer.Play(song);
			MediaPlayer.IsRepeating = false;
			MediaPlayer.Volume -= .75f;
			MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
		}

		private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
		{
			MediaPlayer.Play(song);
		}

		protected override void Update(GameTime gameTime)
		{
			GamePadState gst1 = GamePad.GetState(PlayerIndex.One);
			GamePadState gst2 = GamePad.GetState(PlayerIndex.Two);
			if (Screen.titleScreen.Bool == true)
			{
				Screen.Update(gameTime);
			}
			else if (Screen.SelectScreen.Bool == true)
			{
				addLines();
				Screen.Update(gameTime);
				if (gst1.IsButtonDown(Buttons.A))
				{
					playerone = new Titan(0, 500);
					playerone.LoadContent(Content);
				}
				else if (gst1.IsButtonDown(Buttons.X))
				{
					playerone = new Mystic(0 , 500);
					playerone.LoadContent(Content);
				}
				if (gst2.IsButtonDown(Buttons.A))
				{
					playertwo = new Titan(400, 500);
					playertwo.LoadContent(Content);
				}
				else if (gst2.IsButtonDown(Buttons.X))
				{
					playertwo = new Mystic(400,500);
					playertwo.LoadContent(Content);
				}
			}
			else
			{
				playerone.Update(gameTime, Lines,gst1);
			//	playertwo.Update(gameTime, Lines, gst2);
			}
			base.Update(gameTime);
		}
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();

			if (Screen.titleScreen.Bool == true)
			{
				Screen.titleScreen.Draw(spriteBatch, new Rectangle(0, 0, 1000, 600));
			}
			else if (Screen.SelectScreen.Bool == true)
			{
				Screen.SelectScreen.Draw(spriteBatch, new Rectangle(0, 0, 1000, 600));
				spriteBatch.Draw(mysticTexture, new Rectangle(125, 0, 100, 100), Color.White);
				spriteBatch.Draw(titanTexture, new Rectangle(350, 0, 100, 100), Color.White);
				spriteBatch.Draw(firstlevelTexture, new Rectangle(125, 500, 100, 100), Color.White);
				spriteBatch.DrawString(Fontone, "First, both players choose a fighter, then choose a level", new Vector2(135 , 250), Color.Black);
				spriteBatch.DrawString(Fontone, "A", new Vector2(125, 125), Color.Green);
				spriteBatch.DrawString(Fontone, "B", new Vector2(575, 125), Color.Red);
				spriteBatch.DrawString(Fontone, "X", new Vector2(350, 125), Color.Blue);
				spriteBatch.DrawString(Fontone, "Y", new Vector2(800, 125), Color.Yellow);
				spriteBatch.DrawString(Fontone, "LT", new Vector2(125, 425), Color.Black);
				spriteBatch.DrawString(Fontone, "LB", new Vector2(350, 425), Color.Black);
				spriteBatch.DrawString(Fontone, "RB", new Vector2(575, 425), Color.Black);
				spriteBatch.DrawString(Fontone, "RT", new Vector2(800, 425), Color.Black);
			}
		
			else //play
			{
				foreach (Line Lines in Lines)
				{
					Lines.Draw(spriteBatch);
				}
				Screen.Draw(spriteBatch);
				for (int i = 0; i < playerone.health; i++)
				{
					spriteBatch.Draw(playerone.healthTexture, new Rectangle(-30, -40, 25000 / playerone.health, 100), Color.White);
				}
				//need to make it half the screen width instead of 250000
				/*for (int i = 0; i < playertwo.health; i++)
				{
					spriteBatch.Draw(playertwo.healthTexture2, new Rectangle(480, -40, 25000 / playertwo.health, 100), Color.White);
				}*/
				playerone.Draw(spriteBatch);
				//playertwo.Draw(spriteBatch);
			}
			spriteBatch.End();
			base.Draw(gameTime);
		}
		public void addLines()
		{
			Lines.Add(new Line(Content.Load<Texture2D>("TestMystic"), new Vector2(0, 600), 1200, 8));
		}
	}
}

