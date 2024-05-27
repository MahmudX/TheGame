using SFML.Graphics;
namespace TheFight;

class GameObject
{
    public float X { get; set; }
    public float Y { get; set; }

    public virtual void Update(double deltaTime)
    {
        // Update object state
    }

    public virtual void Render(RenderWindow window)
    {
        // Render object
    }
}
