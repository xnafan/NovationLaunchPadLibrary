namespace Snake.Controllers;
public class SnakeControllerBase : ISnakeController
{
    public event DirectionEventHandler? DirectionEvent;
    protected void OnButtonChange(Direction direction)
    {
        DirectionEvent?.Invoke(this, new DirectionEventArgs(direction));
    }
}