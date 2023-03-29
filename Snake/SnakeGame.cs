using LaunchPad;
using System.Drawing;
namespace Snake;
public class SnakeGame
{
    #region Attributes
    private ISnakeController _controller;
    private Snake _snake;
    private NovationLaunchPad _launchPad;
    private readonly static Random random = new Random();
    private Point TopLeftSquare = new(0, 1), BottomRight = new(7, 8);
    Timer _timer;
    Point _apple; 
    #endregion

    public SnakeGame(ISnakeController controller, NovationLaunchPad launchPad)
    {
        _controller = controller;
        _controller.DirectionEvent += Controller_DirectionEvent;            
        _launchPad = launchPad;
        ReStart();
        _timer = new Timer(new TimerCallback(Timer_Tick), _snake, 0, 600);
    }

    private void Timer_Tick(object? state)
    {
        MoveSnake();
        
        if (_snake.Head.X < TopLeftSquare.X || _snake.Head.X > BottomRight.X || _snake.Head.Y < TopLeftSquare.Y || _snake.Head.Y > BottomRight.Y) { ReStart(); }
        MoveAppleIfEaten();
        Draw();
    }

    private void MoveAppleIfEaten()
    {
        if (_snake.Head == _apple)
        {
            do
            {
                _apple = GetRandomApplePoint();
            } while (_snake.Contains(_apple));
        }
    }

    private void MoveSnake()
    {
        var snakeEnd = _snake.BodyParts.Last();
        _launchPad.ButtonOff(snakeEnd.X, snakeEnd.Y);
        if (_snake.MoveAndReturnCollisionWithOwnBody(_apple)) { ReStart(); }
    }

    private Point GetRandomSnakePoint()
    {
        return new Point(random.Next(4,6), random.Next(4,6));
    }
    private Point GetRandomApplePoint()
    {
        return new Point(random.Next(TopLeftSquare.X, BottomRight.X), random.Next(TopLeftSquare.Y, BottomRight.Y));
    }

    private void Controller_DirectionEvent(object? sender, DirectionEventArgs e)
    {
        _snake.Direction = e.Direction;
        Draw();
    }

    private void Draw()
    {
        for (int i = 0; i < _snake.BodyParts.Count; i++)
        {
            var bodyPart = _snake.BodyParts[i];
            _launchPad.ButtonOn(bodyPart.X, bodyPart.Y, ButtonColor.Green);
            Console.Write(bodyPart);
        }
        Console.WriteLine();
        _launchPad.ButtonOn(_apple.X, _apple.Y, ButtonColor.Red);
    }

    public void ReStart()
    {
        _launchPad.AllOff();
        var randomPoint = GetRandomSnakePoint();
        _snake = new Snake(randomPoint);
        _snake.Direction = Direction.Up;
        do
        {
            _apple = GetRandomSnakePoint();
        } while (_apple == _snake.BodyParts.First());
        Draw();
    }
}
