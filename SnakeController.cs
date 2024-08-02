using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class SnakeController
    {
        public SnakeController()
        {
            snakeKeys = new Dictionary<Snake, (ConsoleKey left, ConsoleKey up, ConsoleKey right, ConsoleKey down)>();
        }

        ConsoleKey startKey, pausekey, resumeKey, stopkey;
        Dictionary<Snake, (ConsoleKey left, ConsoleKey up, ConsoleKey right, ConsoleKey down)> snakeKeys;

        void GameControllerLoopAction(SnakeGameCore game)
        {
            if (game == null)
                return;

            while (!game.Stoped)
            {
                ConsoleKey key = System.Console.ReadKey().Key;

                if (key == startKey && !game.Started)
                    game.Start();

                if (key == pausekey && !game.Paused)
                    game.Pause();
                else if (key == resumeKey && game.Paused)
                    game.Resume();

                if (key == stopkey && !game.Stoped)
                    game.Stop();

                foreach (var snakeKV in snakeKeys)
                {
                    if (key == snakeKV.Value.left)
                        snakeKV.Key.MoveDirection = MoveDirection.Left;
                    else if (key == snakeKV.Value.up)
                        snakeKV.Key.MoveDirection = MoveDirection.Up;
                    else if (key == snakeKV.Value.right)
                        snakeKV.Key.MoveDirection = MoveDirection.Right;
                    else if (key == snakeKV.Value.down)
                        snakeKV.Key.MoveDirection = MoveDirection.Down;
                }
            }
        }

        public void HandleController(SnakeGameCore game)
        {
            GameControllerLoopAction(game);
        }

        public Task HandleControllerAsync(SnakeGameCore game)
        {
            return Task.Run(() => GameControllerLoopAction(game));
        }


        public SnakeController SetPauseKey(ConsoleKey key)
        {
            pausekey = key;
            return this;
        }

        public SnakeController SetResumeKey(ConsoleKey key)
        {
            resumeKey = key;
            return this;
        }

        public SnakeController SetSnakeKeys(Snake snake, ConsoleKey left, ConsoleKey up, ConsoleKey right, ConsoleKey down)
        {
            snakeKeys[snake] = (left, up, right, down);
            return this;
        }

        public SnakeController SetStartKey(ConsoleKey key)
        {
            startKey = key;
            return this;
        }

        public SnakeController SetStopKey(ConsoleKey key)
        {
            stopkey = key;
            return this;
        }
    }
}
