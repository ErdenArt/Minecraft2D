using Engine;
using Engine.TileMap;
using ImGuiNET;
using Minecraft2D_GameJam.Scripts;
using MonoGame.ImGuiNet;
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

        ImGuiRenderer rendererGUI;
        public Scene1(GameManager gameManager) : base(gameManager)
        {
            Texture2D txt = GLOBALS.Content.Load<Texture2D>("tiles/grass");

            tiles = new TileMap2D(Vector2.Zero, txt, new Vector2Int(48, 48), 16, new Vector2Int(10, 10));
            player = new Player();

            rendererGUI = new ImGuiRenderer(GLOBALS.Game);
            rendererGUI.RebuildFontAtlas();
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
            GLOBALS.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            tiles.Draw();
            player.Draw();
            GLOBALS.SpriteBatch.End();

            rendererGUI.BeginLayout(GLOBALS.Time);
            ImGui.Text($"Player position: ({player.position.X} : {player.position.Y})");
            rendererGUI.EndLayout();
        }

        protected override void Load()
        {
            
        }
    }
}
