using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Diagnostics;

namespace Tempt_Fate
{
	public class Titan : Character
	{
		//creates list of buttons that are attacks
		List<Buttons> baseAttackOne = new List<Buttons>() { Buttons.X }; //x
		List<Buttons> comboOne = new List<Buttons>() {Buttons.A , Buttons.X , Buttons.Y };//y x a
		List<Buttons> comboTwo = new List<Buttons>() { Buttons.X, Buttons.X, Buttons.A };//a x x
		List<Buttons> comboThree = new List<Buttons>() { Buttons.Y, Buttons.A, Buttons.Y }; // y a y
		List<Buttons> specialShotLeft = new List<Buttons>() { Buttons.X, Buttons.DPadLeft, Buttons.DPadDown };//down left X
		List<Buttons> specialShotRight = new List<Buttons>() { Buttons.X, Buttons.DPadRight, Buttons.DPadDown };//down right X
		private Texture2D basePunchRight;
		private Texture2D basePunchLeft;
		private Texture2D BlockRight;
		private Texture2D BlockLeft;

		public Titan(int x,int y):base(new Rectangle(x, y, 100, 100), 6.6, 500, 500)
		{
			facingRight = true;
			playerNum = 1;
		}
		public override void LoadContent(ContentManager Content)
		{
			BlockRight = Content.Load<Texture2D>("Blocking");
			BlockLeft = Content.Load<Texture2D>("Blocking (2)");
			basePunchRight = Content.Load<Texture2D>("basePunch");
			basePunchLeft = Content.Load<Texture2D>("basePunch (2)");
			base.LoadContent(Content, "base_walk_edt_mid", "base_walk_edt_mid (2)", "jumpingedt", "jumpingedt (2)", "base_walk_edt_mid (2)", "base_walk_edt_mid");
		}

		public override void Update(GameTime gameTime, List<Line> Lines, GamePadState gst1, Character enemy)
		{
            enemy.velocity2.X = 0;
            enemy.velocity2.Y = 0;
			KeyboardState ks = Keyboard.GetState();
			//makes sure modifyAttackBox cannot do damage yet
			modifyAttackBox(-300, 500, 20, 100);
			button = gamePadButtons.Update(gst1, 1);
			if (gamePadButtons.newB && mana < 500)
			{
				mana += 2f;
			}
			if (button == Buttons.X && canAttack == true)
			{
				effect.Play(volume , pitch-.2f , pan);
				if (enemy.block == true)
				{
					blockDamage = 3.6f;
					mana -= 10;
				}
				if (mana > 0 && enemy.block == false)
				{
					mana -= 22;
					damage = 15;
				}
				else { damage = 0; }
				if (facingRight == true)
				{
					modifyAttackBox(hitbox.X + 100, hitbox.Y, 20, 100);
                    animation.SetTexture(basePunchRight, 0,true);
                    animation.movetexture();
                }
				else
				{
					modifyAttackBox(hitbox.X - 20, hitbox.Y, 20, 100);
                    animation.SetTexture(basePunchLeft, 0,true);
                    animation.movetexture();

                }
				SomeKeyPressed = true;
			}
			if (button == Buttons.Y && canAttack == true)
				{
					effectHeavy.Play(volume, pitch - .2f, pan);
					if (enemy.block == true)
					{
						blockDamage = 4.4f;
						mana -= 15;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 35;
						damage = 24;
					}
					else { damage = 0; }
					if (facingRight == true)
					{
						modifyAttackBox(hitbox.X + 100, hitbox.Y, 20, 100);
						animation.SetTexture(basePunchRight, 0,true);
						animation.movetexture();
					}
					else
					{
						modifyAttackBox(hitbox.X - 20, hitbox.Y, 20, 100);
						animation.SetTexture(basePunchLeft, 0,true);
						animation.movetexture();
					}
					SomeKeyPressed = true;
				}
			if (button == Buttons.A && canAttack == true)
			{
                effect.Play(volume, pitch - .2f, pan);
                if (enemy.block == true)
				{
					blockDamage = 4;
					mana -= 13;
				}
				if (mana > 0 && enemy.block == false)
				{
					mana -= 29;
					damage = 21;
				}
				else { damage = 0; }
				if (facingRight == true)
				{
					modifyAttackBox(hitbox.X + 100, hitbox.Y, 20, 100);
					animation.SetTexture(basePunchRight, 0, true);
					animation.movetexture();
				}
				else
				{
					modifyAttackBox(hitbox.X - 20, hitbox.Y, 20, 100);
					animation.SetTexture(basePunchLeft, 0,true);
					animation.movetexture();
				}
				SomeKeyPressed = true;
			}
			//creates combos makes sure they are in range 

			if (Combos.Count > 2)
			{
                if (comboOne[0] == Combos[0] && comboOne[1] == Combos[1] && comboOne[2] == Combos[2] && canAttack == true)
                {

                    if (enemy.block == true)
                    {
                        blockDamage = 3f;
                        mana -= 5;

                    }
                    if (mana >= 20 && enemy.block == false)
                    {
                        mana -= 28;
                        damage = 10;
                        enemy.velocity2.Y = -20;
                    }
                    else { damage = 0; }
                    if (facingRight == true)
                    {
                        modifyAttackBox(hitbox.X + 100, hitbox.Y, 20, 100);
                        enemy.velocity2.X = 7;
                    }
                    else
                    {
                        modifyAttackBox(hitbox.X - 20, hitbox.Y, 20, 100);
                        enemy.velocity2.X = -7;
                    }
                    SomeKeyPressed = true;
                    Combos.Clear();
				}
				else if (comboTwo[0] == Combos[0] && comboTwo[1] == Combos[1] && comboTwo[2] == Combos[2] && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 2.4f;
						mana -= 3;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 19;
						damage = 8;
					}
					else { damage = 0; }
					if (facingRight == true)
					{
                        velocity.X += 7;
                        enemy.velocity2.X += 8;
                        modifyAttackBox(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
                        velocity.X -= 7;
                        enemy.velocity2.X -= 8;
                        modifyAttackBox(hitbox.X - 20, hitbox.Y, 20, 100);
					}
					SomeKeyPressed = true;
                    Combos.Clear();
				}
				else if (comboThree[0] == Combos[0] && comboThree[1] == Combos[1] && comboThree[2] == Combos[2] && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 5f;
						mana -= 6.5f;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 8;
						damage = 12;
					}
					else { damage = 0; }
					if (facingRight == true)
					{
						modifyAttackBox(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						modifyAttackBox(hitbox.X - 20, hitbox.Y, 20, 100);
					}
					SomeKeyPressed = true;
                    Combos.Clear();
				}
               
				//creates a shot that that is based on down left / right and x and also has a timer
				else if (specialShotLeft[0] == Combos[0] && specialShotLeft[1] == Combos[1] && specialShotLeft[2] == Combos[2] && canshoot == true && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 1.5f;
						mana -= 2;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 10;
						damage = 30;
					}
					else { damage = 0; }
					shotDelay.Start();
					Shoot();
					canshoot = false;
					SomeKeyPressed = true;
                    Combos.Clear();
				}
				else if (specialShotRight[0] == Combos[0] && specialShotRight[1] == Combos[1] && specialShotRight[2] == Combos[2] && canshoot == true && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 1.5f;
						mana -= 2;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 10;
						damage = 30;
					}
					else { damage = 0; }
					shotDelay.Start();
					Shoot();
					canshoot = false;
					SomeKeyPressed = true;
                    Combos.Clear();
				}
			}
            //block
            if (gamePadButtons.newRightTrigger && Jumped == false)
			{
				block = true;
				canAttack = false;
				canWalk = false;
				SomeKeyPressed = true;
				if (facingRight == true)
				{
					animation.SetTexture(BlockRight, 0,false);
					animation.movetexture();
				}
				else
				{
					animation.SetTexture(BlockLeft, 0,false);
					animation.movetexture();
				}
			}
			else
			{
				block = false;
				canAttack = true;
				canWalk = true;
			}
			//Update Bullets
			for (int i = 0; i < Math.Abs(shootlist.Count); i++)
				{
					shootlist[i].Update();
					attackBox = shootlist[i].hitbox;
					if (shootlist[i].hitbox.Intersects(enemy.hitbox))
					{
						damage = 50;
						shootlist[i].isvisible = false;
					}
					if (shootlist[i].isvisible == false)
					{
						shootlist.RemoveAt(i);
						i--;
					}
				}
			animation.Update(gameTime, hitbox);
			base.Update(gameTime, Lines, gst1, enemy);
		}
	}
}
