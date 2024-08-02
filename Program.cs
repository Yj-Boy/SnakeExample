using System.Xml.Linq;

namespace Snake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Snake snake = new Snake(MoveDirection.Up, 20, 20, 4);
            SnakeMap map = new SnakeMap(30, 30, snake);
            SnakeGameCore gameCore = new SnakeGameCore(map);

            gameCore.SnakeMapRender = new SnakeMapRender();

            SnakeController gameController = new SnakeController();
            gameController
                .SetStartKey(ConsoleKey.Spacebar)
                .SetPauseKey(ConsoleKey.P)
                .SetResumeKey(ConsoleKey.P)
                .SetStopKey(ConsoleKey.Escape)
                .SetSnakeKeys(gameCore.Map.Snake,
                    ConsoleKey.LeftArrow, ConsoleKey.UpArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow);

            Console.WriteLine("按 空格 开始游戏 (30x30, x2)");

            gameController.HandleController(gameCore);

            Console.WriteLine("游戏结束");
            Console.ReadKey();
        }
    }
}
