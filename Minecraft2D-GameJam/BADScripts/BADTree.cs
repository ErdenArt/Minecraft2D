using Bloom_Sack.Engine.Drawings;
using Engine;
using EngineArt.Engine;
using EngineArt.Engine.Drawings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft2D.Scripts
{
    internal class BADTree : Sprite
    {
        public Collider collider;
        public BADTree(Vector2 position) : base(GLOBALS.Content.Load<Texture2D>("Tree"))
        {
            this.Position = position;
            this.collider = new Collider() { Width = 10, Height = 10};
        }
        public void Update()
        {

        }
        public override void Draw(float layerDepth = 0)
        {
            base.Draw(layerDepth);
        }
        public void DrawHitBox()
        {
            collider.DrawCollider();
        }
    }
}
