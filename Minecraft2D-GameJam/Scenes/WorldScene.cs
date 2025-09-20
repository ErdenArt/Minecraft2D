using Engine;
using Engine.TileMap;
using Minecraft2D.MainScripts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft2D.Scenes
{
    internal class WorldScene : Scene
    {
        Player player;
        Camera camera;
        TileMap2D tilemap;
        protected override void Load()
        {
            player = new Player();
            camera = new Camera();
            camera.Update(Vector2.Zero, 1f);
            tilemap = new TileMap2D(Vector2.Zero, GLOBALS.Content.Load<Texture2D>("tiles/grass"), new Vector2Int(16,16), 16, "scene.csv");
        }
        public override void Activate(int enterDoor)
        {
            
        }

        public override void Update()
        {
            player.Update();
            camera.InputUpdate();
            camera.Update(Vector2.Zero, camera.Zoom);
        }

        protected override void Draw()
        {
            GLOBALS.SpriteBatch.Begin(transformMatrix: camera.GetMatrix(), samplerState: SamplerState.PointWrap);
            tilemap.Draw();
            player.Draw();
            GLOBALS.SpriteBatch.End();
        }

    }
}
