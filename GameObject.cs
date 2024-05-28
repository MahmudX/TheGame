using SFML.Graphics;
using SFML.System;

namespace TheFight;

public abstract class GameObject(float x, float y)
{
    public float X { get; set; } = x;
    public float Y { get; set; } = y;
    protected Vector2f Velocity { get; set; }
    protected Shape shape;

    public abstract void Update(double deltaTime, Vector2u windowSize);

    public virtual void Render(RenderWindow window)
    {
        window.Draw(shape);
    }

    protected abstract bool CollidesWith(GameObject other);
    public abstract CollisionType CheckCollision(GameObject other);
}
