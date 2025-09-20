
using Bloom_Sack.Engine.Drawings;
using Engine;
using Engine.Drawings.UI;
using Engine.TileMap;
using EngineArt.Engine;
using EngineArt.Engine.Drawings;
using ImGuiNET;
using Minecraft2D.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Minecraft2D.Scripts
{
    class Item
    {
        enum ItemType
        {
            None,
            Block,
            Weapon,
            Tool,
            Magic
        }
        int id { get; set; }
        int count { get; set; }
        string name { get; set; }
        ItemType type { get; set; }
        public Item(int id, int count = 1)
        {
            
        }
    }
    internal class BADPlayer : Sprite
    {
        Rectangle frameRender = Rectangle.Empty;

        Collider collider = new Collider() { Width = 10, Height = 10 };
        //Rectangle hitbox { get { return new Rectangle((int)Position.X - 5, (int)Position.Y - 8, 10, 16); } }
        enum LookDirections
        {
            Down,
            Left,
            Up,
            Right
        }
        LookDirections currentLookDir = LookDirections.Down;
        //float speed = 2;
        List<Collider> collisions;
        TileMap2D objectTiles;


        // UI
        //Dictionary<int, Item> slotItems;
        UIText uiText = new UIText()
        {
            Position = new Vector2Int(10, -20),
            Text = "Inventory"
        };
        UIBoxes backBox = new UIBoxes()
        {
            BackgroundColor = Color.Red,
            Alignment = EngineArt.Engine.Drawings.UI.Alignments.Bottom,
            Bounds = new Rectangle(0, 0, 500, 30),
        };
        UIBoxes[] inventorySlot = new UIBoxes[9];
        UIBoxes[] inventorySlotItemIcon = new UIBoxes[9];
        UIText[] inventorySlotItemCount = new UIText[9];
        public BADPlayer(ref List<Collider> collisions, TileMap2D objectTiles) : base (GLOBALS.Content.Load<Texture2D>("player"))
        {
            SpriteScale = new Vector2Int(1, 1);

            this.objectTiles = objectTiles;
            this.collisions = collisions;

            Scene1.canvas.AddChild(backBox);
            backBox.AddChild(uiText);
            for (int i = 0; i < 9; i++)
            {
                inventorySlot[i] = new UIBoxes();
                inventorySlot[i].Bounds = new Rectangle(35 * i + 2,-2,30,30);
                inventorySlot[i].BackgroundColor = Color.White;

                inventorySlotItemIcon[i] = new UIBoxes();
                inventorySlotItemIcon[i].Bounds = new Rectangle(0, 0, 24, 24);
                inventorySlotItemIcon[i].Alignment = EngineArt.Engine.Drawings.UI.Alignments.Center;
                inventorySlotItemIcon[i].BackgroundColor = Color.Orange;

                inventorySlotItemCount[i] = new UIText();
                inventorySlotItemCount[i].Text = "0";
                inventorySlotItemCount[i].Alignment = EngineArt.Engine.Drawings.UI.Alignments.BottomRight;
                inventorySlotItemCount[i].TextColor = Color.Black;

                inventorySlot[i].AddChild(inventorySlotItemIcon[i]);
                inventorySlot[i].AddChild(inventorySlotItemCount[i]);

                backBox.AddChild(inventorySlot[i]);
            }
        }
        public void Update()
        {
            float range = 10;
            
            Vector2 attackPos = new Vector2();
            switch (currentLookDir)
            {
                case LookDirections.Down:  attackPos = new Vector2(0,range); break;
                case LookDirections.Left:  attackPos = new Vector2(-range,0); break;
                case LookDirections.Up:    attackPos = new Vector2(0,-range); break;
                case LookDirections.Right: attackPos = new Vector2(range,0); break;
            }
            attackPos += Position;
            if (Input.GetKeyDown(Keys.L))
            {
                if (objectTiles.TryGetCloseTile(attackPos, out var tile))
                {
                    objectTiles.RemoveTile(tile.Item1);
                }
                foreach (Collider collider in collisions)
                {
                    if (collider.Contains(attackPos))
                    {
                        collider.Parent.IsActive = false;
                    }
                }
            }
        }
        //int animateTime;
        //void Movement()
        //{
        //    bool isMoving = false;
        //    Vector2 moveDir = Input.LeftStickDirection;
        //    if (moveDir != Vector2.Zero)
        //    {
        //        moveDir.Normalize();
        //        ChangeLookDir(moveDir);
        //        moveDir = moveDir * speed;
        //        isMoving = true;
        //    }

        //    animateTime += 1;
        //    if (animateTime >= 30) animateTime = 0;

        //    int dir = (int)currentLookDir;
        //    int doAnimate = isMoving ? 1 : 0;
        //    int frame = (animateTime / 15) + 1;

        //    frameRender = new Rectangle(SingleFrameSize.Width * doAnimate * frame, SingleFrameSize.Height * dir, SingleFrameSize.Width, SingleFrameSize.Height);

        //    Rectangle newHitBox = new Rectangle(hitbox.X + (int)moveDir.X, hitbox.Y, hitbox.Width, hitbox.Height);
        //    List<Collider> allHitBoxes = new();

        //    if (collisions.Count > 0)
        //    {
        //        allHitBoxes.AddRange(collisions);
        //        allHitBoxes.AddRange(objectTiles.GetCollision());
        //    }
        //    //Debug.WriteLine(allHitBoxes.Count);
        //    foreach (var hitbox in allHitBoxes)
        //    {
        //        if (newHitBox.Intersects(hitbox))
        //        {
        //            if (moveDir.X > 0)
        //            {
        //                Position = new Vector2(0, hitbox.Left - newHitBox.Width / 2);
        //                moveDir.X = 0;
        //            }
        //            if (moveDir.X < 0)
        //            {
        //                Position = new Vector2(0, hitbox.Right + newHitBox.Width / 2);
        //                moveDir.X = 0;
        //            }
        //        }
        //    }

        //    newHitBox = new Rectangle(hitbox.X, hitbox.Y + (int)moveDir.Y, hitbox.Width, hitbox.Height);
        //    foreach (var hitbox in allHitBoxes)
        //    {
        //        if (newHitBox.Intersects(hitbox))
        //        {
        //            if (moveDir.Y < 0)
        //            {
        //                Position = new Vector2(0, hitbox.Bottom + newHitBox.Height / 2);
        //                moveDir.Y = 0;
        //            }
        //            if (moveDir.Y > 0)
        //            {
        //                Position = new Vector2(0, hitbox.Top - newHitBox.Height / 2);
        //                moveDir.Y = 0;
        //            }
        //        }
        //    }
        //    Position += moveDir;
        //}
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

        public override void Draw(float layerDepth = 0)
        {
            //TextureSource = frameRender;
            base.Draw(0);
        }
        //public void DrawHitBox()
        //{
        //    Shape.DrawEmptyBox(hitbox);
        //}
    }
}
