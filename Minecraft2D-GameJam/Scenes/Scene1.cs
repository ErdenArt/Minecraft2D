using Engine;
using Engine.Mathematic;
using Engine.Scenes;
using Engine.TileMap;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Minecraft2D.Scripts;
using MonoGame.ImGuiNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft2D.Scenes
{
    class Scene1 : Scene
    {
        TileMap2D backgroundTiles;
        TileMap2D objectTiles;
        Player player;
        List<Tree> tree;
        Camera camera;

        ImGuiRenderer rendererGUI;
        bool doShowTreeHitbox = false;
        bool doShowPlayerHitbox = false;

        protected override void Load()
        {
            Texture2D txt = GLOBALS.Content.Load<Texture2D>("tiles/grass");

            backgroundTiles = new TileMap2D(Vector2.Zero, txt, new Vector2Int(16, 16), 16, new Vector2Int(10, 10));
            objectTiles = new TileMap2D(Vector2.Zero, txt, new Vector2Int(16, 16), 16, new Vector2Int(3, 3));
            objectTiles.colorOfTiles = Color.Black;

            tree = new List<Tree>() { new Tree(new Vector2(100, 100)), new Tree(new Vector2(100, 400)) };

            player = new Player(ref tree, objectTiles);

            rendererGUI = new ImGuiRenderer(GLOBALS.Game);
            rendererGUI.RebuildFontAtlas();

            camera = new Camera();
            camera.Update(Vector2.Zero, 1.5f);
            //camera.Zoom = 1.5f;
        }
        public override void Activate(int enterDoor)
        {
            
        }

        public override void Update()
        {
            player.Update();
            if (Input.GetMouseDown(Button.LeftClick))
            {
                //tree.Add(new Tree(Input.GetMousePosition().ToVector2()));
            }
            tree.RemoveAll(tree => tree.isActive == false);
            camera.Update(player.position, camera.Zoom);

            if (Input.GetKeyDown(Keys.M)) ResetScene();
        }
        protected override void Draw()
        {
            GLOBALS.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetMatrix());
            backgroundTiles.Draw();
            player.Draw();
            if (doShowPlayerHitbox) player.DrawHitBox();
            foreach (Tree t in tree)
            {
                t.Draw();
                if (doShowTreeHitbox)
                {
                    t.DrawHitBox();
                }
            }
            objectTiles.Draw();
            GLOBALS.SpriteBatch.End();

            rendererGUI.BeginLayout(GLOBALS.Time);

            ImGui.Begin("idk");
            ImGui.Text($"Player position: ({player.position.X} : {player.position.Y})");
            ImGui.Text($"Camera position: ({camera.GetPosition().X} : {camera.GetPosition().Y})");
            ImGui.SliderFloat("Camera ZOOM: ", ref camera.Zoom, 0, 5);
            ImGui.Checkbox("Show tree hitbox", ref doShowTreeHitbox);
            ImGui.Checkbox("Show player hitbox", ref doShowPlayerHitbox);
            ImGui.End();

            rendererGUI.EndLayout();
        }
    }
}
