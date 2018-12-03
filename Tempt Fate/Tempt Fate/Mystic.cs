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
	public class Mystic : Character
	{
		List<Buttons> comboOne = new List<Buttons>() { Buttons.A, Buttons.X, Buttons.Y };//y x a
		List<Buttons> specialShotLeft = new List<Buttons>() { Buttons.X, Buttons.DPadLeft, Buttons.DPadDown };//down left X
		List<Buttons> specialShotRight = new List<Buttons>() { Buttons.X, Buttons.DPadRight, Buttons.DPadDown };//down right X
		public Mystic(int x, int y) :base(new Rectangle(x, y, 100, 100), 6.6, 50)
		{

		}
		public override void LoadContent(ContentManager Content)
		{
			base.LoadContent(Content, "mysticwalkright", "mysticwalkleft", "ss (2)", "Knife","ss (2)");
		}
		public override void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate, Character enemy)
		{
			try
			{
				if (comboOne[0] == Combos[0] && comboOne[1] == Combos[1] && comboOne[2] == Combos[2])
				{
					hitbox.X = 600;
					hitbox.Y = 100;
					damage = 100;
				}
				if (specialShotLeft[0] == Combos[0] && specialShotLeft[1] == Combos[1] && specialShotLeft[2] == Combos[2] || specialShotRight[0] == Combos[0] && specialShotRight[1] == Combos[1] && specialShotRight[2] == Combos[2])
				{
					shotDelay.Start();
					Shoot();
					UpdateShot();
				}
				
			}

			catch (ArgumentOutOfRangeException ex) { }
			base.Update(gameTime, Lines, gamepadstate, enemy);
		}
		
	}
}
