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
using System.Timers;

namespace Tempt_Fate
{
	public class Character
	{
		public int damage;
		protected bool canshoot;
		protected Rectangle attackBox;
		private static Timer combosReset;
		protected static Timer shotDelay;
		protected Texture2D Shottexture;
		private Texture2D hitTexture;
		public int health;
		private Animation animation;
		private Texture2D RightWalk;
		public List<Shot> shootlist;
		protected int Direction;
		private Texture2D LeftWalk;
		private Texture2D JumpAnimation;
		public Rectangle hitbox;
		public Texture2D healthTexture;
		public Texture2D healthTexture2;
		public Rectangle healthRectangle;
		private bool Jumped;
		protected bool facingLeft;
		protected bool facingRight;
		private Vector2 velocity;
		private double speed;
		private Gamepadbuttons gamePadButtons;
		private bool SomeKeyPressed;
		protected List<Buttons> Combos;
		public Character (Rectangle hitbox , double speed , int health)
		{
			combosReset = new System.Timers.Timer();
			combosReset.Interval = 500;
			combosReset.Elapsed += ComboReset;
			shotDelay = new System.Timers.Timer();
			shotDelay.Interval = 2000;
			shotDelay.Elapsed += ShotDelay;
			this.speed = speed;
			this.health = health;
			this.hitbox = hitbox;
			Combos = new List<Buttons>();
			shootlist = new List<Shot>();
			canshoot = true;
			Jumped = true;
			gamePadButtons = new Gamepadbuttons();
		}
		public virtual void LoadContent(ContentManager Content)
		{ }
		public void LoadContent(ContentManager Content, string rightTexture, string leftTexture , string jumpAnimation, string shottexture, string hittexture)
		{
			hitTexture = Content.Load<Texture2D>(hittexture);
			RightWalk = Content.Load<Texture2D>(rightTexture);
			JumpAnimation = Content.Load<Texture2D>(jumpAnimation);
			LeftWalk = Content.Load<Texture2D>(leftTexture);
			animation = new Animation(RightWalk, 4, 1, hitbox);
			Shottexture = Content.Load<Texture2D>(shottexture);
			healthTexture = Content.Load<Texture2D>("Healthbar");
			healthTexture2 = Content.Load<Texture2D>("Healthbar (3)");
		}
		public virtual void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate,Character enemy)
		{
			//fighting
			if (attackBox.Intersects(enemy.hitbox))
			{
				enemy.TakeDamage(damage);
			}
			velocity.Y += .8f; //default gravity
			Buttons? button=gamePadButtons.Update(gamepadstate);
			if (button != null)
			{
				//reset timer
				combosReset.Stop();
				combosReset.Start();
				//puts combos to the front of the list
				Combos.Insert(0,(Buttons)button);
				//start countdown to clear combos
				if(Combos.Count >= 5)
				{
					Combos.RemoveAt(4);
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
				facingRight = true;
				facingLeft = false;
				Direction = 1;
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
				facingLeft = true;
				facingRight = false;
				Direction = -1;
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
		public void TakeDamage(int damage)
		{
			animation.SetTexture(hitTexture,0);
			animation.movetexture();
			health = health - damage;
		}
		private void ComboReset(Object source, System.Timers.ElapsedEventArgs e)
		{
			Combos.Clear();
			combosReset.Stop();
		}
		public void ShotDelay(Object source, System.Timers.ElapsedEventArgs e)
		{
			if (canshoot == false)
			{
				shotDelay.Start();
				shotDelay.Stop();
				canshoot = true;
			}
		}
		protected void Shoot()
		{
			Shot newShot = new Shot(Shottexture, hitbox.X, hitbox.Y, Direction);
			if (shootlist.Count() < 1)
			{
				shootlist.Add(newShot);
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
			foreach (Shot s in shootlist)
			{
				s.Draw(spritebatch);
			}
			animation.Draw(spritebatch);
			
		}
	}
}