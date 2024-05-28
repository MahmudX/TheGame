using System.Diagnostics;
using System.Numerics;
using Microsoft.Extensions.Logging;
using Players;
using SFML.Graphics;
using SFML.Window;
using TheGame;

namespace TheFight;

internal class Program
{
    static void Main(string[] args)
    {
        const int rockCount = 300;
        const int paperCount = 300;
        const int scissorCount = 300;
        const int windowHeight = 600;
        const int windowWidth = 800;

        var rocks = new List<Rock?>(
            Enumerable
                .Range(0, rockCount)
                .Select(_ => new Rock(
                    Random.Shared.Next(0, windowWidth),
                    Random.Shared.Next(0, windowHeight)
                ))
        );

        var papers = new List<Rock?>(
            Enumerable
                .Range(0, rockCount)
                .Select(_ => new Rock( // Temporary assume rock is paper
                    Random.Shared.Next(0, windowWidth),
                    Random.Shared.Next(0, windowHeight)
                ))
        );

        var scissors = new List<Rock?>(
            Enumerable
                .Range(0, rockCount)
                .Select(_ => new Rock( // Temporary assume rock is scissor
                    Random.Shared.Next(0, windowWidth),
                    Random.Shared.Next(0, windowHeight)
                ))
        );

        //var papers = new List<Paper>(Enumerable.Range(0, paperCount).Select(_ => new Paper()));
        //var scissors = new List<Scissor>(Enumerable.Range(0, scissorCount).Select(_ => new Scissor()));

        VideoMode mode = new(windowWidth, windowHeight);
        RenderWindow window = new(mode, "Rock Paper Scissors Championship");
        window.Closed += (sender, e) =>
        {
            ((RenderWindow)sender).Close();
        };
        IEnumerable<GameObject?> allPlayers = rocks.Concat(papers).Concat(scissors);
        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear();

            double deltaTime = 0.016; // Assume 60 FPS for simplicity

            foreach (var player in allPlayers)
            {
                player.Update(deltaTime, window.Size);
                player.Render(window);
            }

            int validRocks = rocks.Count;

            for (int i = 0; i < validRocks; i++)
            {
                var otherPlayers = papers.Concat(scissors);
                foreach (var otherPlayer in otherPlayers)
                {
                    validRocks--;
                    if (rocks[i].CheckCollision(otherPlayer))
                    {
                        rocks[i] = rocks[validRocks];
                        rocks[validRocks] = null;
                        i--;
                        break;
                    }
                }
            }

            rocks = rocks.GetRange(0, validRocks);

            // Same for papers and scissors



            window.Display();
        }
    }

    void CheckCollisionAndRemovePlayer(
        List<Rock?> target,
        List<Rock?> opponent1,
        List<Rock?> opponent2
    )
    {
        int validItems = target.Count;

        for (int i = 0; i < validItems; i++)
        {
            var otherPlayers = opponent1.Concat(opponent2);
            foreach (var otherPlayer in otherPlayers)
            {
                validItems--;
                var collisionState = target[i].CheckCollision(otherPlayer);
                if (collisionState != CollisionType.None)
                {
                    if (collisionState.Equals(CollisionType.Self))
                    {
                        target[i] = rocks[validItems];
                        target[validItems] = null;
                        i--;
                    }
                    else if (collisionState.Equals(CollisionType.Other))
                    {
                        // Do something
                    }

                    break;
                }
            }
        }

        target = rocks.GetRange(0, validItems);
    }
}
