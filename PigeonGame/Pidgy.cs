﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PigeonGame
{
	public class Pidgy : GameObjects
	{
		// FIELDS
		Vector2 _gravity;
		Vector2 _fly;
		KeyboardState _keyboard;
		public Rectangle rectangle;
		//		public Rectangle collisionRect;

		float _elapsed;
		float _delay = 100;
		int _frames;
		//		int _frames2;
		int _rij;

		//		float _cooldowntime = 0;
		float _flying = 0;
		float _regen = 0;
		bool _flyup = true;

		//		public Color[] textureData;

		// PROPERTIES
		public Vector2 GetPosition ()
		{
			return _position;
		}

		public Pidgy (Game1 g, World w, Texture2D texture, Vector2 position, float scale) : base (g, w, texture, position, scale)
		{

			_fly = new Vector2 (0, 1.5f);
			_gravity = new Vector2 (0, 2);
			_scale = 0.2f;


			int size = _texture.Width/12;
			_rij = 2;
			_sourceRectangle = new Rectangle (size *0, size* _rij, size, size);

			//			textureData = new Color[_texture.Width * _texture.Height];
			//			_texture.GetData (textureData); 

		}

		public Rectangle PigeonPosition()
		{
			return new Rectangle ((int)_position.X, (int)_position.Y, _texture.Width/5/12, _texture.Height/5/4);
		}

		public void ResetPosition() {
			_position = new Vector2 (_game.GraphicsDevice.Viewport.Width/8, 500);
		}

		public void BossLevelPosition() {	
			_position = new Vector2 (_game.GraphicsDevice.Viewport.Width/8, 265);
		}

		public void Update (GameTime gameTime)
		{

			if (Assets.LevelComplete) {
				ResetPosition ();
			} else if (_world.StartBossLevel) {
				BossLevelPosition ();
			}

			int size = _texture.Width/12;

			_elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

			if (_elapsed >= _delay) 
			{
				if (_frames >= 11) {
					_frames = 0;
				} else {
					_frames++;
				}
				_elapsed = 0;

			}

			//			if (_elapsed >= _delay) 
			//			{
			//				if (_frames2 >= 3) {
			//					_frames2 = 0;
			//				} else {
			//					_frames2++;
			//				}
			//				_elapsed = 0;
			//				_rij = 3;
			//
			//			}
			//			Console.WriteLine ("ben ik " + _scale);
			_keyboard = Keyboard.GetState ();


			_position += _gravity;


			if (_position.Y > 500)
			{
				_position.Y = 500;
			}

			_regen += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			//			_cooldowntime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			//			if (_cooldowntime >= 3000f) 
			//			{
			//				_flyup = true;
			//			}

			//			if (_flying += (float) gameTime.ElapsedGameTime.TotalMilliseconds)
			//Console.WriteLine (_flying);

			if (_regen >= 300 && _flying >= 0) 
			{
				_flying -= 100;
				_regen = 0;

			}
//			if (_flying >= 2000f) {
//				_flyup = false;
//			} else {
//				_flyup = true;
//			}

			if (_world.StartBossLevel) {
				// BOSS LEVEL INITIATED SO YOU CAN'T MOVE ANYMORE.
				// PIDGY TOO SCARED TO MOVE.
			} else {
				if (_keyboard.IsKeyDown (Keys.Up) && _flyup) {
					_flying += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
					_fly.Y *= 1.05f;
					if (_fly.Y > 6) {
						_fly.Y = 6;
					}
					_position.Y -= _fly.Y;
					_sourceRectangle = new Rectangle (size * _frames, size * _rij, size, size);


					if (_flying <= 0) {
						_flying = 0;
					} 
				}else {
					_fly = new Vector2 (0, 1.5f);
				}


				//			rectangle = new Rectangle (size * _frames, size * _rij, _texture.Width/12, _texture.Height/4);
				//			rectangle = new Rectangle (size * _frames, size * _rij, size, size);
				//			collisionRect = new Rectangle (rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
				//			collisionRect.X += (int)_position.X;
				//			collisionRect.Y += (int)_position.Y;


				//			rectangle = new Rectangle (size * _frames, size * _rij, _texture.Width/12/5, _texture.Height/4/5);




				if (_keyboard.IsKeyDown (Keys.Right)) {
					_sourceRectangle = new Rectangle (size * _frames, size * _rij, size, size);

					//_position += new Vector2 (3, 0);
					_position += new Vector2 (3, 0);

					if (_position.Y > 499) {
						_rij = 2;

					} else {
						_rij = 0;
						//					_sourceRectangle = new Rectangle (size * _frames, size* _rij, size, size);
					}

				}

				if (_keyboard.IsKeyDown (Keys.Left)) {
					_position -= new Vector2 (3, 0);
					_sourceRectangle = new Rectangle (size * _frames, size * _rij, size, size);


					if (_position.Y > 499) {
						_rij = 3;
						//					_sourceRectangle = new Rectangle (size * _frames, size * _rij, size, size);

					} else {
						_rij = 1;
						//					_sourceRectangle = new Rectangle (size * _frames, size* _rij, size, size);
					}


				}



				if (_position.Y < 0) {
					_fly = new Vector2 (0, 0);
				}

				if (_position.Y < 500 && _rij == 2) {
					_rij = 0;
				} else if (_position.Y < 500 && _rij == 3) {
					_rij = 1;
				}

				//			if (_position.Y == 500 && _rij == 0) {
				//				_rij = 2;
				//			} else if (_position.Y == 500 && _rij == 0){
				//				_rij = 3;
				//			}

				if (_position.X > _game.GraphicsDevice.Viewport.Width / 2 + 1) {
					_position.X = _game.GraphicsDevice.Viewport.Width / 2 + 1;
				}

				if (_position.X < _game.GraphicsDevice.Viewport.Width / 8) {
					_position.X = _game.GraphicsDevice.Viewport.Width / 8;
				}
			}


			//			_position = new Vector2 (0,0);
		}


	}
}