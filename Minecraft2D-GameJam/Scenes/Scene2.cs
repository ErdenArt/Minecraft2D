using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft2D.Scenes
{
    internal class Scene2 : Scene
    {
        public override void Activate(int enterDoor)
        {
            //throw new NotImplementedException();
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }

        protected override void Draw()
        {
            //throw new NotImplementedException();
            GLOBALS.SpriteBatch.Begin();
            GLOBALS.SpriteBatch.DrawString(GLOBALS.Font, "you are bitch", Vector2.Zero, Color.White);
            GLOBALS.SpriteBatch.DrawString(GLOBALS.Font, "you are bitch", Vector2.One * 10, Color.White);
            GLOBALS.SpriteBatch.End();
        }

        protected override void Load()
        {
            //throw new NotImplementedException();
        }
    }
}
