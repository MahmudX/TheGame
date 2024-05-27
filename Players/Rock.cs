using SFML.Graphics;
using SFML.System;
using TheFight;


namespace Players;
public class Rock : GameObject
{
    public override void Update(double deltaTime, Vector2u windowSize)
    {
        if (shape is CircleShape circleShape)
        {
            X += Velocity.X * (float)deltaTime;
            Y += Velocity.Y * (float)deltaTime;

            // Check boundaries and reverse direction if necessary
            if (X - circleShape.Radius < 0 || X + circleShape.Radius > windowSize.X)
            {
                // Velocity.X = -Velocity.X;
                Velocity = new Vector2f(-Velocity.X, Velocity.Y);
                X = Math.Clamp(X, circleShape.Radius, windowSize.X - circleShape.Radius);
            }
            if (Y - circleShape.Radius < 0 || Y + circleShape.Radius > windowSize.Y)
            {
                // Velocity.Y = -Velocity.Y;
                Velocity = new Vector2f(Velocity.X, -Velocity.Y);
                Y = Math.Clamp(Y, circleShape.Radius, windowSize.Y - circleShape.Radius);
            }

            shape.Position = new Vector2f(X, Y);
        }
    }
}