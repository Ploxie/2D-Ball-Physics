using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _2DPhysicBall
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Ball whiteBall, redBall;

        Texture2D ballTex;
        Vector2 pos, vel;
        float radius;
        Point boundary;
        List<Ball> ballList;

        int bx, by;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            bx = graphics.PreferredBackBufferWidth = 800;
            by = graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballList = new List<Ball>();

            radius = 100.0f;
            ballTex = CreateCircleTexture((int)radius, Color.White);
            boundary = new Point(bx - ballTex.Width, by - ballTex.Height);

            pos = new Vector2(50, 50);
            vel = new Vector2(2, 3);
            whiteBall = new Ball(ballTex, pos, vel, radius, boundary);


            radius = 50.0f;
            ballTex = CreateCircleTexture((int)radius, Color.Red);
            pos = new Vector2(500, 50);
            vel = new Vector2(-5, 2);
            boundary = new Point(bx - ballTex.Width, by - ballTex.Height);
            redBall = new Ball(ballTex, pos, vel, radius, boundary);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public Texture2D CreateCircleTexture(int radius, Color color)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diameter = radius / 2f;
            float diameterSquared = diameter * diameter;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diameter, y - diameter);
                    if (pos.LengthSquared() <= diameterSquared)
                    {
                        colorData[index] = color;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            whiteBall.Update(gameTime);
            redBall.Update(gameTime);

            // Check collision between balls
            if (Vector2.Distance(whiteBall.GetPos, redBall.GetPos) < (whiteBall.GetRadius + redBall.GetRadius))
            {
                /*
                Consider two balls B1 and B2 having initial velocities U1 and U2. After the two balls collide, their
                normal velocities get exchanged.The normal N at the point of contact is given by the equation
                N = (C1C2)Nu
                where Nu is the unit normal vector at the point of contact and C1 and C2 are the centers of the two
                balls at the time of collision.
                The normal components U1n and U2n of balls B1 and B2 are :
                U1n = dot(U1.N)N
                U2n = dot(U2.N)N
                The new velocities V1 and V2 of the balls B1 and B2 after their collision are:
                                V1 = U1 – U1n + U2n
                V2 = U2 – U2n + U1n
                */

                Vector2 delta = whiteBall.GetPos - redBall.GetPos; //skillnad mellan ballarnas centers.

                Vector2 normal = delta;
                normal.Normalize(); //normaliserar delta

                // Ju mer lika bollarnas riktning är, ju närmre 1 blir deras dot-produkt
                // Medans ju mindre lika bollarnas riktning är, ju närmre -1 blir deras dot-produkt
                // Så ju närmre man kommer -1, ju mer blir deras riktning inverterad                

                Vector2 velDiff1 = Vector2.Dot(whiteBall.GetVel, normal) * normal;
                Vector2 velDiff2 = Vector2.Dot(redBall.GetVel, normal) * normal;

                whiteBall.GetVel += -velDiff1 + velDiff2;
                redBall.GetVel += -velDiff2 + velDiff1;

                //Console.WriteLine("Normal 1: " + norm1);
                //Console.WriteLine("Ball 1 vel " + ball1.GetVel);
                //Console.WriteLine("Ball 1 pos:" + ball1.GetPos + " | Ball 2 pos: " + ball2.GetPos);
                Console.WriteLine("Collision: p1"+whiteBall.GetPos+" | p2"+redBall.GetPos + " Time: "+gameTime.TotalGameTime.TotalSeconds);
            }
            
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


            whiteBall.Draw(spriteBatch);
            redBall.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
