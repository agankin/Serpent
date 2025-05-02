using Chalkboard;

namespace Serpent;

public static class SnakeTransformer
{
    public static Transformer<SnakeModel> CreateMoveTransformer(SnakeDirection direction)
    {
        return direction switch
        {
            SnakeDirection.Up => MoveUp,
            SnakeDirection.Down => MoveDown,
            SnakeDirection.Left => MoveLeft,
            SnakeDirection.Right => MoveRight,
            _ => throw new InvalidOperationException($"Unsupported {nameof(SnakeDirection)} enum value: {direction}.")
        };
    }

    private static SnakeModel MoveUp(SnakeModel model) => model.Move(head => head.TranslateTop(-1));

    private static SnakeModel MoveDown(SnakeModel model) => model.Move(head => head.TranslateTop(1));

    private static SnakeModel MoveLeft(SnakeModel model) => model.Move(head => head.TranslateLeft(-1));

    private static SnakeModel MoveRight(SnakeModel model) => model.Move(head => head.TranslateLeft(1));
}