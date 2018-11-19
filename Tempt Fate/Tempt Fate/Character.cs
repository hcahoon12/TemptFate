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
		public Character (Rectangle hitbox , double speed , int health)
		{
			this.speed = speed;
			this.health = health;
			this.hitbox = hitbox;
			Combos = new List<Buttons>();
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
		public virtual void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate)
		{
			velocity.Y += .8f; //default gravity
		
			for (int i = Combos.Count-1; i >=0; i--)
			{
				
				if (Combos.Count >= 5)
				{
					Combos.RemoveAt(0);
				}
			}
			
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
			if (gamepadstate.IsButtonDown(Buttons.DPadUp) && Jumped == false || gamepadstate.IsButtonDown(Buttons.LeftThumbstickUp) && Jumped == false)
			{
				animation.Update(gameTime, hitbox);
				animation.SetTexture(JumpAnimation, 0);
				animation.movetexture();
				velocity.Y -= 19;
				Jumped = true;
				SomeKeyPressed = true;
			}
			 if (gamepadstate.IsButtonDown(Buttons.DPadRight) || gamepadstate.IsButtonDown(Buttons.LeftThumbstickRight) )
			{
				for (int i = 0; i < speed; i++)
				{
					WalkRight(gameTime);

					animation.movetexture();
					hitbox.X++;
				}
				SomeKeyPressed = true;
			}
			if (gamepadstate.IsButtonDown(Buttons.DPadLeft) || gamepadstate.IsButtonDown(Buttons.LeftThumbstickLeft))
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
		public void ButtonsPressed(GamePadState gamepadstate)
		{
			if (gamepadstate.IsButtonDown(Buttons.A))
			{
				Combos.Add(Buttons.A);
			}
			if (gamepadstate.IsButtonDown(Buttons.B))
			{
				Combos.Add(Buttons.B);
			}
			if (gamepadstate.IsButtonDown(Buttons.X))
			{
				Combos.Add(Buttons.X);
			}
			if (gamepadstate.IsButtonDown(Buttons.Y))
			{
				Combos.Add(Buttons.Y);
			}
			if (gamepadstate.IsButtonDown(Buttons.DPadLeft))
			{
				Combos.Add(Buttons.DPadLeft);
			}
			if (gamepadstate.IsButtonDown(Buttons.DPadRight))
			{
				Combos.Add(Buttons.DPadRight);
			}
			if (gamepadstate.IsButtonDown(Buttons.DPadDown))
			{
				Combos.Add(Buttons.DPadDown);
			}
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