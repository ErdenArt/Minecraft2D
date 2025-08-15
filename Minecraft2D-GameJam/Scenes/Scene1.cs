using Engine;
using Engine.TileMap;
using Minecraft2D_GameJam.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft2D_GameJam.Scenes
{
    internal class Scene1 : Scene
    {
        TileMap2D tiles;
        Player player;
        public Scene1(GameManager gameManager) : base(gameManager)
        {
            Texture2D txt = GLOBALS.Content.Load<Texture2D>("tiles/grass");

            tiles = new TileMap2D(Vector2.Zero, txt, new Vector2Int(32, 32), 16, new Vector2Int(10, 10));
            player = new Player();
        }

        public override void Activate(int enterDoor)
        {
            
        }

        public override void Update()
        {
            player.Update();
        }

        protected override void Draw()
        {
            GLOBALS.SpriteBatch.Begin();
            tiles.Draw();
            GLOBALS.SpriteBatch.End();
        }

        protected override void Load()
        {
            
        }
    }
}
