using System.Collections;
using System.Collections.Immutable;
using Chalkboard;

namespace Serpent;

public record SnakeModel(ImmutableList<Position> body) : IEnumerable<Position>
{
    private readonly ImmutableList<Position> _body = body;
    
    public SnakeModel(Position head, Transformer<Position> nextSegment, int length)
        : this(Generate(head, nextSegment, length))
    {
    }

    public Position Head => _body[0];

    public IEnumerable<Position> Tail => _body.Skip(1);

    public SnakeModel Move(Func<Position, Position> moveHead)
    {
        var newBody = _body.RemoveAt(_body.Count - 1)
            .Insert(0, moveHead(Head));

        return new(newBody);
    }

    private static ImmutableList<Position> Generate(Position head, Transformer<Position> getNext, int length)
    {
        if (length <= 0)
            throw new ArgumentException("Snake must have length.", nameof(length));

        return Enumerable.Range(0, length)
            .Aggregate(
                ImmutableList<Position>.Empty.Add(head),
                (list, _) => list.Add(getNext(GetLast(list))));
    }

    private static Position GetLast(ImmutableList<Position> list) => list[list.Count - 1];

    public IEnumerator<Position> GetEnumerator() => _body.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}