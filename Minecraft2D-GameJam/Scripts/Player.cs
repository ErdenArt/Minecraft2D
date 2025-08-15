
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
        Rectangle frameRender = Rectangle.Empty;
        enum LookDirections
        {
            Down,
            Left,
            Up,
            Right
        }
        LookDirections currentLookDir;
        float speed = 2;
        public Player()
        {
            texture = GLOBALS.Content.Load<Texture2D>("player");
            textureSize = new Vector2Int(16, 16);
            frameRender = new Rectangle(0,0, textureSize.Width, textureSize.Height);
            spriteSizeScale = 3f;

            currentLookDir = LookDirections.Down;
        }
        int animateTime;
        public void Update()
        {
            bool isMoving = false;
            Vector2 moveDir = Input.LeftStickDirection;
            if (moveDir != Vector2.Zero)
            {
                moveDir.Normalize();
                ChangeLookDir(moveDir);
                moveDir = moveDir * speed;
                isMoving = true;
            }

            animateTime += 1;
            if (animateTime >= 30) animateTime = 0;

            int dir = (int)currentLookDir;
            int doAnimate = isMoving ? 1 : 0;
            int frame = (animateTime / 15) + 1;

            frameRender = new Rectangle(textureSize.Width * doAnimate * frame, textureSize.Height * dir, textureSize.Width,textureSize.Height);

            position += moveDir;
        }
        void ChangeLookDir(Vector2 moveDir)
        {
            if (Math.Abs(moveDir.X) > Math.Abs(moveDir.Y))
            {
                if (moveDir.X < 0)
                {
                    currentLookDir = LookDirections.Left;
                    return;
                }
                currentLookDir = LookDirections.Right;
                return;
            }
            if (moveDir.Y < 0)
            {
                currentLookDir = LookDirections.Up;
                return;
            }
            currentLookDir = LookDirections.Down;
        }


        public override void Draw()
        {
            base.Draw(frameRender);
        }
    }
}
