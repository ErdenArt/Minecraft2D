using MonoGame.ImGuiNet;
using ImGuiNET;
using System.Diagnostics;

namespace Engine
{
    internal class GameManager
    {
        SceneManager sceneManager;
        ImGuiRenderer rendererGUI;
        public GameManager(Game game1)
        {
            sceneManager = new SceneManager(this);
            rendererGUI = new ImGuiRenderer(game1);
            rendererGUI.RebuildFontAtlas();
        }
        float fpsLimiter = 60f; // 60fps
        float time = 0;
        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyDown(Keys.O)) fpsLimiter /= 2;
            if (Input.GetKeyDown(Keys.P)) fpsLimiter *= 2;
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (time > 1 / fpsLimiter)
            {
                sceneManager.Update();
                time = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            rendererGUI.BeginLayout(gameTime);
            ImGui.Text("FPS: " + (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString("F2"));
            ImGui.Text("Time since start: " + gameTime.TotalGameTime.TotalSeconds.ToString("F2"));
            rendererGUI.EndLayout();
        }
    }
}
