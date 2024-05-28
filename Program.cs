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

        List<GameObject?> rocks =
            new(
                Enumerable
                    .Range(0, rockCount)
                    .Select(_ => new Rock(
                        Random.Shared.Next(0, windowWidth),
                        Random.Shared.Next(0, windowHeight)
                    ))
            );

        List<GameObject?> papers =
            new(
                Enumerable
                    .Range(0, paperCount)
                    .Select(_ => new Paper(
                        Random.Shared.Next(0, windowWidth),
                        Random.Shared.Next(0, windowHeight)
                    ))
            );
        List<GameObject?> scissors =
            new(
                Enumerable
                    .Range(0, scissorCount)
                    .Select(_ => new Scissors(
                        Random.Shared.Next(0, windowWidth),
                        Random.Shared.Next(0, windowHeight)
                    ))
            );

        VideoMode mode = new(windowWidth, windowHeight);
        RenderWindow window = new(mode, "Rock Paper Scissors Championship");
        window.Closed += (sender, e) =>
        {
            ((RenderWindow)sender).Close();
        };
        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear();

            double deltaTime = 0.016; // Assume 60 FPS for simplicity

            var allPlayers = rocks.Concat(papers).Concat(scissors);
            foreach (var player in allPlayers)
            {
                player.Update(deltaTime, window.Size);
                player.Render(window);
            }

            CheckCollisionAndRemovePlayer(ref rocks, papers, scissors);
            CheckCollisionAndRemovePlayer(ref papers, rocks, scissors);
            CheckCollisionAndRemovePlayer(ref scissors, rocks, papers);
            window.Display();
        }
    }

    static void CheckCollisionAndRemovePlayer(
        ref List<GameObject?> target,
        List<GameObject?> opponent1,
        List<GameObject?> opponent2
    )
    {
        int validItems = target.Count;

        for (int i = 0; i < validItems; i++)
        {
            var otherPlayers = opponent1.Concat(opponent2);
            foreach (var otherPlayer in otherPlayers)
            {
                if (target[i].CheckCollision(otherPlayer))
                {
                    validItems--;
                    target[i] = target[validItems];
                    target[validItems] = null;
                    i--;
                    break;
                }
            }
        }

        if (validItems < target.Count)
        {
            System.Console.WriteLine($"Removing {target.Count - validItems} players");
            target = target.GetRange(0, validItems);
        }
    }
}
