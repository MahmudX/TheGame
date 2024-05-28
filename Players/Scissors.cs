using Players;
using SFML.Graphics;
using SFML.System;
using TheFight;

public class Scissors : GameObject
{
    public Scissors(float x, float y)
        : base(x, y)
    {
        shape = new RectangleShape()
        {
            FillColor = Color.Blue,
            Position = new Vector2f(X, Y),
            Size = new Vector2f(10, 10)
        };
    }

    public override bool CheckCollision(GameObject other)
    {
        return other is Rock && CollidesWith(other);
    }

    public override void Update(double deltaTime, Vector2u windowSize)
    {
        if (shape is RectangleShape rectangleShape)
        {
            X += Velocity.X * (float)deltaTime;
            Y += Velocity.Y * (float)deltaTime;

            float width = rectangleShape.Size.X;
            float height = rectangleShape.Size.Y;

            // Check boundaries and reverse direction if necessary
            if (X < 0 || X + width > windowSize.X)
            {
                // Velocity.X = -Velocity.X;
                Velocity = new Vector2f(-Velocity.X, Velocity.Y);
                X = Math.Clamp(X, 0, windowSize.X - width);
            }
            if (Y < 0 || Y + height > windowSize.Y)
            {
                // Velocity.Y = -Velocity.Y;
                Velocity = new Vector2f(Velocity.X, -Velocity.Y);
                Y = Math.Clamp(Y, 0, windowSize.Y - height);
            }

            shape.Position = new Vector2f(X, Y);
        }
    }

    protected override bool CollidesWith(GameObject other)
    {
        float dx = X - other.X;
        float dy = Y - other.Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

        if (shape is RectangleShape rectangleShape)
        {
            return distance < rectangleShape.Size.X * 2 && distance < rectangleShape.Size.Y * 2;
        }
        return false;
    }
}
