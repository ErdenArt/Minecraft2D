using Bloom_Sack.Engine.Drawings;
using Engine;
using Engine.Drawings;
using Engine.TileMap;
using EngineArt.Engine;
using EngineArt.Engine.Drawings;
using Minecraft2D.Scenes;
using System.Collections.Generic;
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
        TileMap2D tilemap;
        public Player(TileMap2D tilemap)
        {
            sprite = new Sprite(GLOBALS.Content.Load<Texture2D>("player"));
            collider = new Collider();
            this.tilemap = tilemap;

            this.AddChild(sprite);
            sprite.AddChild(collider);

            Position = new Vector2(-50, -50);
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

            List<Collider> allHitBoxes = new();
            Collider newHitBox = new Collider(collider.X + moveDirection.X, collider.Y, collider.Width, collider.Height);
            allHitBoxes.AddRange(tilemap.GetCollision());
            foreach (var hitbox in allHitBoxes)
            {
                if (newHitBox.Intersects(hitbox))
                {
                    if (moveDirection.X > 0)
                    {
                        Position = new Vector2(hitbox.Left - collider.Width / 2, Position.Y);
                        moveDirection.X = 0;
                    }
                    if (moveDirection.X < 0)
                    {
                        Position = new Vector2(hitbox.Right + collider.Width / 2, Position.Y);
                        moveDirection.X = 0;
                    }
                }
            }

            newHitBox = new Collider(collider.X, collider.Y + (int)moveDirection.Y, collider.Width, collider.Height);
            foreach (var hitbox in allHitBoxes)
            {
                if (newHitBox.Intersects(hitbox))
                {
                    if (moveDirection.Y < 0)
                    {
                        Position = new Vector2(Position.X, hitbox.Bottom + newHitBox.Height / 2);
                        moveDirection.Y = 0;
                    }
                    if (moveDirection.Y > 0)
                    {
                        Position = new Vector2(Position.X, hitbox.Top - newHitBox.Height / 2);
                        moveDirection.Y = 0;
                    }
                }
            }
            Position += moveDirection;
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
