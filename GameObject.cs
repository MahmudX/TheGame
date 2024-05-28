using SFML.Graphics;
using SFML.System;

namespace TheFight;

public abstract class GameObject
{
    public GameObject(float x, float y)
    {
        X = x;
        Y = y;
        float angle = (float)(Random.Shared.NextDouble() * 2 * Math.PI);
        Velocity = new Vector2f((float)Math.Cos(angle), (float)Math.Sin(angle)) * Speed;
    }

    private const float Speed = 30.0f;
    public float X { get; set; }
    public float Y { get; set; }
    protected Vector2f Velocity { get; set; }
    protected Shape shape;

    public abstract void Update(double deltaTime, Vector2u windowSize);

    public virtual void Render(RenderWindow window)
    {
        window.Draw(shape);
    }

    protected abstract bool CollidesWith(GameObject other);
    public abstract bool CheckCollision(GameObject other);
}
