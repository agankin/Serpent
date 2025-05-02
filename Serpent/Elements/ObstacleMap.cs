
using Chalkboard;

namespace Serpent;

public class ObstacleMap : FuncLayoutElement<AppStore>
{
    public ObstacleMap() : base(GetObstacles)
    {
    }

    private static IEnumerable<Element<AppStore>> GetObstacles(AppStore store)
    {
        foreach (var position in store?.Obstacles ?? [])
        {
            yield return new Obstacle
            {
                Margin = new Margin(Left: position.Left, Top: position.Top)
            };
        } 
    }
}