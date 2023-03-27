
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

                Console.WriteLine("Input device is listening for events. Press any key to exit...");
                Console.ReadLine();
            }
        }

        private static void Launchpad_ButtonEvent(object? sender, ButtonEventArgs e)
        {
            Console.WriteLine(e);
            launchpad.ButtonOn(e.Position.X, e.Position.Y, ButtonColor.Red);
        }
    }
}