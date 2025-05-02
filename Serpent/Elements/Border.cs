using Chalkboard;

namespace Serpent;

public class Border : ContentElement<AppStore>
{
    public override RenderedRect Render(Size size)
    {
        var rect = new RenderedRect(size);
        
        RenderBorder(rect);
        RenderContent(rect);

        return rect;
    }

    private RenderedRect RenderBorder(RenderedRect rect)
    {
        rect[0, 0] = 0x2554;
        rect[rect.Width - 1, 0] = 0x2557;
        rect[rect.Width - 1, rect.Height - 1] = 0x255D;
        rect[0, rect.Height - 1] = 0x255A;

        Enumerable.Range(1, rect.Width - 2).ForEach(left => rect[left, 0] = rect[left, rect.Height - 1] = 0x2550);
        Enumerable.Range(1, rect.Height - 2).ForEach(top => rect[0, top] = rect[rect.Width - 1, top] = 0x2551);

        return rect;
    }

    private void RenderContent(RenderedRect rect)
    {
        if (Content is null)
            return;

        var content = Content.Value;

        var innerRect = rect.GetSubRect(new Thickness(1, 1, 1, 1));
        var contentRect = content.Render(innerRect.GetSize());
        
        innerRect.Apply(contentRect);
    }
}