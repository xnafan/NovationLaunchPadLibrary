
using LaunchPad;

namespace Runner
{
    public class Program
    {

        static NovationLaunchPad launchpad;
        static void Main(string[] args)
        {
            using (launchpad = new NovationLaunchPad())
            {
                launchpad.ButtonEvent += Launchpad_ButtonEvent;
                launchpad.AllOff();
                Console.WriteLine("Input device is listening for events. Press any key to exit...");
                Console.ReadLine();
            }
        }

        private static void Launchpad_ButtonEvent(object? sender, ButtonEventArgs e)
        {
            // Console.WriteLine(e);
            if (e.EventType == ButtonEventType.Pressed)
            {
                ButtonColor newColor = ButtonColor.Off;
                ButtonColor currentColor = launchpad.GetButtonColor(e.Position.X, e.Position.Y);
                switch (currentColor)
                {
                    case ButtonColor.Off:
                        newColor = ButtonColor.Green;
                        break;
                    case ButtonColor.Green:
                        newColor = ButtonColor.Amber;
                        break;
                    case ButtonColor.Amber:
                        newColor = ButtonColor.Red;
                        break;
                    case ButtonColor.Red:
                        newColor = ButtonColor.Off;
                        break;
                    default:
                        break;
                }

                launchpad.ButtonOn(e.Position.X, e.Position.Y, newColor); 
                Console.WriteLine(launchpad);
            }
        }
    }
    
}