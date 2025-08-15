using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Mathematic;
using System.Diagnostics;

namespace Engine.Drawings
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position = Vector2.Zero;
        public Vector2 offSet = Vector2.Zero;
        public Vector2Int spriteSize = Vector2Int.Zero;
        public bool isVisible = true;
        public Point drawPosition => position.ToPoint();
        public Sprite()
        {
        }
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            this.position = Vector2.Zero;
            spriteSize = (Vector2Int)texture.Bounds.Size;
        }
        public Sprite(Texture2D texture, Vector2 position, Vector2 offSet = default)
        {
            this.texture = texture;
            this.position = position;
            spriteSize = (Vector2Int)texture.Bounds.Size;
            this.offSet = offSet;
        }
        public Sprite(Texture2D texture, Point position)
        {
            this.texture = texture;
            this.position = position.ToVector2();
            spriteSize = (Vector2Int)texture.Bounds.Size;
        }
        public Sprite(Vector2 position)
        {
            this.position = position;
        }
        public virtual void DrawFromTopLeft()
        {
            //From Top-Left corner of position
            //For me it's easier to draw things from center
            //So it will probably will never be used
            //But it't good to have it
            if (isVisible == false)
                return;
            GLOBALS.SpriteBatch.Draw(texture,
                             new Rectangle(drawPosition.X,
                                           drawPosition.Y,
                                           spriteSize.X,
                                           spriteSize.Y),
                             Color.White);
        }
        public virtual void Draw()
        {
            if (isVisible == false)
                return;
            //From Center of position
            GLOBALS.SpriteBatch.Draw(
                              texture,
                              new Vector2(position.X - spriteSize.X / 2,
                                           position.Y - spriteSize.Y / 2),
                              new Rectangle(0, 0, spriteSize.X, spriteSize.Y), 
                              Color.White);
        }
        public virtual void DrawVector()
        {
            if (isVisible == false)
                return;
            //From Center of position
            GLOBALS.SpriteBatch.Draw(texture,
                             new Vector2(drawPosition.X - spriteSize.X / 2 + offSet.X,
                                           drawPosition.Y - spriteSize.Y / 2 + offSet.Y),
                             Color.White);
        }
        public virtual void Draw(Texture2D texture)
        {
            if (isVisible == false)
                return;
            //From Center of position
            GLOBALS.SpriteBatch.Draw(texture,
                             new Rectangle(drawPosition.X - spriteSize.X / 2,
                                           drawPosition.Y - spriteSize.Y / 2,
                                           spriteSize.X,
                                           spriteSize.Y),
                             Color.White);
        }


        float minZ = -1000f;
        float maxZ = 1000f;
        public virtual void Draw(Rectangle source = default, Color color = default, float drawOrder = 0)
        {
            if (isVisible == false)
                return;
            if (color == default)
                color = Color.White;
            if (source == default)
                source = new Rectangle(0, 0, texture.Width, texture.Height);

            // layerDepth musi być od 0f do 1f
            float layerDepth = 1f - Math.Clamp((drawOrder - minZ) / (maxZ - minZ), 0f, 1f);
            //From Center of position
            GLOBALS.SpriteBatch.Draw(
                             texture,
                             new Vector2((position.X + offSet.X - spriteSize.X / 2), (position.Y + offSet.Y - spriteSize.Y / 2)),
                             source,
                             color,
                             0f,
                             Vector2.Zero,
                             1f,
                             SpriteEffects.None,
                             layerDepth);
        }
        public static void Draw(Texture2D texture, Vector2 position, Color color)
        {
            Point spriteSize = texture.Bounds.Size;
            GLOBALS.SpriteBatch.Draw(texture,
                             new Vector2(position.X - spriteSize.X / 2,
                                         position.Y - spriteSize.Y / 2),
                             color);
        }
    }
}
