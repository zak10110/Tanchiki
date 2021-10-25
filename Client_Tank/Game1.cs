using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tank_Server_Client_lib;

namespace Client_Tank
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D tank_texture;
        Tank tank = new Tank(300, 5, 30);
        Vector2 position = Vector2.Zero;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Client client = new Client("127.0.0.1", 8000);
            client.CreateIPEndPoint();
            client.Conect();
            client.SengMsg("Conection");


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tank_texture = Content.Load<Texture2D>(@"Texture\Tank");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                tank.X -= tank.Speed;
                tank.Rotation = 23.55f;



            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                tank.X += tank.Speed;
                tank.Rotation = 7.85f;

            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                tank.Y -= tank.Speed;
                tank.Rotation = 0f;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {

                tank.Y += tank.Speed;
                tank.Rotation = 15.7f;

            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();


            _spriteBatch.Draw(tank_texture, new Rectangle(tank.X, tank.Y, 40, 49), null, Color.White, tank.Rotation, new Vector2(40 / 2f, 49 / 2f), SpriteEffects.None, 0f);



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
