using Bloom_Sack.Engine.Drawings;
using Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft2D.Scripts
{
    internal class Tree : Sprite
    {
        public bool isActive = true;
        public Rectangle hitbox { get { return new Rectangle(drawPosition.X - 8, drawPosition.Y - 8, 16, 20); } }
        public Tree(Vector2 position)
        {
            texture = GLOBALS.Content.Load<Texture2D>("Tree");
            textureSize = new Vector2Int(32,32);
            //spriteSizeScale = 3f;

            this.position = position;
        }
        public void Update()
        {

        }
        public override void Draw()
        {
            base.Draw(new Rectangle(0,0,texture.Width, texture.Height));
        }
        public void DrawHitBox()
        {
            Shape.DrawEmptyBox(hitbox);
        }
    }
}
