using System.Drawing;

namespace Snake;
public class Snake
{
    public Direction Direction { get; set; }
    public List<BodyPart> BodyParts { get; set; }

    public BodyPart Head { get { return BodyParts.First(); } }
    public Snake(Point startingPoint)
    {
        Direction = Direction.Up;
        BodyParts = new List<BodyPart> { startingPoint };
    }

    public void Move(Point applePosition)
    {
        Point tailPosition = BodyParts.Last();
        for (int bCounter = BodyParts.Count - 1; bCounter >= 1; bCounter--)
        {
            BodyParts[bCounter].X = BodyParts[bCounter - 1].X;
            BodyParts[bCounter].Y = BodyParts[bCounter - 1].Y;
        }
        var head = BodyParts.First();
        head.Move(Direction);
        if (head == applePosition)
        {
            BodyParts.Add(tailPosition);
        }
    }

    private bool MoveResultInCollisionWithBody()
    {
        Point headPosition = Head;
        BodyPart newHeadPosition = headPosition;
        newHeadPosition.Move(Direction);
        return Contains(newHeadPosition, true);
    }

    public Direction Heading{
        get { 
            if(BodyParts.Count == 1) { return Direction; }
            Point headDirectionFromBody = new Point(BodyParts[0].X - BodyParts[1].X, BodyParts[0].Y - BodyParts[1].Y);
            if (headDirectionFromBody.X == -1) { return Direction.Left; }
            if (headDirectionFromBody.X == 1) { return Direction.Right; }
            if (headDirectionFromBody.Y == -1) { return Direction.Up; }
            if (headDirectionFromBody.Y == 1) { return Direction.Down; }
            return Direction.None;

        }
    }
    public bool Contains(Point point, bool excludeHeadInCheck = false)
    {
        var partsToCheck = BodyParts.Select(item => item);
        if (excludeHeadInCheck) { partsToCheck = partsToCheck.Where(bp => bp != Head); }
        return partsToCheck.Any(part => part.X == point.X && part.Y == point.Y);
    }

    public Point GetNextPosition()
    {
        Point headPosition = Head;
        BodyPart newHeadPosition = new BodyPart(headPosition);
        newHeadPosition.Move(Direction);
        return newHeadPosition;
    }
}

public class BodyPart
{
    public int X { get; set; }
    public int Y { get; set; }
    public BodyPart(int x, int y)
    {
        X = x;
        Y = y;
    }
    public BodyPart(Point position)
    {
        X = position.X;
        Y = position.Y;
    }

    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.None: break;
            case Direction.Up: Y--; break;
            case Direction.Down: Y++; break;
            case Direction.Left: X--; break;
            case Direction.Right: X++; break;
            default: break;
        }
    }
    public static implicit operator Point(BodyPart bodyPart) => new Point(bodyPart.X, bodyPart.Y);
    public static implicit operator BodyPart(Point point) => new BodyPart(point.X, point.Y);

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}