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
        Texture2D ballTex;
        Vector2 pos, vel;
        float radius;
        int bX;
        int bY;



        public Ball(Texture2D ballTex, Vector2 pos, Vector2 vel, float radius, Point boundary)
        {
            this.ballTex = ballTex;
            this.pos = pos;
            this.vel = vel;
            this.radius = radius;
            this.bX = boundary.X;
            this.bY = boundary.Y;

        }

        public bool CircleCollision(Ball other)
        {
            return Vector2.Distance(pos, other.pos) < (radius + other.radius);
        }

        public void Update(GameTime gameTime)
        {
            pos.Y += vel.Y;

            if (pos.X <= 0 && vel.X < 0 || pos.X >= bX && vel.X > 0)
            {
                vel.Y *= -1;
            }

            if (pos.Y <= 0 && vel.Y < 0 || pos.Y >= bY && vel.Y > 0)
            {
                vel.Y *= -1;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(ballTex, pos, Color.White);
        }


    }
}
