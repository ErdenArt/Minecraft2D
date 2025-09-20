using Bloom_Sack.Engine.Drawings;
using Engine;
using Engine.Drawings;
using EngineArt.Engine;
using EngineArt.Engine.Drawings;
using System.Diagnostics;

namespace Minecraft2D.MainScripts
{
    internal class Player : GameObject
    {
        Sprite sprite;
        Collider collider;
        enum LookDirections
        {
            Down,
            Left,
            Up,
            Right
        }
        LookDirections currentLookDir = LookDirections.Down;
        Rectangle frameRender = new Rectangle();
        public Player()
        {
            sprite = new Sprite(GLOBALS.Content.Load<Texture2D>("player"));
            collider = new Collider();

            this.AddChild(sprite);
            sprite.AddChild(collider);
        }
        public void Draw()
        {
            sprite.DrawFrame(frameRender);
            Shape.DrawEmptyBox(new Rectangle((int)Position.X, (int)Position.Y, 1, 1));
            collider.DrawCollider();
        }
        Vector2 moveDirection;
        int animateTime = 0;
        bool isMoving = false;
        public void Update()
        {
            moveDirection = Input.LeftStickDirection;
            Position += moveDirection;
            collider.Update(Position, new Vector2(16,16));

            isMoving = false;
            if (moveDirection != Vector2.Zero)
            {
                isMoving = true;
                ChangeLookDir(moveDirection);
                moveDirection.Normalize();
                
            }

            animateTime += 1;
            if (animateTime >= 30) animateTime = 0;


            int dir = (int)currentLookDir;
            int doAnimate = isMoving ? 1 : 0;
            int frame = (animateTime / 15) + 1;
            frameRender = new Rectangle(16 * frame * doAnimate, 16 * dir, 16, 16);
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
    }
}
