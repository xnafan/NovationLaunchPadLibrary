using NESControllerLibrary;

namespace Snake
{
    internal class NesSnakeController : ISnakeController
    {
        public event DirectionEventHandler? DirectionEvent;

        private NESController _controller;

        public NesSnakeController()
        {
            _controller = new NESController();
            _controller.ButtonStateChanged += _controller_ButtonStateChanged;
        }

        private void _controller_ButtonStateChanged(object? sender, NESControllerEventArgs e)
        {
            if (e.Action == NESControllerButtonAction.Pressed)
            {
                switch (e.Button)
                {
                    case NESControllerButton.Up:
                        DirectionEvent?.Invoke(this, new DirectionEventArgs(Direction.Up));
                        break;
                    case NESControllerButton.Down:
                        DirectionEvent?.Invoke(this, new DirectionEventArgs(Direction.Down));
                        break;
                    case NESControllerButton.Left:
                        DirectionEvent?.Invoke(this, new DirectionEventArgs(Direction.Left));
                        break;
                    case NESControllerButton.Right:
                        DirectionEvent?.Invoke(this, new DirectionEventArgs(Direction.Right));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}