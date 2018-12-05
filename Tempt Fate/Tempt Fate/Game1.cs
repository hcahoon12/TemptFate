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
		//fix attack box being on screen to long, add timer

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont Fontone;
		Screenmanager Screen;
		Character titan;
		Character mystic;
		List<Line> Lines = new List<Line>();
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
			//adds in a font for text to be added
			Fontone = Content.Load<SpriteFont>("Font");
			//adds in music to be playing in the backround
			song = Content.Load<Song>("Beat");
			MediaPlayer.Play(song);
			MediaPlayer.IsRepeating = false;
			MediaPlayer.Volume -= .75f;
			MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
		}
		//plays the song
		private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
		{
			MediaPlayer.Play(song);
		}

		protected override void Update(GameTime gameTime)
		{
			addLines();
			GamePadState gst1 = GamePad.GetState(PlayerIndex.One);
			GamePadState gst2 = GamePad.GetState(PlayerIndex.Two);
			//now creates and loads in the character while the person is still on the title screen
			if (Screen.titleScreen.Bool == true)
			{
				Screen.Update(gameTime);
				titan = new Titan(0, 500);
				titan.LoadContent(Content);
				mystic = new Mystic(400, 500);
				mystic.LoadContent(Content);
			}
			//once they leave titlescreen players update and can fight
			else
			{
				titan.Update(gameTime, Lines, gst1, mystic);
				mystic.Update(gameTime, Lines, gst2, titan);
			}
			//creates a health bar that can be taken away from 
			titan.healthRectangle = new Rectangle(0, -40, titan.health, 100);
			mystic.healthRectangle = new Rectangle(500, -40, mystic.health, 100);
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
			//play game
			else 
			{
				foreach (Line Lines in Lines)
				{
					Lines.Draw(spriteBatch);
				}
				Screen.Draw(spriteBatch);
				if (titan.health >= 0)
				{
					titan.Draw(spriteBatch);
					spriteBatch.Draw(titan.healthTexture, titan.healthRectangle, Color.White);
				}
				if (mystic.health >= 0)
				{
					mystic.Draw(spriteBatch);
					spriteBatch.Draw(mystic.healthTexture2, mystic.healthRectangle, Color.White);
				}
			}
			spriteBatch.End();
			base.Draw(gameTime);
		}
		//creates line
		public void addLines()
		{
			Lines.Add(new Line(Content.Load<Texture2D>("TestMystic"), new Vector2(0, 600), 1200, 8));
		}
	}
}

