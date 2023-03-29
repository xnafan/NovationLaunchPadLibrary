using LaunchPad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake;
public class LaunchPadController : ISnakeController
{
    public event DirectionEventHandler? DirectionEvent;

    NovationLaunchPad _launchPad = new NovationLaunchPad();
    public LaunchPadController()
    {
        _launchPad.ButtonEvent += _launchPad_ButtonEvent;
    }

    private void _launchPad_ButtonEvent(object? sender, ButtonEventArgs e)
    {
        if(e.Button == ButtonType.Up || e.Button == ButtonType.Down || e.Button == ButtonType.Left|| e.Button == ButtonType.Right)
        {
            switch (e.Button)
            {
                case ButtonType.Up:
                    OnDirectionEvent(Direction.Up);
                    break;
                case ButtonType.Down:
                    OnDirectionEvent(Direction.Up);
                    break;
                case ButtonType.Left:
                    OnDirectionEvent(Direction.Left);
                    break;
                case ButtonType.Right:
                    OnDirectionEvent(Direction.Right);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnDirectionEvent(Direction direction) => DirectionEvent?.Invoke(this, new DirectionEventArgs(direction));
}
