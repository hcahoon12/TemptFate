using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Timers;


namespace Tempt_Fate
{
	public class Titan : Character
	{
		List<Buttons> baseAttackOne = new List<Buttons>() { Buttons.X }; //x
		List<Buttons> comboOne = new List<Buttons>() {Buttons.A , Buttons.X , Buttons.Y };//y x a
		List<Buttons> comboTwo = new List<Buttons>() { Buttons.X, Buttons.X, Buttons.A };//a x x
		List<Buttons> comboThree = new List<Buttons>() { Buttons.Y, Buttons.A, Buttons.Y }; // y a y
		List<Buttons> specialShotLeft = new List<Buttons>() { Buttons.X, Buttons.DPadLeft, Buttons.DPadDown };//down left X
		List<Buttons> specialShotRight = new List<Buttons>() { Buttons.X, Buttons.DPadRight, Buttons.DPadDown };//down right X
		public Titan(int x, int y):base(new Rectangle(x, y, 100, 100), 6.6, 500)
		{
			facingRight = true;
		}
		public override void LoadContent(ContentManager Content)
		{
			base.LoadContent(Content, "TestMystic", "ss (2)", "TestMystic" , "Knife","ss");
		}
		//might make a facing left aND RIGHT FUNCTION To change combos 
		public override void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate, Character enemy)
		{
			attackBox = new Rectangle(-300, 500, 20, 100);
			
			try
			{
				if (gamepadstate.IsButtonDown(Buttons.DPadDown) && gamepadstate.IsButtonDown(Buttons.Y))
				{
					//fix for loop so that they gradually go up and down
					for (int i = 300; i < enemy.hitbox.Y;)
					{
						enemy.hitbox.Y--;
					}
					damage = 1;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (baseAttackOne[0] == Combos[0])
				{
					damage = 5;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (gamepadstate.IsButtonDown(Buttons.Y))
				{
					damage = 5;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (gamepadstate.IsButtonDown(Buttons.A))
				{
					damage = 5;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (comboOne[0] == Combos[0] && comboOne[1] == Combos[1] && comboOne[2] == Combos[2])
				{
					//set combo animation
					damage = 1;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (comboTwo[0] == Combos[0] && comboTwo[1] == Combos[1] && comboTwo[2] == Combos[2])
				{
					damage = 1;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (comboThree[0] == Combos[0] && comboThree[1] == Combos[1] && comboThree[2] == Combos[2])
				{
					damage = 1;
					if (facingRight == true)
					{
						attackBox = new Rectangle(hitbox.X + 100, hitbox.Y, 20, 100);
					}
					else
					{
						attackBox = new Rectangle(hitbox.X - 20, hitbox.Y, 20, 100);
					}
				}
				if (specialShotLeft[0] == Combos[0] && specialShotLeft[1] == Combos[1] && specialShotLeft[2] == Combos[2] && canshoot == true)
				{
					shotDelay.Start();
					Shoot();
					canshoot = false;
				}
				if (specialShotRight[0] == Combos[0] && specialShotRight[1] == Combos[1] && specialShotRight[2] == Combos[2] && canshoot == true)
				{
					shotDelay.Start();
					Shoot();
					canshoot = false;
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

			} catch (ArgumentOutOfRangeException ex) {}
			
			base.Update(gameTime, Lines, gamepadstate, enemy);
		}
	
	}
}
