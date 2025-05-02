using Chalkboard;

namespace Serpent;

public class Obstacle : Element<AppStore>
{
    public override RenderedRect Render(Size size)
    {
        var rect = new RenderedRect(size);
        var (left, top) = Margin?.Value ?? default;

        rect[left, top] = new Symbol(' ', Foreground: Color.White, Background: Color.DarkRed);

        return rect;
    }
}