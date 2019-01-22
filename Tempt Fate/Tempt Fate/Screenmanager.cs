using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace Tempt_Fate
{
	public class Screenmanager
	{
		private SoundEffect glassSound;
		private SoundEffect waterSound;
		List<Buttons> comboOne = new List<Buttons>() { Buttons.A, Buttons.X, Buttons.Y };
		List<Buttons> Shot = new List<Buttons>() { Buttons.X, Buttons.DPadRight, Buttons.DPadDown };
		public bool tutorialone;
		public bool tutorialtwo;
		public bool tutorialthree;
		public bool tutorialfour;
		public bool tutorialfive;
		public bool tutorialsix;
		public bool tutorialseven;
		public bool tutorialeight;
		public bool tutorialdone;
		public bool continuePlay;
		public bool music;
		public bool exit;
		public Levels FirstLevel;
		public Levels titleScreen;
		public Levels selectScreen;
		public Levels tutorialScreen;
		public Levels settingsScreen;
		public Levels pauseScreen;
		public bool Play;
		private bool Tutorial;
		private bool settings;
		private bool Quit;
		public Rectangle LinePosition;
		public Texture2D LineTexture;
		private int x;
		private int y;
		private Gamepadbuttons gamePadButtons;
		private List<Buttons> Combos;


		public Screenmanager()
		{
			gamePadButtons = new Gamepadbuttons();
			titleScreen = new Levels();
			FirstLevel = new Levels();
			selectScreen = new Levels();
			tutorialScreen = new Levels();
			settingsScreen = new Levels();
			pauseScreen = new Levels();
			LinePosition = new Rectangle(x, y, 0, 0);
			titleScreen.Bool = true;
			selectScreen.Bool = false;
			FirstLevel.Bool = false;
			pauseScreen.Bool = false;
			Play = true;
			Tutorial = false;
			settings = false;
			Quit = false;
			tutorialone = true;
			Combos = new List<Buttons>();
		}
		public void LoadContent(ContentManager Content)
		{
			titleScreen.LoadContent(Content, "TitleSreen");
			FirstLevel.LoadContent(Content, "FirstLevel");
			selectScreen.LoadContent(Content, "SelectScreen2");
			LineTexture = Content.Load<Texture2D>("Arrow");
			tutorialScreen.LoadContent(Content, "tutoialScreen");
			settingsScreen.LoadContent(Content, "SettingScreen3");
			pauseScreen.LoadContent(Content, "pauseScreen2");
			glassSound = Content.Load<SoundEffect>("Glass");
			waterSound = Content.Load<SoundEffect>("soundscrate-water-drip-single-3");
		}
		public void Update(GameTime gameTime)
		{
			titleScreen.Update();
			FirstLevel.Update();
			selectScreen.Update();
			pauseScreen.Update();
			tutorialScreen.Update();
			settingsScreen.Update();
			var gst1 = GamePad.GetState(PlayerIndex.One);
			Buttons? button = gamePadButtons.Update(gst1 , 1);
			//makes sure combos are all set for the tutorial
			if (button != null)
			{
				Combos.Insert(0, (Buttons)button);
			}
			if (Combos.Count >= 5)
			{
				Combos.RemoveAt(4);
			}
			if (titleScreen.Bool == true)
			{
				if (button == Buttons.Start)
				{
					glassSound.Play();
					titleScreen.Bool = false;
					selectScreen.Bool = true;
				}
			}
			//select screen lets player choose between three options
			else if (selectScreen.Bool == true)
			{
				if (button == Buttons.DPadDown && Play == true)
				{
					waterSound.Play();
					Tutorial = true;
					Play = false;
				}
				else if (button == Buttons.DPadDown && Tutorial == true)
				{
					waterSound.Play();
					settings = true;
					Tutorial = false;
				}
				else if (button == Buttons.DPadDown && settings == true)
				{
					waterSound.Play();
					settings = false;
					Quit = true;
				}
				else if (button == Buttons.DPadUp && Tutorial == true)
				{
					waterSound.Play();
					Play = true;
					Tutorial = false;
				}
				else if (button == Buttons.DPadUp && Quit == true)
				{
					waterSound.Play();
					settings = true;
					Quit = false;
				}
				else if (button == Buttons.DPadUp && settings == true)
				{
					waterSound.Play();
					settings = false;
					Tutorial = true;
				}
			}
			if (Play == true)
			{
				LinePosition = new Rectangle(250, 70, 50, 50);
				if (button == Buttons.A)
				{
					glassSound.Play();
					FirstLevel.Bool = true;
					selectScreen.Bool = false;
					tutorialScreen.Bool = false;
				}
			}
			else if (Tutorial == true)
			{
				LinePosition = new Rectangle(250, 205, 50, 50);
				if (button == Buttons.A)
				{
					glassSound.Play();
					tutorialScreen.Bool = true;
					selectScreen.Bool = false;
				}
			}
			else if (settings == true)
			{
				LinePosition = new Rectangle(250, 330, 50, 50);
				if (button == Buttons.A)
				{
					glassSound.Play();
					settingsScreen.Bool = true;
					selectScreen.Bool = false;
					music = true;
				}
			}
			else if (Quit == true)
			{
				LinePosition = new Rectangle(250, 470, 50, 50);
				if (button == Buttons.A)
				{
					glassSound.Play();
					System.Environment.Exit(0);
				}
			}
			if (music == true)
			{
				LinePosition = new Rectangle(200, 220, 100, 100);
			}
			//goes to tutorial and runs through diffrent types of things player needs to know
			if (tutorialScreen.Bool == true)
			{
				if (tutorialone == true)
				{
					if (gst1.IsButtonDown(Buttons.DPadRight))
					{
						tutorialone = false;
						tutorialtwo = true;
					}
				}
				else if (tutorialtwo == true)
				{
					if (gst1.IsButtonDown(Buttons.DPadUp))
					{
						tutorialtwo = false;
						tutorialthree = true;
					}
				}
				else if (tutorialthree == true)
				{
					if (button == Buttons.X)
					{
						tutorialfour = true;
						tutorialthree = false;
					}
				}
				else if (tutorialfour == true)
				{
					if (button == Buttons.Y)
					{
						tutorialfour = false;
						tutorialfive = true;
					}
				}
				else if (tutorialfive == true)
				{
					try
					{
						if (comboOne[0] == Combos[0] && comboOne[1] == Combos[1] && comboOne[2] == Combos[2])
						{
							tutorialsix = true;
							tutorialfive = false;
						}
					}
					catch (ArgumentOutOfRangeException ex)
					{
						//logging system errors
						var errorfile = File.Create("C:\\TemptFate\\TemptFate\\Tempt Fate\\Files\\errorTemptFateScreenManager");
						errorfile.Close();
						File.WriteAllText("C:\\TemptFate\\TemptFate\\Tempt Fate\\Files\\errorTemptFateScreenManager", ex.Message);
					}
				}
				else if (tutorialsix == true)
				{
					try
					{
						if (Shot[0] == Combos[0] && Shot[1] == Combos[1] && Shot[2] == Combos[2])
						{
							tutorialsix = false;
							tutorialseven = true;
						}
					}
					catch (ArgumentOutOfRangeException ex)
					{
						var errorfile = File.Create("C:\\TemptFate\\TemptFate\\Tempt Fate\\Files\\errorTemptFate");
						errorfile.Close();
						File.WriteAllText("C:\\TemptFate\\TemptFate\\Tempt Fate\\Files\\errorTemptFate", ex.Message);
					}
				}
				else if (tutorialseven == true)
				{
					if (gst1.IsButtonDown(Buttons.B))
					{
						tutorialseven = false;
						tutorialeight = true;
					}
				}
				else if (tutorialeight == true)
				{
					if (gst1.IsButtonDown(Buttons.RightTrigger))
					{
						tutorialeight = false;
						tutorialdone = true;
					}
				}
			}
			if (tutorialdone == true)
			{
				selectScreen.Bool = true;
				tutorialdone = false;
				tutorialone = true;
				Play = true;
			}
			else if (pauseScreen.Bool == true)
			{
				if (gst1.IsButtonDown(Buttons.DPadDown))
				{
					continuePlay = false;
					exit = true;
				}
				else if (gst1.IsButtonDown(Buttons.DPadUp))
				{
					exit = false;
					continuePlay = true;
				}
			}
			if (continuePlay == true)
			{
				LinePosition = new Rectangle(200, 300, 100, 100);
				if (button == Buttons.A)
				{
					FirstLevel.Bool = true;
					pauseScreen.Bool = false;
					continuePlay = false;
				}
			}
			else if (exit == true)
			{
				LinePosition = new Rectangle(200, 450, 100, 100);
				if (button == Buttons.A)
				{
					glassSound.Play();
					selectScreen.Bool = true;
					pauseScreen.Bool = false;
					exit = false;
					Play = true;
				}
			}
		}
		public virtual void Draw(SpriteBatch spritebatch)
		{
			if (FirstLevel.Bool == true)
			{
				FirstLevel.Draw(spritebatch, new Rectangle(0, 0, 1000, 600));
			}
		}
	}
}
