// using SFML.Graphics;
// using SFML.System;
// using TheFight;

// namespace Players;

// public class Paper : GameObject
// {
//     public Paper(float x, float y)
//         : base(x, y)
//     {
//         shape = new RectangleShape()
//         {
//             FillColor = Color.White,
//             Position = new Vector2f(X, Y),
//             Size = new Vector2f(10, 10)
//         };
//     }

//     public override void Update(double deltaTime, Vector2u windowSize)
//     {
//         if (shape is RectangleShape rectangleShape)
//         {
//             X += Velocity.X * (float)deltaTime;
//             Y += Velocity.Y * (float)deltaTime;

//             float width = rectangleShape.Size.X;
//             float height = rectangleShape.Size.Y;

//             // Check boundaries and reverse direction if necessary
//             if (X < 0 || X + width > windowSize.X)
//             {
//                 // Velocity.X = -Velocity.X;
//                 Velocity = new Vector2f(-Velocity.X, Velocity.Y);
//                 X = Math.Clamp(X, 0, windowSize.X - width);
//             }
//             if (Y < 0 || Y + height > windowSize.Y)
//             {
//                 // Velocity.Y = -Velocity.Y;
//                 Velocity = new Vector2f(Velocity.X, -Velocity.Y);
//                 Y = Math.Clamp(Y, 0, windowSize.Y - height);
//             }

//             shape.Position = new Vector2f(X, Y);
//         }
//     }
// }
