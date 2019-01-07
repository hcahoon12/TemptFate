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
	public class Titan : Character
	{
		//creates list of buttons that is are attacks
		List<Buttons> baseAttackOne = new List<Buttons>() { Buttons.X }; //x
		List<Buttons> comboOne = new List<Buttons>() {Buttons.A , Buttons.X , Buttons.Y };//y x a
		List<Buttons> comboTwo = new List<Buttons>() { Buttons.X, Buttons.X, Buttons.A };//a x x
		List<Buttons> comboThree = new List<Buttons>() { Buttons.Y, Buttons.A, Buttons.Y }; // y a y
		List<Buttons> specialShotLeft = new List<Buttons>() { Buttons.X, Buttons.DPadLeft, Buttons.DPadDown };//down left X
		List<Buttons> specialShotRight = new List<Buttons>() { Buttons.X, Buttons.DPadRight, Buttons.DPadDown };//down right X
		public Titan(int x,int y):base(new Rectangle(x, y, 100, 100), 6.6, 500, 500)
		{
			facingRight = true;
		}
		public override void LoadContent(ContentManager Content)
		{
			base.LoadContent(Content, "baseWalkRight", "baseWalkLeft", "baseWalkRight", "baseWalkLeft", "baseWalkLeft");
		}
		public override void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate, Character enemy)
		{
			//makes sure modifyAttackBox cannot do damage yet
			modifyAttackBox(-300, 500, 20, 100);
			//mana
			if (gamepadstate.IsButtonDown(Buttons.B) && mana < 500)
			{
				mana += 2f;
			}
			//try makes sure the inputs are in range
			try
			{
				if (gamepadstate.IsButtonDown(Buttons.DPadDown) && gamepadstate.IsButtonDown(Buttons.Y) && canAttack == true)
				{
					//fix for loop so that they gradually go up and down
					for (int i = 300; i < enemy.hitbox.Y;)
					{
						enemy.hitbox.Y--;
					}
					if (enemy.block == true)
					{
						blockDamage = 1.8f;
						mana -= 2;
					}
					if (mana > 0 && enemy.block == false)
					{
						
						damage = 1;
						mana -= 3;
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
				}
				if (gamepadstate.IsButtonDown(Buttons.X) && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 1.8f;
						mana -= 2;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 4;
						damage = 3;
					}
					else { damage = 0; }
					if (facingRight == true)
					{
					modifyAttackBox(hitbox.X + 100, hitbox.Y, 20, 100);
				//	attackDelay.Stop();
					//attackDelay.Start();
					}
					else
					{
						modifyAttackBox(hitbox.X - 20, hitbox.Y, 20, 100);
					}
					SomeKeyPressed = true;
				}
				if (gamepadstate.IsButtonDown(Buttons.Y) && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 2.4f;
						mana -= 3;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 7;
						damage = 7;
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
				}
				if (gamepadstate.IsButtonDown(Buttons.A) && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 2;
						mana -= 3;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 5;
						damage = 5;
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
				}
				//creates combos
				if (comboOne[0] == Combos[0] && comboOne[1] == Combos[1] && comboOne[2] == Combos[2] && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 3f;
						mana -= 5;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 20;
						damage = 10;
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
				}
				if (comboTwo[0] == Combos[0] && comboTwo[1] == Combos[1] && comboTwo[2] == Combos[2] && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 2.4f;
						mana -= 3;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 13;
						damage = 8;
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
				}
				if (comboThree[0] == Combos[0] && comboThree[1] == Combos[1] && comboThree[2] == Combos[2] && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 5f;
						mana -= 6.5f;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 20;
						damage = 15;
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
				}
				//creates a shot that that is based on down left / right and x and also has a timer
				if (specialShotLeft[0] == Combos[0] && specialShotLeft[1] == Combos[1] && specialShotLeft[2] == Combos[2] && canshoot == true && canAttack == true)
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
				}
				if (specialShotRight[0] == Combos[0] && specialShotRight[1] == Combos[1] && specialShotRight[2] == Combos[2] && canshoot == true && canAttack == true)
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
				}
				//block
				if (gamepadstate.IsButtonDown(Buttons.RightTrigger))
				{
					block = true;
					canAttack = false;
					canWalk = false;
					SomeKeyPressed = true;
				}
				else
				{
					block = false;
					canAttack = true;
					canWalk = true;
				}
			}
			catch (ArgumentOutOfRangeException ex) {}
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
			base.Update(gameTime, Lines, gamepadstate, enemy);
		}
	}
}
