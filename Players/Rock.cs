using SFML.Graphics;
using SFML.System;
using TheFight;

namespace Players;

public class Rock : GameObject
{
    public Rock(float x, float y)
        : base(x, y)
    {
        shape = new CircleShape()
        {
            FillColor = Color.Red,
            Position = new Vector2f(X, Y),
            Radius = 5
        };
    }

    public override bool CheckCollision(GameObject other)
    {
        throw new NotImplementedException();
    }

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

    protected override bool CollidesWith(GameObject other)
    {
        float dx = X - other.X;
        float dy = Y - other.Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);
        return distance < (shape as CircleShape).Radius * 2;
    }
}
