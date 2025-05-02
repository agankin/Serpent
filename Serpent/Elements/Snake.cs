using Chalkboard;

namespace Serpent;

public class Snake : Element<AppStore>
{
    public override RenderedRect Render(Size size)
    {
        var initialRect = new RenderedRect(size);
        return Store.Snake
            .Select(IndexedPosition.Create)
            .Aggregate(initialRect, AddPosition);
    }

    private static RenderedRect AddPosition(RenderedRect rect, IndexedPosition indexedPosition)
    {
        var (index, position) = indexedPosition;

        var ch = index switch
        {
            0 => ':',
            _ => ' '
        };
        
        Color color = index switch
        {
            0 => Color.DarkGray,
            _ => Color.Green
        };

        return rect.Apply(new PositionedSymbol(position, new(ch, Color.Green, color)));
    }

    private record IndexedPosition(int Index, Position Position)
    {
        public static IndexedPosition Create(Position position, int idx) => new(idx, position);
    }
}