using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace part_2___List___Loops
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Random generator;
        Rectangle window;
        Texture2D backgroundTexture;
        List<Texture2D> textures;
        List<Rectangle> plannetRects;
        SpriteFont timer;
        SpriteFont counter;

        MouseState mouseState, previousMouseState;


        float seconds;
        float respawnTime;
        float time;
        float points;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            

            window = new Rectangle(0, 0, 800, 600);
            generator = new Random();
            textures = new List<Texture2D>();
            plannetRects = new List<Rectangle>();

            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;

            _graphics.ApplyChanges();

            for (int i = 0; i < 30; i++)
            {

                int size = generator.Next(20, 51);

                plannetRects.Add
                    (
                    new Rectangle(generator.Next(window.Width - 25), generator.Next(window.Height - 25), size, size)

                    );

            }

            seconds = 0f;

            respawnTime = 2f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            backgroundTexture = Content.Load<Texture2D>("Images/space_background");
            timer = Content.Load<SpriteFont>("Timer");
            counter = Content.Load<SpriteFont>("Counter");
            for (int i = 1; i < 13; i++)
            {
               textures.Add(Content.Load<Texture2D>("Images/16-bit-planet" + i));
            }

        }

        protected override void Update(GameTime gameTime)
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds > respawnTime)
            {
                int size = generator.Next(20, 51);

                plannetRects.Add
                    (
                    new Rectangle(generator.Next(window.Width - 25), generator.Next(window.Height - 25), size, size)

                    );
                seconds = 0;

                //points = points + 1;
            }

            //time = 0f + 1f;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < plannetRects.Count; i++)
                {
                    if (plannetRects[i].Contains(mouseState.Position))
                    {
                        plannetRects.RemoveAt(i);
                        textures.RemoveAt(i);
                        i--;
                        points = points + 1;

                    }
                }
            }

            if (plannetRects.Count == 0)
            {
                Exit();
            }

            for (int i = 1; i <= 13; i++)
            {
                //textures.Add(Content.Load<Texture2D>("Images/space_background"));
            }
            for (int i = 0; i < plannetRects.Count;i++)
                textures.Add(textures[generator.Next(textures.Count)]);
            


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundTexture, window, Color.White);
            for (int i = 0;i < plannetRects.Count; i++)
                _spriteBatch.Draw(textures[i], plannetRects[i], Color.White);
            _spriteBatch.DrawString(timer, (time + ""), new Vector2 (600, 10), Color.White);
            _spriteBatch.DrawString(counter, (points + ""), new Vector2(730, 520), Color.Pink);
            

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
