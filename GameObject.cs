using SFML.Graphics;
using SFML.System;
namespace TheFight;

public abstract class GameObject
{
    public float X { get; set; }
    public float Y { get; set; }
    protected Vector2f Velocity { get; set; }
    protected Shape shape;

    public abstract void Update(double deltaTime, Vector2u windowSize);

    public virtual void Render(RenderWindow window)
    {
        window.Draw(shape);
    }
}
