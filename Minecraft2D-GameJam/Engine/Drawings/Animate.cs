using System.Diagnostics;

namespace Engine.Drawings
{
    internal class Animate : Sprite
    {
        //Vector2Int spriteSheetSize;
        int curFrame = 0;
        int endFrame = 0;

        public Animate(Vector2 position, Vector2Int spriteSize) : base(position)
        {
            this.spriteSize = spriteSize;
        }
        public Animate(Vector2 position, Vector2Int spriteSize,int endFrame) : base(position)
        {
            this.spriteSize = spriteSize;
            this.endFrame = endFrame;
        }
        public void PlayAnimation(int frame, float drawOrder = 0f)
        {
            curFrame = frame % (endFrame - 1);
            int x = (curFrame * spriteSize.X) % texture.Width;
            int y = ((curFrame * spriteSize.X) / texture.Width) * spriteSize.Y;
            Rectangle rectangle = new Rectangle(x,y,spriteSize.X,spriteSize.Y);
            base.Draw(rectangle, drawOrder: drawOrder);
        }
        public void ChangeCycle(int startFrame, int endFrame)
        {
            this.curFrame = startFrame;
            this.endFrame = endFrame;
        }
    }
}
