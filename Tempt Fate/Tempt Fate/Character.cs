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
		protected static Timer attackDelay;
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
		public int x;
		public int y;
		public Character (Rectangle hitbox , double speed , int health)
		{
			//sets a couple of timers for polishing the fighting
			attackDelay = new System.Timers.Timer();
			attackDelay.Interval = 1000;
			attackDelay.Elapsed += AttackDelay;
			combosReset = new System.Timers.Timer();
			combosReset.Interval = 500;
			combosReset.Elapsed += ComboReset;
			shotDelay = new System.Timers.Timer();
			shotDelay.Interval = 2000;
			shotDelay.Elapsed += ShotDelay;
			shotDelay.AutoReset = true;
			shotDelay.Enabled = true;
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
			//gives textures a variable so when the character is created they can change the animations
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
			//how the attacks do damage to the other character
			if (attackBox.Intersects(enemy.hitbox))
			{
				enemy.TakeDamage(damage);
			}
			velocity.Y += .8f; //default gravity
			Buttons? button=gamePadButtons.Update(gamepadstate);
			//if no button is pressed then the timer will go off and the list clears 
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
			//cresates interaction for lines and characters
			for (int l = 0; l < Lines.Count; l++)
			{
				if (hitbox.Intersects(Lines[l].rectangle))
				{
					hitbox.Y--;
					velocity.Y = 0;
				}
			}
			//makes ther character able to jump when he reaches the bottom of the screen
			if (hitbox.Y + hitbox.Height >= 599)
			{
				velocity.Y = 0f;
				Jumped = false;
			}
				SomeKeyPressed = false;
			//if up is pressed on the gamepad the character can jump
			if (gamepadstate.IsButtonDown(Buttons.DPadUp) && Jumped == false || gamepadstate.IsButtonDown(Buttons.LeftThumbstickUp) && Jumped == false)
			{
				animation.Update(gameTime, hitbox);
				animation.SetTexture(JumpAnimation, 0);
				animation.movetexture();
				velocity.Y -= 19;
				Jumped = true;
				SomeKeyPressed = true;
			}
			//walking right
			 if (gamepadstate.IsButtonDown(Buttons.DPadRight) || gamepadstate.IsButtonDown(Buttons.LeftThumbstickRight) )
			{
				// detirmines if hes facing right for attack moves
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
			 //walking left
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
			//if no button is being pressed reset animation
			if (SomeKeyPressed == false)
				{
					animation.ResetFrames();
				}
			
			animation.Update(gameTime, hitbox);
			hitbox.Y += (int)velocity.Y;
			POnScreen();
		}
		//makes enemies health minus the damage given
		public void TakeDamage(int damage)
		{
			animation.SetTexture(hitTexture,0);
			animation.movetexture();
			health = health - damage;
		}
		//clears combos
		private void ComboReset(Object source, System.Timers.ElapsedEventArgs e)
		{
			Combos.Clear();
			combosReset.Stop();
		}
		public void AttackDelay(Object source, System.Timers.ElapsedEventArgs e)
		{
			attackBox = new Rectangle(-300, 500 , 20, 100);
			attackDelay.Stop();
		}
		//makes it where the character can only shoot once every two seconds
		public void ShotDelay(Object source, System.Timers.ElapsedEventArgs e)
		{
			if (canshoot == false)
			{
				shotDelay.Stop();
				canshoot = true;
			}
		}
		//creates new bullet
		protected void Shoot()
		{
			Shot newShot = new Shot(Shottexture, hitbox.X, hitbox.Y, Direction);
			if (shootlist.Count() < 1)
			{
				shootlist.Add(newShot);
			}
		}
	//below are a few functions to clean the update a bit
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