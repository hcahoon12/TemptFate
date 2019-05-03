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
using System.IO;

namespace Tempt_Fate
{
	public class Character
	{
        protected SoundEffect effectHeavy;
		protected SoundEffect effect;
		public bool canAttack;
		public bool canWalk;
		public float mana;
		public bool block;
		public Rectangle manabox;
		public Texture2D manaTexture;
		public int damage;
		public float blockDamage;
		protected bool canshoot;
		protected Rectangle attackBox;
		private static Timer combosReset;
		protected static Timer attackDelay;
		protected static Timer shotDelay;
		protected Texture2D Shottexture;
		private Texture2D hitTexture;
        public int health;
		protected Animation animation;
		protected Texture2D RightWalk;
		public List<Shot> shootlist;
		protected int Direction;
		private Texture2D LeftWalk;
		private Texture2D JumpAnimation;
		private Texture2D JumpLeft;
		public Rectangle hitbox;
		public Texture2D healthTexture;
		public Texture2D healthTexture2;
		public Rectangle healthRectangle;
		public bool Jumped;
		protected bool facingLeft;
		protected bool facingRight;
		public Vector2 velocity;
        public Vector2 velocity2;
        private double speed;
		protected Gamepadbuttons gamePadButtons;
        protected Buttons? button;
		protected bool SomeKeyPressed;
		protected List<Buttons> Combos;
		public int x;
		public int y;
		protected float volume = 1.0f;
		protected float pitch = 0.0f;
		protected float pan = 0.0f;
		protected int playerNum;

		public Character(Rectangle hitbox, double speed, int health , float mana)
		{
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
			this.mana = mana;
			this.hitbox = hitbox;
			Combos = new List<Buttons>();
			shootlist = new List<Shot>();
			canshoot = true;
			block = false;
			canAttack = true;
			canWalk = true;
			Jumped = true;
			gamePadButtons = new Gamepadbuttons();
		}

		public virtual void LoadContent(ContentManager Content)
		{ }
		public void LoadContent(ContentManager Content, string rightTexture, string leftTexture, string jumpAnimation, string JumpLeftAnimation, string shottexture, string hittexture)
		{
			//gives textures a variable so when the character is created they can change the animations
			hitTexture = Content.Load<Texture2D>(hittexture);
			RightWalk = Content.Load<Texture2D>(rightTexture);
			JumpAnimation = Content.Load<Texture2D>(jumpAnimation);
			JumpLeft = Content.Load<Texture2D>(JumpLeftAnimation);
			LeftWalk = Content.Load<Texture2D>(leftTexture);
			animation = new Animation(RightWalk, 4, 1, hitbox);
			Shottexture = Content.Load<Texture2D>(shottexture);
			healthTexture = Content.Load<Texture2D>("Healthbar");
			healthTexture2 = Content.Load<Texture2D>("Healthbar (3)");
			manaTexture = Content.Load<Texture2D>("manaTexture");
			effect = Content.Load<SoundEffect>("Slap");
            effectHeavy = Content.Load<SoundEffect>("upperCut");
        }
		
		public virtual void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate, Character enemy)
		{
			//how the attacks do damage to the other character
			if (attackBox.Intersects(enemy.hitbox) && enemy.block == false)
			{
				enemy.TakeDamage(damage);
                enemy.velocity.X += enemy.velocity2.X;
                enemy.velocity.Y += enemy.velocity2.Y;
			}
			if (attackBox.Intersects(enemy.hitbox) && enemy.block == true)
			{
				enemy.BlockDamage(blockDamage);
			}
			velocity.Y += .8f; //default gravity
			KeyboardState ks = Keyboard.GetState();
			//if no button is pressed then the timer will go off and the list clears 
			if (button != null)
			{
				//reset timer
				combosReset.Stop();
				combosReset.Start();
				//puts combos to the front of the list
				Combos.Insert(0, (Buttons)button);
				//start countdown to clear combos
				if (Combos.Count >= 5)
				{
					Combos.RemoveAt(4);
				}
			}
			//cresates interaction for lines and characters
			for (int l = 0; l < Math.Abs(velocity.Y); l++)
			{
                hitbox.Y+=(velocity.Y>0)?1:-1;
				if (hitbox.Intersects(Lines[0].rectangle))
				{
                    hitbox.Y -= (velocity.Y > 0) ? 1 : -1;
                    velocity.Y = 0;
                    Jumped = false;
                }
			}
            velocity.X += (velocity.X > 0) ? -.1f : .1f;
            //for (int l = 0; l <Math.Abs(velocity.X); l++)
            //{
            //   hitbox.X += ((int)velocity.X > 0) ? 1 : ((int)velocity.X<0)? -1:0;
            hitbox.X += (int)velocity.X;
                if (hitbox.X > 860 || hitbox.X < 0)
                {
                    hitbox.X = (velocity.X > 0) ? 860 : 0;
                    velocity.X = 0;
                    Jumped = false;
                }
          //  }
            //makes ther character able to jump when he reaches the bottom of the screen
            /*if (hitbox.Y + hitbox.Height >= 599)
			{
				velocity.Y = 0f;
				Jumped = false;
			}*/
            SomeKeyPressed = false;
			if (gamePadButtons.newUp && Jumped == false && canWalk == true)
			{
				animation.Update(gameTime, hitbox);
				if (facingRight == true)
				{
					animation.SetTexture(JumpAnimation, 0, false);
					animation.movetexture();
				}
				else
				{
					animation.SetTexture(JumpLeft, 0,false);
					animation.movetexture();
				}
				velocity.Y -= 19;
				Jumped = true;
				SomeKeyPressed = true;
			}
			if (gamePadButtons.newRight && canWalk == true)
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
			if (gamePadButtons.newLeft && canWalk == true)
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
		
			animation.Update(gameTime, hitbox);
			//hitbox.Y += (int)velocity.Y;
			POnScreen();
		}
		/// <summary>
		/// makes enemies health minus the damage given
		/// </summary>
		/// <param name="damage"></param>
		public void TakeDamage(int damage)
		{
			animation.SetTexture(hitTexture, 0,false);
			animation.movetexture();
			health = health - damage;
		}
		/// <summary>
		/// a function to get hit while blocking and take away a certain amount of damage
		/// </summary>
		/// <param name="blockDamage"></param>
		public void BlockDamage(float blockDamage)
		{
			animation.SetTexture(RightWalk, 0,false);
			animation.movetexture();
			health = health - (int)blockDamage;
		}
		/// <summary>
		/// clears combos
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void ComboReset(Object source, System.Timers.ElapsedEventArgs e)
		{
			Combos.Clear();
			combosReset.Stop();
		}
		/// <summary>
		/// function for moving attackbox
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void modifyAttackBox(int x, int y, int width , int height)
		{
			attackBox = new Rectangle(x, y, 20 ,100);
		}
		/// <summary>
		/// makes it where the character can only shoot once every two seconds
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		public void ShotDelay(Object source, System.Timers.ElapsedEventArgs e)
		{
			if (canshoot == false)
			{
				shotDelay.Stop();
				canshoot = true;
			}
		}
		/// <summary>
		/// creates new bullet
		/// </summary>
		protected void Shoot()
		{
			Shot newShot = new Shot(Shottexture, hitbox.X, hitbox.Y, Direction);
			if (shootlist.Count() < 1)
			{
				shootlist.Add(newShot);
			}
		}
		/// <summary>
		/// below are a few functions to clean the update a bit
		/// </summary>
		/// <param name="gameTime"></param>
		public void WalkLeft(GameTime gameTime)
		{
			animation.Update(gameTime, hitbox);
			animation.SetTexture(LeftWalk, 0,false);
		}
		public void WalkRight(GameTime gameTime)
		{
			animation.Update(gameTime, hitbox);
            animation.SetTexture(RightWalk, 0, false);
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