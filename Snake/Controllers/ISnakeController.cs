namespace Snake.Controllers
{
    public interface ISnakeController
    {
        event DirectionEventHandler? DirectionEvent;
    }
}