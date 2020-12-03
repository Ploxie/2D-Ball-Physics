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
        Vector2 pos,vel;
        float r;
        
        public Ball(Texture2D ballTex,Vector2 pos, Vector2 vel, float r)
        {
            this.ballTex = ballTex;
            this.pos = pos;
            this.vel = vel;
            this.r = r;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw();
        }


    }
}
