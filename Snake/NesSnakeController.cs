using NESControllerLibrary;

namespace Snake
{
    internal class NesSnakeController : SnakeControllerBase
    {
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
                        OnButtonChange(Direction.Up);
                        break;
                    case NESControllerButton.Down:
                        OnButtonChange(Direction.Down);
                        break;
                    case NESControllerButton.Left:
                        OnButtonChange(Direction.Left);
                        break;
                    case NESControllerButton.Right:
                        OnButtonChange(Direction.Right);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}