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
		public Mystic(int x, int y) :base(new Rectangle(x, y, 100, 100), 6.6, 50)
		{

		}
		public override void LoadContent(ContentManager Content)
		{
			base.LoadContent(Content, "ss", "TestMystic", "ss (2)");
		}
		public override void Update(GameTime gameTime, List<Line> Lines, GamePadState gamepadstate)
		{
		   base.Update(gameTime, Lines, gamepadstate);
		}
		public new void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}
	}
}
