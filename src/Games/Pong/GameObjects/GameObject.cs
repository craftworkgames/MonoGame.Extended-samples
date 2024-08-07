﻿using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;

namespace Pong.GameObjects
{
    public class GameObject
    {
        public Vector2 Position;
        public Vector2 Scale = Vector2.One;
        public float Rotation;
        public Sprite Sprite;
        public RectangleF BoundingRectangle => Sprite.GetBoundingRectangle(Position, 0, Vector2.One);
    }
}
