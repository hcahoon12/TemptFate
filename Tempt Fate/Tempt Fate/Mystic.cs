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
		public Mystic(int x, int y) :base(new Rectangle(x, y, 100, 100), 6.6, 500, 600)
		{
			facingLeft = true;
		}
		public override void LoadContent(ContentManager Content)
		{
			base.LoadContent(Content, "mysticwalkright", "mysticwalkleft", "ss (2)", "Knife","ss (2)");
		}
		public override void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate, Character enemy)
		{
			//makes sure modifyAttackBox cannot do damage yet
			modifyAttackBox(-300, 500, 20, 100);
			//mana
			if (gamepadstate.IsButtonDown(Buttons.B) && mana < 600)
			{
				mana += 1.5f;
			}
			//try makes sure the inputs are in range
			try
			{
				if (gamepadstate.IsButtonDown(Buttons.X) && canAttack == true)
				{
					if (enemy.block == true)
					{
						blockDamage = 1.8f;
						mana -= 2;
					}
					if (mana > 0 && enemy.block == false)
					{
						mana -= 8;
						damage = 3;
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
						mana -= 12;
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
						mana -= 10;
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
				}
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
				}
				//block
				if (gamepadstate.IsButtonDown(Buttons.RightTrigger))
				{
					block = true;
					canAttack = false;
					canWalk = false;
				}
				else
				{
					block = false;
					canAttack = true;
					canWalk = true;
				}
			}
			catch (ArgumentOutOfRangeException ex)
			{
				//logs sytem errors to text file
				var errorfile = File.Create("C:\\TemptFate\\TemptFate\\Tempt Fate\\Tempt Fate\\bin\\Windows\\x86\\Debug\\errorTemptFate");
				errorfile.Close();
				File.WriteAllText("C:\\TemptFate\\TemptFate\\Tempt Fate\\Tempt Fate\\bin\\Windows\\x86\\Debug\\errorTemptFate", ex.Message);
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
			base.Update(gameTime, Lines, gamepadstate, enemy);
		}
		
	}
}
