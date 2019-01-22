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
		private bool Jumped;
		protected bool facingLeft;
		protected bool facingRight;
		private Vector2 velocity;
		private double speed;
		private Gamepadbuttons gamePadButtons;
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
			//sets a couple of timers for polishing the fighting
			//attackDelay = new System.Timers.Timer();
			//attackDelay.Interval = 1000;
			//attackDelay.Elapsed += AttackDelay;
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
			effect = Content.Load<SoundEffect>("hitting");
		}
		public virtual void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate, Character enemy)
		{
			//how the attacks do damage to the other character
			if (attackBox.Intersects(enemy.hitbox))
			{
				enemy.TakeDamage(damage);
			}
			if (attackBox.Intersects(enemy.hitbox) && enemy.block == true)
			{
				enemy.BlockDamage(blockDamage);
			}
			velocity.Y += .8f; //default gravity
			KeyboardState ks = Keyboard.GetState();
			Buttons? button = gamePadButtons.Update(gamepadstate, playerNum);
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
			if (button == (Buttons.DPadUp) && Jumped == false && canWalk == true)
			{
				animation.Update(gameTime, hitbox);
				if (facingRight == true)
				{
					animation.SetTexture(JumpAnimation, 0);
					animation.movetexture();
				}
				else
				{
					animation.SetTexture(JumpLeft, 0);
					animation.movetexture();
				}
				velocity.Y -= 19;
				Jumped = true;
				SomeKeyPressed = true;
			}
			if (button == (Buttons.DPadRight) && canWalk == true)
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
			if (button == (Buttons.DPadLeft) && canWalk == true)
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
				if (facingRight == true)
				{
					animation.ResetFrames(RightWalk);
				}
				else
				{
					animation.ResetFrames(LeftWalk);
				}
			}
			animation.Update(gameTime, hitbox);
			hitbox.Y += (int)velocity.Y;
			POnScreen();
			//make a text file to check varaibeles 
			using (var stream = File.Create("C:\\TemptFate\\TemptFate\\Tempt Fate\\Files\\Debugging_VariablesCharacter")) { }
			using (StreamWriter sw = new StreamWriter("C:\\TemptFate\\TemptFate\\Tempt Fate\\Files\\Debugging_VariablesCharacter"))
			{
				sw.WriteLine("Combo reset = " + combosReset.Interval);
			    //nf.WriteLine("block = " + block);
				//nf.WriteLine("can shoot = " + canshoot);
				//nf.WriteLine("can walk = " + canWalk);
				//nf.WriteLine("health = " + health);
				//nf.WriteLine("mana = " + mana);
			}
		}
		//makes enemies health minus the damage given
		public void TakeDamage(int damage)
		{
			animation.SetTexture(hitTexture, 0);
			animation.movetexture();
			health = health - damage;
		}
		public void BlockDamage(float blockDamage)
		{
			animation.SetTexture(RightWalk, 0);
			animation.movetexture();
			health = health - (int)blockDamage;
		}
		//clears combos
		private void ComboReset(Object source, System.Timers.ElapsedEventArgs e)
		{
			Combos.Clear();
			combosReset.Stop();
		}
		//once it starts it removes the attackbox does timer
	//	public void AttackDelay(Object source, System.Timers.ElapsedEventArgs e)
		//{
		//	modifyAttackBox(-300, 500, 20, 100);
		//	attackDelay.Stop();
	//	}
		//function for moving attackbox
		public void modifyAttackBox(int x, int y, int width , int height)
		{
			attackBox = new Rectangle(x, y, 20 ,100);
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