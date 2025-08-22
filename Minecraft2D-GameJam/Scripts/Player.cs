
using Bloom_Sack.Engine.Drawings;
using Engine;
using Engine.Drawings.UI;
using Engine.TileMap;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft2D.Scripts
{
    internal class Player : Sprite
    {
        Rectangle frameRender = Rectangle.Empty;
        Rectangle hitbox { get { return new Rectangle(drawPosition.X - 5, drawPosition.Y - 8, 10, 16); } }
        enum LookDirections
        {
            Down,
            Left,
            Up,
            Right
        }
        LookDirections currentLookDir;
        float speed = 2;
        List<Tree> trees;
        TileMap2D objectTiles;

        UICanvas inventory = new UICanvas(new List<Action>()
        {
            () => UIBoxes.Draw()
        });
        
        public Player(ref List<Tree> trees, TileMap2D objectTiles)
        {
            texture = GLOBALS.Content.Load<Texture2D>("player");
            textureSize = new Vector2Int(16, 16);
            frameRender = new Rectangle(0,0, textureSize.Width, textureSize.Height);
            //spriteSizeScale = 3f;
            currentLookDir = LookDirections.Down;

            this.objectTiles = objectTiles;
            this.trees = trees;
        }
        int animateTime;
        public void Update()
        {
            Movement();

            float range = 10;
            
            Vector2 attackPos = new Vector2();
            switch (currentLookDir)
            {
                case LookDirections.Down:  attackPos = new Vector2(0,range); break;
                case LookDirections.Left:  attackPos = new Vector2(-range,0); break;
                case LookDirections.Up:    attackPos = new Vector2(0,-range); break;
                case LookDirections.Right: attackPos = new Vector2(range,0); break;
            }
            attackPos += position;
            if (Input.GetKeyDown(Keys.L))
            {
                if (objectTiles.TryGetCloseTile(attackPos, out var tile))
                {
                    objectTiles.RemoveTile(tile.Item1);
                }
                foreach (Tree tree in trees)
                {
                    if (tree.hitbox.Contains(attackPos.X, attackPos.Y))
                    {
                        tree.isActive = false;
                    }
                }
            }
        }
        void Movement()
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

            frameRender = new Rectangle(textureSize.Width * doAnimate * frame, textureSize.Height * dir, textureSize.Width, textureSize.Height);

            Rectangle newHitBox = new Rectangle(hitbox.X + (int)moveDir.X, hitbox.Y, hitbox.Width, hitbox.Height);
            List<Rectangle> allHitBoxes = new();

            allHitBoxes.AddRange([.. trees.Select(tree => tree.hitbox)]);
            allHitBoxes.AddRange(objectTiles.GetCollision());
            Debug.WriteLine(allHitBoxes.Count);
            foreach (var hitbox in allHitBoxes)
            {
                if (newHitBox.Intersects(hitbox))
                {
                    if (moveDir.X > 0)
                    {
                        position.X = hitbox.Left - newHitBox.Width / 2;
                        moveDir.X = 0;
                    }
                    if (moveDir.X < 0)
                    {
                        position.X = hitbox.Right + newHitBox.Width / 2;
                        moveDir.X = 0;
                    }
                }
            }
            position.X += moveDir.X;

            newHitBox = new Rectangle(hitbox.X, hitbox.Y + (int)moveDir.Y, hitbox.Width, hitbox.Height);
            foreach (var hitbox in allHitBoxes)
            {
                if (newHitBox.Intersects(hitbox))
                {
                    if (moveDir.Y < 0)
                    {
                        position.Y = hitbox.Bottom + newHitBox.Height / 2;
                        moveDir.Y = 0;
                    }
                    if (moveDir.Y > 0)
                    {
                        position.Y = hitbox.Top - newHitBox.Height / 2;
                        moveDir.Y = 0;
                    }
                }
            }
            position.Y += moveDir.Y;
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
        public void DrawHitBox()
        {
            Shape.DrawEmptyBox(hitbox);
        }
    }
}
