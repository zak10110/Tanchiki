using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Tank_Server_Client_lib;


namespace Client_Tank
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D tank_texture;
        Texture2D map_texture;
        Tank tank = new Tank(300, 3, 30);
        Vector2 position = Vector2.Zero;
        Client client = new Client("127.0.0.1", 8000);
        List<Tank> tanks = new List<Tank>();
        Map mapp = new Map(12,12);
        List<Cell> cells = new List<Cell>();
        //List<Texture2D> texture2Ds = new List<Texture2D>();

        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 560;
            _graphics.PreferredBackBufferHeight = 560;
            _graphics.ApplyChanges();
            var rand = new Random();
            // TODO: Add your initialization logic here
            mapp.CreateManualMAp();
            client.CreateIPEndPoint();
            client.Conect();
            client.ID = int.Parse(client.TakeMSGFromServ());
            client.SengMsg($"Conection Success Client ID-{client.ID}");
           

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tank_texture = Content.Load<Texture2D>(@"Texture\Tank");
            map_texture = Content.Load<Texture2D>(@"Texture\wall");
        
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            string json = JsonSerializer.Serialize<Tank>(tank);
            try
            {
                tanks = JsonSerializer.Deserialize<List<Tank>>(client.TakeMSGFromServ());
            }
            catch (Exception)
            {

            }
         

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                tank.X -= tank.Speed;
                tank.Rotation = 23.55f;
                tank.rectangle = new System.Drawing.Rectangle(tank.X,tank.Y,40,49);
                client.SengMsg(json);
                foreach (var item in cells)
                {
                    if (tank.rectangle.IntersectsWith(item.rectangle))
                    {

                        tank.X += tank.Speed;


                    }


                }

            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                tank.X += tank.Speed;
                tank.Rotation = 7.85f;
                client.SengMsg(json);
                tank.rectangle = new System.Drawing.Rectangle(tank.X, tank.Y, 40, 49);
                foreach (var item in cells)
                {
                    if (tank.rectangle.IntersectsWith(item.rectangle))
                    {

                        tank.X -= tank.Speed;


                    }


                }
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                tank.Y -= tank.Speed;
                tank.Rotation = 0f;
                client.SengMsg(json);
                tank.rectangle = new System.Drawing.Rectangle(tank.X, tank.Y, 40, 49);
                foreach (var item in cells)
                {
                    if (tank.rectangle.IntersectsWith(item.rectangle))
                    {

                        tank.Y += tank.Speed;


                    }


                }
            }
            else if(keyboardState.IsKeyDown(Keys.Down))
            {

                tank.Y += tank.Speed;
                tank.Rotation = 15.7f;
                client.SengMsg(json);
                tank.rectangle = new System.Drawing.Rectangle(tank.X, tank.Y, 40, 49);
                foreach (var item in cells)
                {
                    if (tank.rectangle.IntersectsWith(item.rectangle))
                    {

                        tank.Y -= tank.Speed;


                    }


                }

            }
            // TODO: Add your update logic here

          



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            int rows = mapp.map.GetUpperBound(0) + 1;    // количество строк
            int columns =mapp.map.Length / rows;        // количество столбцов
            int x = 0;
            int y = 0;
            for (int i = 0; i < mapp.wight; i++)
            {
                for (int j = 0; j < mapp.height; j++)
                {
                    if (mapp.map[i,j]=='X')
                    {
                        cells.Add(new Cell(50,50,y,x));
                        _spriteBatch.Draw(map_texture, new Rectangle(y, x, 50, 50), null, Color.White, 0, new Vector2(40 / 2f, 49 / 2f), SpriteEffects.None, 0f);
                        x = i*50;
                        y = j*50;
                    }
                    

                }

            }
            _spriteBatch.Draw(map_texture, new Rectangle(550, 550, 50, 50), null, Color.White, 0, new Vector2(40 / 2f, 49 / 2f), SpriteEffects.None, 0f);
            //_spriteBatch.Draw(tank_texture, new Rectangle(tank.X, tank.Y, 40, 49), null, Color.White, tank.Rotation, new Vector2(40 / 2f, 49 / 2f), SpriteEffects.None, 0f);

            foreach (var item in tanks)
            {
                _spriteBatch.Draw(tank_texture, new Rectangle(item.X, item.Y, 40, 49), null, Color.White, item.Rotation, new Vector2(40 / 2f, 49 / 2f), SpriteEffects.None, 0f);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
