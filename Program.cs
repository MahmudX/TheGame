using System.Diagnostics;
using System.Numerics;
using Microsoft.Extensions.Logging;
using Players;
using SFML.Graphics;
using SFML.Window;

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

            // var removablePlayers = new HashSet<int>();
            // for (int i = 0; i < players.Count; i++)
            // {
            //     if (players[i] is null)
            //         continue;
            //     for (int j = i + 1; j < players.Count; j++)
            //     {
            //         if (players[j] is null)
            //             continue;

            //         if (
            //             players[i].GetType() != players[j].GetType()
            //             && players[i].CheckCollision(players[j])
            //         )
            //         {
            //             removablePlayers.Add(i);
            //             break;
            //         }
            //     }
            // }

            // if (removablePlayers.Count != 0)
            // {
            //     foreach (var item in removablePlayers.OrderByDescending(i => i))
            //     {
            //         players.RemoveAt(item);
            //     }
            // }

            window.Display();
        }
    }
}
