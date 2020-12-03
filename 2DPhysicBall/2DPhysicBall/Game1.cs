﻿using Microsoft.Xna.Framework;
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

        Ball ball1, ball2;

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

            radius = 50.0f;
            ballTex = CreateCircleTexture((int)radius, Color.White);
            boundary = new Point(bx - ballTex.Width, by - ballTex.Height);

            pos = new Vector2(50, 50);
            vel = new Vector2(2, 0);
            ball1 = new Ball(ballTex, pos, vel, radius, boundary);


            radius = 50.0f;
            ballTex = CreateCircleTexture((int)radius, Color.Red);
            pos = new Vector2(500, 50);
            vel = new Vector2(-2, 0);
            ball2 = new Ball(ballTex, pos, vel, radius, boundary);

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

            ball1.Update(gameTime);
            ball2.Update(gameTime);

            if (ball1.CircleCollision(ball2))
            {
                ball1.GetVel *= -1;
                ball2.GetVel *= -1;
                Console.WriteLine("Ball 1 pos:" + ball1.GetPos + " | Ball 2 pos: " + ball2.GetPos);
            }





            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


            ball1.Draw(spriteBatch);
            ball2.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
