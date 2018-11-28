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
		Character titan;
		Character mystic;
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
			addLines();
			GamePadState gst1 = GamePad.GetState(PlayerIndex.One);
			GamePadState gst2 = GamePad.GetState(PlayerIndex.Two);
			if (Screen.titleScreen.Bool == true)
			{
				Screen.Update(gameTime);
				titan = new Titan(0, 500);
				titan.LoadContent(Content);
				//mystic.LoadContent(Content);
				//mystic = new Mystic(400, 500);
			}
			else
			{
				titan.Update(gameTime, Lines, gst1);
				//mystic.Update(gameTime, Lines, gst2);
			}
			if (titan.firstCombo == true && titan.hitbox.Intersects(mystic.hitbox))
			{
				mystic.health -= titan.damage;
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
			
			else //play
			{
				foreach (Line Lines in Lines)
				{
					Lines.Draw(spriteBatch);
				}
				Screen.Draw(spriteBatch);
				for (int i = 0; i < titan.health; i++)
				{
					spriteBatch.Draw(titan.healthTexture, new Rectangle(-30, -40, 25000 / titan.health, 100), Color.White);
				}
				//need to make it half the screen width instead of 250000
				/*for (int i = 0; i < mystic.health; i++)
				{
					spriteBatch.Draw(mystic.healthTexture2, new Rectangle(480, -40, 25000 / mystic.health, 100), Color.White);
				}*/
				titan.Draw(spriteBatch);
				//mystic.Draw(spriteBatch);
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

