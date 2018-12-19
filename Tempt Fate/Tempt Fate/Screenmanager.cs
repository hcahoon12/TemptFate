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
		List<Buttons> comboOne = new List<Buttons>() { Buttons.A, Buttons.X, Buttons.Y };
		List<Buttons> Shot = new List<Buttons>() { Buttons.X, Buttons.DPadRight, Buttons.DPadDown };
		public bool tutorialone;
		public bool tutorialtwo;
		public bool tutorialthree;
		public bool tutorialfour;
		public bool tutorialfive;
		public bool tutorialsix;
		public bool tutorialdone;
		public Levels FirstLevel;
		public Levels titleScreen;
		public Levels selectScreen;
		public Levels tutorialScreen;
		private bool Play;
		private bool Tutorial;
		private bool Quit;
		public Rectangle LinePosition;
		public Texture2D LineTexture;
		private int x;
		private int y;
		private Gamepadbuttons gamePadButtons;
		protected List<Buttons> Combos;

		public Screenmanager()
		{
			gamePadButtons = new Gamepadbuttons();
			titleScreen = new Levels();
			FirstLevel = new Levels();
			selectScreen = new Levels();
			tutorialScreen = new Levels();
			LinePosition = new Rectangle(x, y, 0, 0);
			titleScreen.Bool = true;
			Play = true;
			Tutorial = false;
			Quit = false;
			tutorialone = true;
			Combos = new List<Buttons>();
		}
		public void Update(GameTime gameTime)
		{
			titleScreen.Update();
			FirstLevel.Update();
			selectScreen.Update();
			tutorialScreen.Update();
			var gst1 = GamePad.GetState(PlayerIndex.One);
			var KeyboardState = Keyboard.GetState();
			Buttons? button=gamePadButtons.Update(gst1);
		// maybe put this in	Combos.Insert(0, (Buttons)button);
			if (titleScreen.Bool == true)
			{
				if (gst1.IsButtonDown(Buttons.Start) || KeyboardState.IsKeyDown(Keys.Space))
				{
					titleScreen.Bool = false;
					selectScreen.Bool = true;
				}
			}
			//select screen
			if (selectScreen.Bool == true)
			{
				
				if (button == Buttons.DPadDown && Play == true)
				{
					Tutorial = true;
					Play = false;
				}
				else if (button == Buttons.DPadDown && Tutorial == true)
				{
					Quit = true;
					Tutorial = false;
				}
				else if (button == Buttons.DPadUp && Tutorial == true)
				{
					Play = true;
					Tutorial = false;
				}
				else if (button == Buttons.DPadUp && Quit == true)
				{
					Tutorial = true;
					Quit = false;
				}
			}
			if (Play == true)
			{
				LinePosition = new Rectangle(50, 50, 100, 100);
				if (gst1.IsButtonDown(Buttons.A))
				{
					FirstLevel.Bool = true;
					selectScreen.Bool = false;
				}
			}
			else if (Tutorial == true)
			{
				LinePosition = new Rectangle(50, 250, 100, 100);
				if (gst1.IsButtonDown(Buttons.A))
				{
					tutorialScreen.Bool = true;
					selectScreen.Bool = false;
				}
			}
			else if (Quit == true)
			{
				LinePosition = new Rectangle(50, 450, 100, 100);
				if (gst1.IsButtonDown(Buttons.A))
				{
					System.Environment.Exit(0);
				}
			}
			//goes to tutorial and runs through diffrent types of things player needs to know
			if (tutorialScreen.Bool == true)
			{
				if (tutorialone == true)
				{
					if (gst1.IsButtonDown(Buttons.DPadRight) || gst1.IsButtonDown(Buttons.LeftThumbstickRight))
					{
						tutorialone = false;
						tutorialtwo = true;
					}
				}
				else if (tutorialtwo == true)
				{
					if (gst1.IsButtonDown(Buttons.DPadUp) || gst1.IsButtonDown(Buttons.LeftThumbstickUp))
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
					{ }
				}
				else if (tutorialsix == true)
				{
					try
					{
						if (Shot[0] == Combos[0] && Shot[1] == Combos[1] && Shot[2] == Combos[2])
						{
							tutorialsix = false;
							tutorialdone = true;
						}
					}
					catch(ArgumentOutOfRangeException ex)
					{ }
				}
			}
			if (tutorialdone == true)
			{
				titleScreen.Bool = true;
			}
		}
		public void LoadContent(ContentManager Content)
		{
			titleScreen.LoadContent(Content, "TitleSreen");
			FirstLevel.LoadContent(Content, "FirstLevel");
			selectScreen.LoadContent(Content, "SelectScreen");
			LineTexture = Content.Load<Texture2D>("Arrow");
			tutorialScreen.LoadContent(Content, "tutoialScreen");
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
