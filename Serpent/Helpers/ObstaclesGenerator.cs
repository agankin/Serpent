using Chalkboard;

namespace Serpent;

public static class ObstaclesGenerator
{
    public static IReadOnlyCollection<Position> Generate(Size fieldSize, SnakeModel snake, int count)
    {
        var rnd = new Random();
        var obstacles = new HashSet<Position>();
        
        while (count > 0)
        {
            var left = rnd.Next(fieldSize.Width);
            var top = rnd.Next(fieldSize.Height);
            var obstaclePosition = new Position(left, top);

            if (snake.Contains(obstaclePosition))
                continue;

            if (obstacles.Contains(obstaclePosition))
                continue;

            obstacles.Add(obstaclePosition);
            count--;
        }

        return obstacles;
    }
}