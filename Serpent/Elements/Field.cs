using Chalkboard;

namespace Serpent;

public class Field : LayoutElement<AppStore>
{
    public override RenderedRect Render(Size size)
    {
        var rect = new RenderedRect(size);
        return Children.Aggregate(rect, (rect, child) => child.Render(size));
    }
}