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


namespace Tempt_Fate
{
	public class Mystic : Character
	{
		//creates list of buttons that is are attacks
		List<Buttons> comboOne = new List<Buttons>() { Buttons.A, Buttons.X, Buttons.Y };//y x a
		List<Buttons> comboTwo = new List<Buttons>() { Buttons.Y, Buttons.A, Buttons.A };//a a y
		List<Buttons> comboThree = new List<Buttons>() { Buttons.X, Buttons.Y, Buttons.X }; // x y x
		List<Buttons> specialShotLeft = new List<Buttons>() { Buttons.X, Buttons.DPadLeft, Buttons.DPadDown };//down left X
		List<Buttons> specialShotRight = new List<Buttons>() { Buttons.X, Buttons.DPadRight, Buttons.DPadDown };//down right X
		private Texture2D basePunchRight;
		private Texture2D basePunchLeft;
		private Texture2D BlockRight;
		private Texture2D BlockLeft;

		public Mystic(int x, int y) :base(new Rectangle(x, y, 100, 100), 6.6, 500, 600)
		{
			facingLeft = true;
			playerNum = 2;
		}
		public override void LoadContent(ContentManager Content)
		{
			BlockRight = Content.Load<Texture2D>("BlockingMystic (2)");
			BlockLeft = Content.Load<Texture2D>("BlockingMystic");
			basePunchRight = Content.Load<Texture2D>("basePunchMystic");
			basePunchLeft = Content.Load<Texture2D>("basePunchMystic (2)");
			base.LoadContent(Content, "baseRightWalkMystic", "baseWalkLeftMystic", "JumpingMystic", "jumpingMystic (2)", "Knife", "baseWalkLeftMystic");
		}
		public override void Update(GameTime gameTime, List<Line> Lines, GamePadState gst2, Character enemy)
		{
            enemy.velocity2.X = 0;
            enemy.velocity2.Y = 0;
            KeyboardState ks = Keyboard.GetState();
			button = gamePadButtons.Update(gst2, 2);
			//makes sure modifyAttackBox cannot do damage yet
			modifyAttackBox(-300, 500, 20, 100);
			//mana
			if (gamePadButtons.newB && mana < 600)
			{
				mana += 1.5f;
			}
			if (button == Buttons.X && canAttack == true)
			{
				effect.Play(volume, pitch - .2f, pan);
				if (enemy.block == true)
				{
					blockDamage = 3.6f;
					mana -= 10;
				}
				if (mana > 0 && enemy.block == false)
				{
					mana -=22;
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
					animation.SetTexture(basePunchRight, 0,true);
					animation.movetexture();
				}
				else
				{
					modifyAttackBox(hitbox.X - 20, hitbox.Y, 20, 100);
					animation.SetTexture(basePunchLeft, 0,true);
					animation.movetexture();
				}
			}
			//makes sure combos are in range 
			if (Combos.Count > 2)
			{
				//creates a combo based on if combos is equal to input from user
				if (comboOne[0] == Combos[0] && comboOne[1] == Combos[1] && comboOne[2] == Combos[2] && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 3f;
						mana -= 5;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 28;
						damage = 10;
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
                        velocity.X = 7;
                        enemy.velocity2.X = 8;

                        modifyAttackBox(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
                        velocity.X = -7;
                        enemy.velocity2.X = -8;

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
						damage = 5;
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
						damage = 5;
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
			if (gamePadButtons.newRightTrigger)
			{
				block = true;
				canAttack = false;
				canWalk = false;
				canshoot = false;
				if (facingRight == true)
				{
					animation.SetTexture(BlockRight, 0, true);
					animation.movetexture();
				}
				else
				{
					animation.SetTexture(BlockLeft, 0,true);
					animation.movetexture();
				}
			}
			else
			{
				block = false;
				canAttack = true;
				canWalk = true;
				canshoot = true;
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
			base.Update(gameTime, Lines, gst2, enemy);
		}
		
	}
}
