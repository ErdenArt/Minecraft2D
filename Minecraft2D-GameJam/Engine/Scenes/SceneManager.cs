using Minecraft2D.Scenes;
using System.Collections.Generic;

namespace Engine.Scenes
{
    enum SceneEnum
    {
        Scene1, Scene2
    }
    internal class SceneManager
    {
        public SceneEnum ActiveScene { get; private set; }
        private readonly Dictionary<SceneEnum, Scene> _scenes = new Dictionary<SceneEnum, Scene>();

        public SceneManager(GameManager gameManager)
        {
            _scenes.Add(SceneEnum.Scene1, new Scene1(gameManager));

            ActiveScene = SceneEnum.Scene1;
            _scenes[ActiveScene].Activate(0);
        }
        public void SwitchScene(SceneEnum scene, int enterDoor = 0)
        {
            ActiveScene = scene;
            _scenes[ActiveScene].Activate(enterDoor);
        }
        public void Update()
        {
            _scenes[ActiveScene].Update();
        }
        public RenderTarget2D GetFrame()
        {
            return _scenes[ActiveScene].GetFrame();
        }
    }
}
