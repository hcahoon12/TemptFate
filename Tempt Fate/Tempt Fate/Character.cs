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

namespace Tempt_Fate
{
	public class Character
	{
		public int health;
		private Animation animation;
		private Texture2D RightWalk;
		private Texture2D LeftWalk;
		private Texture2D JumpAnimation;
		private Rectangle hitbox;
		public Texture2D healthTexture;
		public Texture2D healthTexture2;
		private bool Jumped;
		private Vector2 velocity;
		private double speed;
		private bool SomeKeyPressed;
		public List<Buttons> Combos;
		public GamePadState gst1 = GamePad.GetState(PlayerIndex.One);
		public KeyboardState KeyboardState = Keyboard.GetState();
		public GamePadState gst2 = GamePad.GetState(PlayerIndex.Two);
		public Character (Rectangle hitbox , double speed , int health)
		{
			this.speed = speed;
			this.health = health;
			this.hitbox = hitbox;
			Jumped = true;
		}
		public virtual void LoadContent(ContentManager Content)
		{ }
			public void LoadContent(ContentManager Content, string rightTexture, string leftTexture , string jumpAnimation)
		{
			RightWalk = Content.Load<Texture2D>(rightTexture);
			JumpAnimation = Content.Load<Texture2D>(jumpAnimation);
			LeftWalk = Content.Load<Texture2D>(leftTexture);
			animation = new Animation(RightWalk, 4, 1, hitbox);
			healthTexture = Content.Load<Texture2D>("Healthbar");
			healthTexture2 = Content.Load<Texture2D>("Healthbar (3)");
		}
		public void Update(GameTime gameTime, List<Line> Lines)
		{
			var gst1 = GamePad.GetState(PlayerIndex.One);
	      	var KeyboardState = Keyboard.GetState();
	    	var gst2 = GamePad.GetState(PlayerIndex.Two);
		velocity.Y += .8f; //default gravity
			/*for (int i = 0; i < Combos.Count; i++)
			{
				if (Combos.Count > 5)
				{
					Combos.RemoveAt(i);
					i--;
				}
			}*/
			for (int l = 0; l < Lines.Count; l++)
			{
				if (hitbox.Intersects(Lines[l].rectangle))
				{
					hitbox.Y--;
					velocity.Y = 0;
				}
			}
			if (hitbox.Y + hitbox.Height >= 599)
			{
				velocity.Y = 0f;
				Jumped = false;
			}
				SomeKeyPressed = false;

			if (gst1.IsButtonDown(Buttons.DPadUp) && Jumped == false || gst1.IsButtonDown(Buttons.LeftThumbstickUp) && Jumped == false || KeyboardState.IsKeyDown(Keys.Up) && Jumped == false)
			{
				animation.Update(gameTime, hitbox);
				animation.SetTexture(JumpAnimation, 0);
				animation.movetexture();
				velocity.Y -= 19;
				Jumped = true;
				SomeKeyPressed = true;
			}
			 if (gst1.IsButtonDown(Buttons.DPadRight) || gst1.IsButtonDown(Buttons.LeftThumbstickRight) || KeyboardState.IsKeyDown(Keys.Right))
			{
				for (int i = 0; i < speed; i++)
				{
					WalkRight(gameTime);

					animation.movetexture();
					hitbox.X++;
				}
				SomeKeyPressed = true;
			}
			if (gst1.IsButtonDown(Buttons.DPadLeft) || gst1.IsButtonDown(Buttons.LeftThumbstickLeft) || KeyboardState.IsKeyDown(Keys.Left))
			{
				for (int i = 0; i < speed; i++)
				{
					WalkLeft(gameTime);
				    animation.movetexture();
					hitbox.X--;
				}
				
				SomeKeyPressed = true;
			}

				if (SomeKeyPressed == false)
				{
					animation.ResetFrames();
				}
			
			animation.Update(gameTime, hitbox);
			hitbox.Y += (int)velocity.Y;
			POnScreen();
		}
		public void WalkLeft(GameTime gameTime)
		{
			animation.Update(gameTime, hitbox);
			animation.SetTexture(LeftWalk, 0);
		}
		public void WalkRight(GameTime gameTime)
		{
			animation.Update(gameTime, hitbox);
			animation.SetTexture(RightWalk, 0);
		}
		public void POnScreen()
		{
			if (hitbox.X >= 860){hitbox.X = 860;}
			if (hitbox.X <= 0) { hitbox.X = 0; }
		}
		public void Draw(SpriteBatch spritebatch)
		{
			
			animation.Draw(spritebatch);
			
		}
	}
}