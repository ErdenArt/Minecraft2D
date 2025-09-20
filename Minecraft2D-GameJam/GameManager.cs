using Minecraft2D.Scenes;
using MonoGame.ImGuiNet;

namespace Engine
{
    public class GameManager
    {
        SceneManager sceneManager;
        ImGuiRenderer rendererGUI;
        public GameManager()
        {
            //sceneManager = new SceneManager(this);
            sceneManager = new SceneManager(new Scene1(), "test");
            sceneManager.AddNewScene(new Scene2(), "haha");
            sceneManager.AddNewScene(new WorldScene(), "world");

            sceneManager.SwitchScene("world");

            rendererGUI = new ImGuiRenderer(GLOBALS.Game);
            rendererGUI.RebuildFontAtlas();
        }
        public void Update(GameTime gameTime)
        {
            sceneManager.Update();
            if (Input.GetKeyDown(Keys.P)) sceneManager.SwitchScene(1);
            if (Input.GetKeyDown(Keys.L)) sceneManager.SwitchScene("test");
            if (Input.GetKeyDown(Keys.K)) sceneManager.SwitchScene("world");
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var frame = sceneManager.GetFrame();


            spriteBatch.Begin();
            spriteBatch.Draw(frame, Vector2.Zero, Color.White);
            spriteBatch.End();

            //rendererGUI.BeginLayout(gameTime);
            //ImGui.Begin("Test");
            //ImGui.Text("FPS: " + (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString("F2"));
            //ImGui.Text("Time since start: " + gameTime.TotalGameTime.TotalSeconds.ToString("F2"));
            //ImGui.End();
            //rendererGUI.EndLayout();
        }
    }
}
