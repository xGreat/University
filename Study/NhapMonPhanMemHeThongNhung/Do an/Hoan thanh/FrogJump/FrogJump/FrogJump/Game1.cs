﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.IO.IsolatedStorage;
using System.IO;


namespace FrogJump
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState
        {
            MainMenu,
            Information,
            Playing,
        }
        GameState currentGameState = GameState.MainMenu;

        //Object
        Frog frog;
        List<Leaf> leafs;
        List<Bound> bounds;

        //Play Leaf
        Texture2D playleafTexture;
        Vector2 playleafPosition;
        Vector2 playleafOrigin;
        float playleafRotation;

        //Background
        Texture2D backgroundTexture;

        //Touch
        Rectangle touchRectangle;
        Rectangle startRectangle;
        Rectangle informationRectangle;
        Rectangle touchjumpRectangle;
        Rectangle pauseRectangle;
        Rectangle musicRectangle;
        Rectangle zeroRectangle;

        //Icon
        Texture2D informationTexture;
        Texture2D pauseTexture;
        Texture2D musiconTexture;
        Texture2D musicoffTexture;
        Texture2D lifeTexture;
        
        //Screen
        Texture2D titleTexture;
        Texture2D informationscreenTexture;
        Texture2D pausescreenTexture;
        Texture2D gameoverscreenTexture;

        //Sounds
        SoundEffect jumpSound, tapSound, deadSound;
        Song backgroundSong;

        //Other
        Camera camera;
        SpriteFont font;

        int randX, indexRotation, leafDistance, score, leafCount, life;
        int[] indexLife;
        float[] leafRotation;
        bool frogDead, pause, gameOver, music;
        string scoreText, text;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            TargetElapsedTime = TimeSpan.FromTicks(333333);
            InactiveSleepTime = TimeSpan.FromSeconds(1);
            
            graphics.PreferredBackBufferHeight = Statics.HEIGHT_SCREEN;
            graphics.PreferredBackBufferWidth = Statics.WIDTH_SCREEN;
            Content.RootDirectory = "Content";
        }

        private void saveText(string filename, string text)
        {
            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream rawStream = isf.CreateFile(filename))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(text);
                    writer.Close();
                }
            }
        }

        private bool loadText(string filename, out string result)
        {
            result = "";
            using (IsolatedStorageFile isf =
                IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(filename))
                {
                    try
                    {
                        using (IsolatedStorageFileStream rawStream =
                            isf.OpenFile(filename, System.IO.FileMode.Open))
                        {
                            StreamReader reader = new StreamReader(rawStream);
                            result = reader.ReadToEnd();
                            reader.Close();
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void Reset()
        {
            score = 0;
            leafCount = 1;
            life = Statics.LIFE;
            indexRotation = Statics.RANDOM.Next(0, 7);
        }

        protected override void Initialize()
        {
            leafRotation = new float[22];
            indexLife = new int[Statics.LIFE];
            frogDead = false;
            pause = false;
            gameOver = false;
            music = true;
            life = Statics.LIFE;
            score = 0;
            leafCount = 0;
            indexLife[0] = 0;
            leafDistance = Statics.HEIGHT_SCREEN - 40;

            for (int i = 0; i < life - 1; i++)
                indexLife[i + 1] = indexLife[i] + 32;
            //Thiết lập tốc độ quay của lá
            for (int i = 0; i < 22; i++)
            {
                if (i < 4)
                    leafRotation[i] = (float)(0.005 * (i + 14));
                if (i >= 4 && i < 8)
                    leafRotation[i] = (float)(-0.005 * (i + 10));

                if (i >= 8 && i < 12)
                    leafRotation[i] = (float)(0.005 * (i + 10));
                if (i >= 12 && i < 16)
                    leafRotation[i] = (float)(-0.005 * (i + 6));

                if (i >= 16 && i < 19)
                    leafRotation[i] = (float)(0.005 * (i + 6));
                if (i >= 19 && i < 22)
                    leafRotation[i] = (float)(-0.005 * (i + 3));
            }

            frog = new Frog();
            leafs = new List<Leaf>();
            bounds = new List<Bound>();
            camera = new Camera(GraphicsDevice.Viewport);
            base.Initialize();
        }

        public void LoadLeaf()
        {
            randX = Statics.RANDOM.Next(80, 400);
            leafDistance = leafDistance - Statics.RANDOM.Next(200, 320);
            leafs.Add(new Leaf(Content.Load<Texture2D>("Object/Leaf"), new Vector2(randX, leafDistance)));
            bounds.Add(new Bound(new Vector2(randX, leafDistance)));
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Other/SpriteFont1");
            Statics.PIXEL = Content.Load<Texture2D>("Other/pixel");

            //Icon
            playleafTexture = Content.Load<Texture2D>("Icon/PlayLeaf");
            informationTexture = Content.Load<Texture2D>("Icon/Information");
            pauseTexture = Content.Load<Texture2D>("Icon/Pause");
            musiconTexture = Content.Load<Texture2D>("Icon/MusicOn");
            musicoffTexture = Content.Load<Texture2D>("Icon/MusicOff");
            lifeTexture = Content.Load<Texture2D>("Icon/Life");

            //Background
            titleTexture = Content.Load<Texture2D>("Screen/Title");
            informationscreenTexture = Content.Load<Texture2D>("Screen/InformationScreen");
            backgroundTexture = Content.Load<Texture2D>("Screen/Background");
            pausescreenTexture = Content.Load<Texture2D>("Screen/PauseScreen");
            gameoverscreenTexture = Content.Load<Texture2D>("Screen/GameOverScreen");
            
            //Sounds
            jumpSound = Content.Load<SoundEffect>("Sound/JumpSound");
            tapSound = Content.Load<SoundEffect>("Sound/TapSound");
            deadSound = Content.Load<SoundEffect>("Sound/DeadSound");
            backgroundSong = Content.Load<Song>("Sound/BackgroundSong");
            MediaPlayer.Play(backgroundSong);
            MediaPlayer.IsRepeating = true;

            startRectangle = new Rectangle(180, 340, playleafTexture.Width - 80, playleafTexture.Height - 80);
            informationRectangle = new Rectangle(10, 750, informationTexture.Width, informationTexture.Height);
            touchjumpRectangle = new Rectangle(0, 100, Statics.WIDTH_SCREEN, Statics.HEIGHT_SCREEN);
            pauseRectangle = new Rectangle(10, 45, pauseTexture.Width, pauseTexture.Height);
            musicRectangle = new Rectangle(60, 45, musiconTexture.Width, musiconTexture.Height);
            zeroRectangle = new Rectangle(0, 0, 0, 0);
            
            leafs.Add(new Leaf(Content.Load<Texture2D>("Object/Leaf"), new Vector2(frog.position.X, leafDistance)));
            bounds.Add(new Bound(new Vector2(frog.position.X, leafDistance)));
            for (int i = 1; i < Statics.LEAFS_COUNT; i++)
                LoadLeaf();
            frog.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    {
                        //Play Leaf
                        playleafPosition = new Vector2(140 + playleafTexture.Width / 2, 300 + playleafTexture.Height / 2);
                        playleafOrigin = new Vector2(playleafTexture.Width / 2, playleafTexture.Height / 2);
                        playleafRotation += 0.05f;

                        foreach (TouchLocation touchLocation in touchCollection)
                        {
                            if (touchLocation.State == TouchLocationState.Pressed)
                                touchRectangle = new Rectangle((int)(touchLocation.Position.X - 10), (int)(touchLocation.Position.Y - 10), 20, 20);
                        }
                        //Thoát khi nhấn phím Back
                        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                            this.Exit();
                        //Đi đến màn hình Information khi chạm vào icon Information
                        if (touchRectangle.Intersects(informationRectangle))
                        {
                            currentGameState = GameState.Information;
                            touchRectangle = zeroRectangle;
                        }
                        //Đi đến màn hình chơi chính khi chạm vào Play
                        if (touchRectangle.Intersects(startRectangle))
                        {
                            currentGameState = GameState.Playing;
                            touchRectangle = zeroRectangle;
                        }
                    }
                    break;
                case GameState.Information:
                    //Trở về màn hình MainMenu khi nhấn phím Back
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        currentGameState = GameState.MainMenu;
                    break;
                case GameState.Playing:
                    {
                        foreach (TouchLocation touchLocation in touchCollection)
                        {
                            if (touchLocation.State == TouchLocationState.Pressed)
                                touchRectangle = new Rectangle((int)(touchLocation.Position.X - 10), (int)(touchLocation.Position.Y + 25), 20, 20);
                        }
                        //Trở về màn hình MainMenu khi nhấn phím Back
                        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        {
                            currentGameState = GameState.MainMenu;
                            if (gameOver)
                                gameOver = false;
                            if (pause)
                                pause = false;
                            Reset();
                        }
                        if (!pause)
                        {
                            if (touchRectangle.Intersects(touchjumpRectangle))
                            {
                                frog.jumped = true;
                                touchRectangle = zeroRectangle;
                            }
                            if (touchRectangle.Intersects(pauseRectangle))
                            {
                                pause = true;
                                if (music)
                                    MediaPlayer.Pause();
                                touchRectangle = zeroRectangle;
                            }
                            if (touchRectangle.Intersects(musicRectangle) && music)
                            {
                                music = false;
                                MediaPlayer.Stop();
                                touchRectangle = zeroRectangle;
                            }
                            if (touchRectangle.Intersects(musicRectangle) && !music)
                            {
                                MediaPlayer.Play(backgroundSong);
                                music = true;
                                touchRectangle = zeroRectangle;
                            }

                            if (leafs.Count() < Statics.LEAFS_COUNT)
                                LoadLeaf();

                            frog.Update(gameTime);
                            foreach (Bound bound in bounds)
                                bound.Update(gameTime);
                            camera.Update(gameTime, frog);

                            if (frog.jumped)
                                frog.rotation += 0;
                            else
                                frog.rotation = leafs[0].rotation;

                            leafs[0].rotation += leafRotation[indexRotation];

                            for (int i = 0; i < leafs.Count(); i++)
                            {
                                if (frog.Bound.Intersects(bounds[i].rectangle))
                                {
                                    frog.touch = false;
                                    frog.jumped = false;
                                    bounds[i].isBound = false;
                                    leafCount += 1;

                                    if (leafCount > 12)
                                        indexRotation = Statics.RANDOM.Next(16, 21);
                                    else
                                        if (leafCount > 6 && leafCount <= 12)
                                            indexRotation = Statics.RANDOM.Next(8, 15);
                                        else
                                            indexRotation = Statics.RANDOM.Next(0, 7);
                                    //Lấy vị trí của Frog
                                    frog.position = bounds[i].position;

                                    if (i > 0)
                                    {
                                        score += 10;
                                        leafs.RemoveAt(0);
                                        bounds.RemoveAt(0);
                                        if(music)
                                            tapSound.Play();
                                    }
                                }
                                //Dead
                                if (frog.position.X < 0 || frog.position.X > Statics.WIDTH_SCREEN || frog.position.Y > leafs[0].position.Y + 100 || frog.position.Y < leafs[1].position.Y - 75)
                                {
                                    frog.touch = false;
                                    frog.jumped = false;
                                    frogDead = true;
                                    frog.position = bounds[i].position;
                                }
                                //Lấy tốc độ quay của Frog
                                if ((frog.position.Y < bounds[i].position.Y) && frog.jumped == false)
                                    frog.rotation = leafs[0].rotation;
                            }
                            if (frogDead)
                            {
                                if (music)
                                    deadSound.Play();
                                frogDead = false;
                                life -= 1;
                            }
                            //Game Over
                            if (life == 0)
                            {
                                gameOver = true;
                                pause = true;
                            }
                            //Lưu điểm cao nhất
                            loadText("bestscore.txt", out text);
                            scoreText = score.ToString();
                            if (scoreText.Length > text.Length)
                                saveText("bestscore.txt", scoreText);
                            else
                                if(scoreText.Length == text.Length)
                                {
                                    if (scoreText.CompareTo(text) == 1)
                                        saveText("bestscore.txt", scoreText);
                                }

                            if (touchRectangle == zeroRectangle && frog.jumped && !pause)
                            {
                                if(music)
                                    jumpSound.Play();
                                touchRectangle = new Rectangle(-10, 0, 0, 0);
                            }
                        }
                        else
                            if (pause)
                            {
                                if (touchRectangle.Intersects(touchjumpRectangle))
                                {
                                    pause = false;
                                    touchRectangle = zeroRectangle;
                                    if (music)
                                        MediaPlayer.Resume();
                                    if (gameOver)
                                    {
                                        Reset();
                                        gameOver = false;
                                    }
                                }
                            }
                    }
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height), Color.White);
                        spriteBatch.Draw(titleTexture, new Rectangle(40, 40, titleTexture.Width, titleTexture.Height), Color.White);
                        
                        spriteBatch.Draw(playleafTexture, playleafPosition, null, Color.White, playleafRotation, playleafOrigin, 1f, SpriteEffects.None, 0f);
                        spriteBatch.DrawString(font, Convert.ToString("Play"), new Vector2(startRectangle.X + 10, startRectangle.Y + 30), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);

                        spriteBatch.Draw(informationTexture, informationRectangle, Color.White);    
                    }
                    break;
                case GameState.Information:
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(informationscreenTexture, new Rectangle(0, 0, Statics.WIDTH_SCREEN, Statics.HEIGHT_SCREEN), Color.White);
                    }
                    break;
                case GameState.Playing:
                    {
                        GraphicsDevice.Clear(Color.DarkBlue);
                        spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend,
                        null, null, null, null,
                        camera.transform);

                        spriteBatch.Draw(backgroundTexture, new Rectangle(0,(int) (camera.centre.Y - Statics.HEIGHT_SCREEN + 40), backgroundTexture.Width, backgroundTexture.Height), Color.White);

                        foreach (Leaf leaf in leafs)
                            leaf.Draw(gameTime, spriteBatch);

                        if(!gameOver)
                            frog.Draw(gameTime, spriteBatch);

                        spriteBatch.Draw(pauseTexture, new Rectangle(10, (int)(camera.centre.Y - Statics.HEIGHT_SCREEN + 50), pauseTexture.Width, pauseTexture.Height), Color.White);
                        if (music)
                            spriteBatch.Draw(musiconTexture, new Rectangle(60, (int)(camera.centre.Y - Statics.HEIGHT_SCREEN + 50), musiconTexture.Width, musiconTexture.Height), Color.White);
                        if (!music)
                            spriteBatch.Draw(musicoffTexture, new Rectangle(60, (int)(camera.centre.Y - Statics.HEIGHT_SCREEN + 50), musiconTexture.Width, musiconTexture.Height), Color.White);
                        for (int i = 0; i < life; i++)
                            spriteBatch.Draw(lifeTexture, new Rectangle(Statics.WIDTH_SCREEN - 35, (int)(camera.centre.Y - Statics.HEIGHT_SCREEN + 50) + indexLife[i], lifeTexture.Width, lifeTexture.Height), Color.White);

                        spriteBatch.DrawString(font, Convert.ToString(score), new Vector2(Statics.WIDTH_SCREEN / 2 - 20, camera.centre.Y - Statics.HEIGHT_SCREEN + 50), Color.Red);

                        if (pause && !gameOver)
                        {
                            spriteBatch.Draw(Statics.PIXEL, new Rectangle(0, (int)(camera.centre.Y - Statics.HEIGHT_SCREEN), Statics.WIDTH_SCREEN, Statics.HEIGHT_SCREEN + 50), new Color(0f, 0f, 0f, 0.3f));
                            spriteBatch.Draw(pausescreenTexture, new Rectangle(90, (int)(camera.centre.Y - Statics.HEIGHT_SCREEN + 255), pausescreenTexture.Width, pausescreenTexture.Height), Color.White);
                        }

                        if (gameOver)
                        {
                            spriteBatch.Draw(Statics.PIXEL, new Rectangle(0, (int)(camera.centre.Y - Statics.HEIGHT_SCREEN), Statics.WIDTH_SCREEN, Statics.HEIGHT_SCREEN + 50), new Color(0f, 0f, 0f, 0.3f));
                            spriteBatch.Draw(gameoverscreenTexture, new Rectangle(80, (int)(camera.centre.Y - Statics.HEIGHT_SCREEN + 295), gameoverscreenTexture.Width, gameoverscreenTexture.Height), Color.White);
                            spriteBatch.DrawString(font, Convert.ToString("Score: " + score), new Vector2(120, (int)(camera.centre.Y - Statics.HEIGHT_SCREEN + 380)), Color.White);
                            spriteBatch.DrawString(font, Convert.ToString("Best: " + text), new Vector2(120, (int)(camera.centre.Y - Statics.HEIGHT_SCREEN + 440)), Color.White);
                        }
                    }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}