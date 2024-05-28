using SFML.Graphics;
using SFML.System;

namespace TheFight;

class Player
{
    public float X { get; set; }
    public float Y { get; set; }
    public string Name { get; set; }

    private CircleShape shape;
    public Vector2f Velocity { get; set; }
    private const float Speed = 30.0f;
    public PlayerType PlayerType { get; set; }

    public Player(string name, float x, float y, PlayerType playerType)
    {
        Name = name;
        PlayerType = playerType;
        X = x;
        Y = y;
        shape = new CircleShape(5)
        {
            FillColor = playerType switch
            {
                PlayerType.Rock => Color.Green,
                PlayerType.Paper => Color.White,
                _ => Color.Red
            },
            Position = new Vector2f(X, Y)
        };

        // Initialize velocity with a random direction
        float angle = (float)(Random.Shared.NextDouble() * 2 * Math.PI);
        Velocity = new Vector2f((float)Math.Cos(angle), (float)Math.Sin(angle)) * Speed;
    }

    public void Update(double deltaTime, Vector2u windowSize)
    {
        X += Velocity.X * (float)deltaTime;
        Y += Velocity.Y * (float)deltaTime;

        // Check boundaries and reverse direction if necessary
        if (X - shape.Radius < 0 || X + shape.Radius > windowSize.X)
        {
            // Velocity.X = -Velocity.X;
            Velocity = new Vector2f(-Velocity.X, Velocity.Y);
            X = Math.Clamp(X, shape.Radius, windowSize.X - shape.Radius);
        }
        if (Y - shape.Radius < 0 || Y + shape.Radius > windowSize.Y)
        {
            // Velocity.Y = -Velocity.Y;
            Velocity = new Vector2f(Velocity.X, -Velocity.Y);
            Y = Math.Clamp(Y, shape.Radius, windowSize.Y - shape.Radius);
        }

        shape.Position = new Vector2f(X, Y);
        //Console.WriteLine($"shape.Position = new Vector2f({X}, {Y});");
        //Console.WriteLine($"Velocity = {Velocity});");
    }

    public void Render(RenderWindow window)
    {
        window.Draw(shape);
    }

    public bool CollidesWith(Player other)
    {
        float dx = X - other.X;
        float dy = Y - other.Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);
        return distance < shape.Radius * 2;
    }

    public bool CheckCollision(Player other)
    {
        if (CollidesWith(other))
        {
            // Console.WriteLine($"{Name} collided with {other.Name}");

            if (PlayerType == PlayerType.Rock && other.PlayerType == PlayerType.Paper)
            {
                return true;
            }
            else if (PlayerType == PlayerType.Paper && other.PlayerType == PlayerType.Scissor)
            {
                return true;
            }
            else if (PlayerType == PlayerType.Scissor && other.PlayerType == PlayerType.Rock)
            {
                return true;
            }
        }
        return false;
    }
}

enum PlayerType
{
    Rock,
    Paper,
    Scissor
}
