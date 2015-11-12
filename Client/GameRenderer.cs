using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaGifAnimation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;

namespace Client
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class GameRenderer : Game
	{
		GraphicsDeviceManager graphics;
		PresentationParameters drawParams = new PresentationParameters();
		SpriteBatch sb;
		SpriteFont AAFont;
		ClientForm parent;
		GifAnimation background;
		GifAnimation character;
		GifAnimation desk;
		GifAnimation chatBG;
		GifAnimation testimony;
		GifAnimation objection;

		public GameRenderer(ClientForm parentForm)
		{
			graphics = new GraphicsDeviceManager(this);
			//renderTarget = new RenderTarget2D(new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, drawParams), 256, 192);
			graphics.PreferredBackBufferWidth = 256;
			graphics.PreferredBackBufferHeight = 192;
			parent = parentForm;

			Content.RootDirectory = "";

		}

		private Texture2D[] GetTextures(Image image)
		{
			try {
				if (graphics.GraphicsDevice == null || image == null || GetFrames(image) == null || GetFrames(image).Length == 0) //We have to do this otherwise it crashes on the second check
					return null;

				Image[] frames = GetFrames(image);
				Texture2D[] result = new Texture2D[frames.Length];
				for (int x = 0; x < frames.Length; x++) {
					using (MemoryStream ms = new MemoryStream()) {
						frames[x].Save(ms, ImageFormat.Png);
						result[x] = Texture2D.FromStream(graphics.GraphicsDevice, ms);
					}
				}

				return result;
			} catch (Exception ex) {
				if (Program.debug)
					MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "GameRenderer", MessageBoxButtons.OK,
						MessageBoxIcon.Error);
			}
			return null;
		}

		public static Image[] GetFrames(Image image)
		{
			try {
				if (image == null)
					return null;

				var dimension = new FrameDimension(image.FrameDimensionsList[0]); //gets the GUID

				var frameCount = image.GetFrameCount(dimension); //total frames in the animation
				Image[] frames = new Image[frameCount];
				for (var index = 0; index < frameCount; index++) {
					image.SelectActiveFrame(dimension, index); //find the frame
					frames[index] = (Image)image.Clone(); //return a copy of it
				}
				return frames;
			} catch (Exception ex) {
				if (Program.debug)
					MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "GameRenderer", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return null;
		}

		public void ChangeSprites()
		{
			background = GetTextures(parent?.backgroundLayerImage) != null ? GifAnimation.FromTextures(GetTextures(parent?.backgroundLayerImage)) : null;
			character = GetTextures(parent?.charLayerImage) != null ? GifAnimation.FromTextures(GetTextures(parent?.charLayerImage)) : null;
			desk = GetTextures(parent?.deskLayerImage) != null ? GifAnimation.FromTextures(GetTextures(parent?.deskLayerImage)) : null;
			chatBG = GetTextures(parent?.backgroundLayerImage) != null ? GifAnimation.FromTextures(GetTextures(parent?.backgroundLayerImage)) : null;
			testimony = GetTextures(parent?.testimonyImage) != null ? GifAnimation.FromTextures(GetTextures(parent?.testimonyImage)) : null;
			objection = GetTextures(parent?.objectLayerImage) != null ? GifAnimation.FromTextures(GetTextures(parent?.objectLayerImage)) : null;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			sb = new SpriteBatch(graphics.GraphicsDevice);

			try
			{
				AAFont = Content.Load<SpriteFont>("base/misc/aafont");
			}
			catch (Exception ex)
			{
				if (Program.debug)
					MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "GameRenderer", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			background?.Update(gameTime.ElapsedGameTime.Ticks);
			character?.Update(gameTime.ElapsedGameTime.Ticks);
			desk?.Update(gameTime.ElapsedGameTime.Ticks);
			chatBG?.Update(gameTime.ElapsedGameTime.Ticks);
			testimony?.Update(gameTime.ElapsedGameTime.Ticks);
			objection?.Update(gameTime.ElapsedGameTime.Ticks);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			try {
				graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

				sb.Begin();

				if (background != null)
				{
					sb.Draw(background.GetTexture(), background.GetTexture().Bounds, Color.White);
				}
				if (character != null)
				{
					sb.Draw(character.GetTexture(), character.GetTexture().Bounds, Color.White);
				}
				if (desk != null)
				{
					sb.Draw(desk.GetTexture(), desk.GetTexture().Bounds, Color.White);
				}
				if (chatBG != null)
				{
					sb.Draw(chatBG.GetTexture(), chatBG.GetTexture().Bounds, Color.White);
				}
				if (testimony != null)
				{
					sb.Draw(testimony.GetTexture(), testimony.GetTexture().Bounds, Color.White);
				}
				if (objection != null)
				{
					sb.Draw(objection.GetTexture(), objection.GetTexture().Bounds, Color.White);
				}
				sb.DrawString(AAFont, parent.displayMsg1.Text, new Vector2(5, 134), XNAColor(parent.dispColor));
				sb.DrawString(AAFont, parent.displayMsg2.Text, new Vector2(5, 152), XNAColor(parent.dispColor));
				sb.DrawString(AAFont, parent.displayMsg3.Text, new Vector2(5, 170), XNAColor(parent.dispColor));

				sb.End();

				base.Draw(gameTime);
			} catch (Exception ex) {
				if (Program.debug)
					MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "GameRenderer", MessageBoxButtons.OK, MessageBoxIcon.Error);
			} finally {
				sb.End();
			}
		}

		public Color XNAColor(System.Drawing.Color color)
		{
			return new Color(color.R, color.G, color.B, color.A);
		}
	}
}
