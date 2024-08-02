namespace Snake
{
    internal class SnakeMap
    {
        private int width;
        private int height;
        private (int x, int y) food;
        private Snake snake;

        public int Width => width;
        public int Height => height;
        public (int X, int Y) Food => food;
        public Snake Snake => snake;

        public SnakeMap(int width, int height, Snake snake)
        {
            this.width = width;
            this.height = height;
            this.snake = snake;
            RefreshFood();
        }

        public bool RefreshFood()
        {
            Random random = new Random();
            (int x, int y) newFood;

            bool regen = false;
            do
            {
                regen = false;
                int x = random.Next(0, width);
                int y = random.Next(0, height);
                newFood = (x, y);

                if (snake.Head == newFood)
                {
                    regen = true;
                }

                if (snake.Body.Contains(newFood))
                {
                    regen = true;
                }
            }
            while (regen);

            food = newFood;
            return true;
        }

        public bool NextStep()
        {
            bool requireRefreshFood = false;

            if (snake == null)
                return false;

            snake.NextStep();

            if (snake.IsDead)
                return false;

            var snakeHead = snake.Head;
            if (snakeHead == food)
            {
                snake.NeedBodySetment = true;
                requireRefreshFood = true;
            }

            if (snakeHead.X < 0 || snakeHead.X >= width || snakeHead.Y < 0 || snakeHead.Y >= height)
            {
                snake.MakeDead();
                return false;
            }

            if (requireRefreshFood)
                RefreshFood();
            return true;
        }
    }
}
