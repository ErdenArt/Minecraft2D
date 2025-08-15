
using Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft2D_GameJam.Scripts
{
    internal class Player : Sprite
    {
        public Player()
        {
            texture = GLOBALS.Content.Load<Texture2D>("player");
        }
        public void Update()
        {
            Vector2 moveDir = Input.LeftStickDirection;
            moveDir.Normalize();
            Debug.WriteLine(moveDir);
            position = moveDir;
        }
    }
}
