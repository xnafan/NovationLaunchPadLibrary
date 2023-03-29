namespace Snake
{
    public class DirectionEventArgs : EventArgs
    {
        public DirectionEventArgs(Direction direction)
        {
            Direction = direction;
        }

        public Direction Direction { get; }
    }
}