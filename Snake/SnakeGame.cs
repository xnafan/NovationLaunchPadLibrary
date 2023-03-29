using LaunchPad;
using System.Drawing;
namespace Snake;
public class SnakeGame
{
    public enum GameState { Playing, Dead };

    #region Attributes
    private ISnakeController _controller;
    private Snake _snake;
    private NovationLaunchPad _launchPad;
    private readonly static Random random = new Random();
    private Point TopLeftSquare = new(0, 1), BottomRight = new(7, 8);
    Timer _timer;
    Point _apple;
    int _deadTicksLeft;
    GameState _gameState = GameState.Playing;
    #endregion

    public SnakeGame(ISnakeController controller, NovationLaunchPad launchPad)
    {
        _controller = controller;
        _controller.DirectionEvent += Controller_DirectionEvent;
        _launchPad = launchPad;
        ReStart();
        _timer = new Timer(new TimerCallback(Timer_Tick), _snake, 0, 250);
    }

    private void Timer_Tick(object? state)
    {
        lock (this)
        {
            if (_gameState == GameState.Playing)
            {

                if (MoveSnake())
                {
                    MoveAppleIfEaten();
                    Draw();
                }
                else
                {
                    GameOver();
                }
            }
            else
            {
                _deadTicksLeft--;
                if (_deadTicksLeft <= 0)
                {
                    ReStart();
                }
            }
        }
    }

    private bool IsOnBoard(Point point)
    {
        return point.X >= TopLeftSquare.X && point.X <= BottomRight.X && point.Y >= TopLeftSquare.Y && point.Y <= BottomRight.Y;
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

    private bool MoveSnake()
    {
        var nextHeadPosition = _snake.GetNextPosition();
        if (!IsOnBoard(nextHeadPosition)) { return false; }
        if (_snake.Contains(nextHeadPosition)) { _launchPad.ButtonOn(nextHeadPosition.X, nextHeadPosition.Y, ButtonColor.Red); return false; }
        var snakeEnd = _snake.BodyParts.Last();
        _launchPad.ButtonOff(snakeEnd.X, snakeEnd.Y);
        _snake.Move(_apple);
        return true;
    }

    private void GameOver()
    {
        _gameState = GameState.Dead;
        _deadTicksLeft = 3;
        foreach (var bodyPart in _snake.BodyParts)
        {
            _launchPad.ButtonOn(bodyPart.X, bodyPart.Y, ButtonColor.Amber);
        }
    }

    private Point GetRandomApplePoint()
    {
        return new Point(random.Next(TopLeftSquare.X, BottomRight.X), random.Next(TopLeftSquare.Y, BottomRight.Y));
    }

    private void Controller_DirectionEvent(object? sender, DirectionEventArgs e)
    {
        if (e.Direction != _snake.Heading.GetOppositeDirection())
        {
            _snake.Direction = e.Direction;
        }
    }

    private void Draw()
    {
        for (int i = 0; i < _snake.BodyParts.Count; i++)
        {
            var bodyPart = _snake.BodyParts[i];
            _launchPad.ButtonOn(bodyPart.X, bodyPart.Y, ButtonColor.Green);
        }
        _launchPad.ButtonOn(_apple.X, _apple.Y, ButtonColor.Red);
    }

    public void ReStart()
    {
        _gameState = GameState.Playing;
        _launchPad.AllOff();
        _snake = new Snake(new Point(4, 8));
        _snake.Direction = Direction.Up;
        do
        {
            _apple = GetRandomApplePoint();
        } while (_apple == _snake.BodyParts.First());
        Draw();
    }
}
