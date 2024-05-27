using Microsoft.Extensions.Logging;
using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;
using System.Numerics;
namespace TheFight;

internal class Program
{
    static void Main(string[] args)
    {
        VideoMode mode = new VideoMode(800, 600);
        RenderWindow window = new RenderWindow(mode, "My Game");
        window.Closed += (sender, e) => { ((RenderWindow)sender).Close(); };


        Console.WriteLine("Player initialization start");
        var sw = Stopwatch.StartNew();
        var players = new List<Player>();
        foreach (var _ in Enumerable.Range(0, 100))
        {
            players.Add(new Player("Player1", Random.Shared.Next(0, 799), Random.Shared.Next(0, 599), PlayerType.Rock));
            players.Add(new Player("Player2", Random.Shared.Next(0, 799), Random.Shared.Next(0, 599), PlayerType.Paper));
            players.Add(new Player("Player3", Random.Shared.Next(0, 799), Random.Shared.Next(0, 599), PlayerType.Scissor));
        }
        sw.Stop();
        Console.WriteLine($"Player initialization took {sw.ElapsedMilliseconds}ms");
        
        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear();

            double deltaTime = 0.016; // Assume 60 FPS for simplicity

            foreach (var player in players)
            {
                player.Update(deltaTime, new SFML.System.Vector2u(800, 600));
                player.Render(window);
            }

            sw.Reset();
            sw.Start();
            var removablePlayers = new HashSet<int>();
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = i + 1; j < players.Count; j++)
                {
                    if (players[i].CheckCollision(players[j]))
                    {
                        removablePlayers.Add(i); break;
                    }
                }
            }

            if (removablePlayers.Count != 0)
            {
                foreach (var item in removablePlayers.OrderByDescending(i => i))
                {
                    players.RemoveAt(item);
                }
            }
            sw.Stop();
            Console.WriteLine($"While loop took {sw.ElapsedMilliseconds}ms");

            window.Display();
            // Thread.Sleep(800);
        }

    }
}
