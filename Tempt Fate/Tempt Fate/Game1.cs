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
using System.IO;

namespace Tempt_Fate
{
	public class Game1 : Game
	{
		//fix attack box being on screen to long, add timer
		//fix animation for combos and blocking, wont rotate through frames
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
			//creates and loads in the character while the person is still on the title screen
			if (Screen.titleScreen.Bool == true)
			{
				Screen.Update(gameTime);
				titan = new Titan(0, 500);
				titan.LoadContent(Content);
				mystic = new Mystic(800, 500);
				mystic.LoadContent(Content);
			}
			else if (Screen.selectScreen.Bool == true)
			{
				Screen.Update(gameTime);
				titan.mana = 500;
			}
			else if (Screen.tutorialScreen.Bool == true)
			{
				Screen.Update(gameTime);
				titan.Update(gameTime, Lines, gst1, mystic);
			}
			else if (Screen.pauseScreen.Bool == true)
			{
				Screen.Update(gameTime);
			}
			//once they leave titlescreen players update and can fight
			else
			{
				titan.Update(gameTime, Lines, gst1, mystic);
				mystic.Update(gameTime, Lines, gst2, titan);
				if (gst1.IsButtonDown(Buttons.Start) || gst2.IsButtonDown(Buttons.Start))
				{
					Screen.pauseScreen.Bool = true;
					Screen.continuePlay = true;
				}
			}
			//creates a health bar that can be taken away from 
			titan.healthRectangle = new Rectangle(0, -40, titan.health, 100);
			titan.manabox = new Rectangle(5, 20, (int)titan.mana, 40);
			mystic.healthRectangle = new Rectangle(500, -40, mystic.health, 100);
			mystic.manabox = new Rectangle(500, 20, (int)mystic.mana, 40);
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
			else if (Screen.selectScreen.Bool == true)
			{
				Screen.selectScreen.Draw(spriteBatch, new Rectangle(0, 0, 1000, 600));
				spriteBatch.Draw(Screen.LineTexture, Screen.LinePosition, Color.White);
				titan.shootlist.Clear();
			}
			//gets player on screen and gives actions for the plauer to complete
			else if (Screen.tutorialScreen.Bool == true)
			{
				Screen.tutorialScreen.Draw(spriteBatch, new Rectangle(0, 0, 1000, 600));
				titan.Draw(spriteBatch);
				spriteBatch.Draw(titan.manaTexture, titan.manabox, Color.White);
				if (Screen.tutorialone == true)
				{
					spriteBatch.DrawString(Fontone, "Press right on the arrow pad to move", new Vector2(50, 100), Color.Black);
				}
				else if (Screen.tutorialtwo == true)
				{
					spriteBatch.DrawString(Fontone, "press up on the arrow pad to jump", new Vector2(50, 100), Color.Black);
				}
				else if (Screen.tutorialthree == true)
				{
					spriteBatch.DrawString(Fontone, "press x for a base attack", new Vector2(50, 100), Color.Black);
				}
				else if (Screen.tutorialfour == true)
				{
					spriteBatch.DrawString(Fontone, "press y for another base attack", new Vector2(50, 100), Color.Black);
				}
				else if (Screen.tutorialfive == true)
				{
					spriteBatch.DrawString(Fontone, "press y , x , a for one of Titans combos", new Vector2(50, 100), Color.Black);
				}
				else if (Screen.tutorialsix == true)
				{
					spriteBatch.DrawString(Fontone, "press down right x to shoot", new Vector2(50, 100), Color.Black);
				}
				else if (Screen.tutorialseven == true)
				{
					spriteBatch.DrawString(Fontone, "Now that you have used some moves you'll need", new Vector2(50, 100), Color.Black);
					spriteBatch.DrawString(Fontone, "to restore mana so you can use more. Press B to restore mana", new Vector2(50, 150), Color.Black);
				}
				else if (Screen.tutorialeight == true)
				{
					spriteBatch.DrawString(Fontone, "'But what if they attack me?'", new Vector2(50, 100), Color.Black);
					spriteBatch.DrawString(Fontone, "dont worry RT blocks", new Vector2(50, 150), Color.Black);
				}
			}
			//play game
			else if (Screen.pauseScreen.Bool == true)
			{
				Screen.pauseScreen.Draw(spriteBatch, new Rectangle(0 , 0 ,1000, 600));
				spriteBatch.Draw(Screen.LineTexture, Screen.LinePosition, Color.White);
				spriteBatch.DrawString(Fontone, "Titan (facing right) " + titan.health, new Vector2(20, 20), Color.Purple);
				spriteBatch.DrawString(Fontone, "X ", new Vector2(20, 50), Color.White);
				spriteBatch.DrawString(Fontone, "Y ", new Vector2(20, 80), Color.White);
				spriteBatch.DrawString(Fontone, "A ", new Vector2(20, 110), Color.White);
				spriteBatch.DrawString(Fontone, "Y - X - A ", new Vector2(20, 140), Color.White);
				spriteBatch.DrawString(Fontone, "A - X - X ", new Vector2(20, 170), Color.White);
				spriteBatch.DrawString(Fontone, "Y - A - Y ", new Vector2(20, 200), Color.White);
				spriteBatch.DrawString(Fontone, "Down - Right - X ", new Vector2(20, 230), Color.White);
				spriteBatch.DrawString(Fontone, "Mystic (facing left) " + mystic.health, new Vector2(650, 20), Color.Purple);
				spriteBatch.DrawString(Fontone, "X ", new Vector2(650, 50), Color.White);
				spriteBatch.DrawString(Fontone, "Y ", new Vector2(650, 80), Color.White);
				spriteBatch.DrawString(Fontone, "A ", new Vector2(650, 110), Color.White);
				spriteBatch.DrawString(Fontone, "Y - X - A ", new Vector2(650, 140), Color.White);
				spriteBatch.DrawString(Fontone, "A - A - Y ", new Vector2(650, 170), Color.White);
				spriteBatch.DrawString(Fontone, "X - Y - X ", new Vector2(650, 200), Color.White);
				spriteBatch.DrawString(Fontone, "Down - Left - X ", new Vector2(650, 230), Color.White);
			}
			else
			{
				foreach (Line Lines in Lines)
				{
					Lines.Draw(spriteBatch);
				}
				Screen.Draw(spriteBatch);
				if (titan.health >= 0)
				{
					if (mystic.health <= 0)
					{
						saveFile("titan");
						Screen.titleScreen.Bool = true;
						Screen.Play = true;
					}
					if (titan.mana >= 0)
					{
						spriteBatch.Draw(titan.manaTexture, titan.manabox, Color.White);
					}
						titan.Draw(spriteBatch);
						spriteBatch.Draw(titan.healthTexture, titan.healthRectangle, Color.White);
					
				}
				if (mystic.health >= 0)
				{
					if (titan.health <= 0)
					{
						saveFile("mystic");
						Screen.titleScreen.Bool = true;
						Screen.Play = true;
					}
					if (mystic.mana >= 0)
					{
						spriteBatch.Draw(mystic.manaTexture, mystic.manabox, Color.White);
					}
					mystic.Draw(spriteBatch);
					spriteBatch.Draw(mystic.healthTexture2, mystic.healthRectangle, Color.White);
				}
			}
			spriteBatch.End();
			base.Draw(gameTime);
		}
		public int wins;
		public void saveFile(string username)
		{
			try
			{
			if (!File.Exists("Save.txt"))
			{
				using (var stream = File.Create("Save.txt")) { }
				using (StreamWriter nf = new StreamWriter("Save.txt"))
				{
					nf.WriteLine("titan,0");
					nf.WriteLine("mystic,0");
				}
			}
			using (StreamWriter nf = new StreamWriter("saveFile.temp"))
				{
				using (StreamReader sr = new StreamReader("Save.txt"))
				{
					string line;

					// Read and display lines from the file until 
					// the end of the file is reached. 
					while ((line = sr.ReadLine()) != null)
					{
						string[] lineArray = line.Split(',');
						if (lineArray[0].Equals(username))
						{
							wins = Convert.ToInt32(lineArray[1]) + 1;
							string lineToWrite = username + "," + wins;
							nf.WriteLine(lineToWrite);
						}
						else
						{
							nf.WriteLine(line);
							
						}
					}
				}
			}
			//File.Replace("saveFile.temp", "Save.txt", null);
			File.Delete("Save.txt");
			File.Move("saveFile.temp", "Save.txt");
		}
			catch (Exception e)
			{
				//delete savefile.temp if it exists
				File.Delete("saveFile.temp");
				// Let the user know what went wrong.
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}
		}
		//creates line
		public void addLines()
		{
			Lines.Add(new Line(Content.Load<Texture2D>("TestMystic"), new Vector2(0, 600), 1200, 8));
		}
	}
}

