using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DPhysicBall
{
    class Ball
    {
        Texture2D tex;
        Vector2 pos, vel;
        float radius;
        int bX;
        int bY;
        Rectangle hitBox;
        



        public Ball(Texture2D tex, Vector2 pos, Vector2 vel, float radius, Point boundary)
        {
            this.tex = tex;
            this.pos = pos;
            this.vel = vel;
            this.radius = tex.Height/2;
            this.bX = boundary.X;
            this.bY = boundary.Y;
            this.hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

        }

        public bool CircleCollision(Ball other)
        {
            return Vector2.Distance(pos, other.pos) < (radius + other.radius);
        }

        public void Update(GameTime gameTime)
        {
            pos += vel;

            if (pos.X <= 0 && vel.X < 0 || pos.X >= bX && vel.X > 0)
            {
                vel.X *= -1;
            }

            if (pos.Y <= 0 && vel.Y < 0 || pos.Y >= bY && vel.Y > 0)
            {
                vel.Y *= -1;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, Color.White);
        }

        public Vector2 GetVel
        {
            get
            {
                return vel;
            }
            set
            {
                vel = value;
            }
        }
        public Vector2 GetPos
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
            }
        }

        //public bool PixelCollision(Ball other)

        //{
        //    Color[] dataA = new Color[tex.Width * tex.Height];
        //    tex.GetData(dataA);

        //    Color[] dataB = new Color[other.tex.Width * other.tex.Height];
        //    other.tex.GetData(dataB);

        //    int top = Math.Max(hitBox.Top, other.hitBox.Top);
        //    int bottom = Math.Min(hitBox.Bottom, other.hitBox.Bottom);
        //    int left = Math.Max(hitBox.Left, other.hitBox.Left);
        //    int right = Math.Min(hitBox.Right, other.hitBox.Right);

        //    for (int y = top; y < bottom; y++)
        //    {
        //        for (int x = left; x < right; x++)
        //        {
        //            Color colorA = dataA[(x - hitBox.Left) + (y - hitBox.Top) * hitBox.Width];
        //            Color colorB = dataB[(x - other.hitBox.Left) + (y - other.hitBox.Top) * other.hitBox.Width];

        //            if (colorA.A != 0 && colorB.A != 0)
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}
    }
}

