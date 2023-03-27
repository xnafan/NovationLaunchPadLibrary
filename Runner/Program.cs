
using LaunchPad;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System.Drawing;

namespace Runner
{
    internal partial class Program
    {       

        static void Main(string[] args)
        {
            NovationLaunchPad launchpad = new NovationLaunchPad();
            launchpad.ButtonEvent += Launchpad_ButtonEvent;

            #region MyRegion
            //for (int i = 0; i < 5; i++)
            //{
            //    AllOn();
            //    Thread.Sleep(1000);
            //    AllOff();
            //    Thread.Sleep(1000); 
            //}

            //int pause = 500;
            //ButtonOn(0, 0, 1);
            //Thread.Sleep(pause);
            //ButtonOn(0, 0, 2);
            //Thread.Sleep(pause);
            //ButtonOn(0, 0, 3);
            //Thread.Sleep(pause);

            //ButtonOn(0, 0, 16);
            //Thread.Sleep(pause);
            //ButtonOn(0, 0, 32);
            //Thread.Sleep(pause);
            //ButtonOn(0, 0, 48);
            //Thread.Sleep(pause);


            #endregion

            #region bumping
            //AllOff();
            //int colIndex = 0;
            //bool done = false;
            //Point lightPosition = new Point(3, 0);
            //Point lightDirection = new Point(1, 1);
            //while (!done)
            //{
            //    if (lightPosition.X + lightDirection.X >= 8 || lightPosition.X + lightDirection.X < 0)
            //    {
            //        lightDirection.X = -lightDirection.X;
            //        colIndex++;
            //    }
            //    if (lightPosition.Y + lightDirection.Y >= 8 || lightPosition.Y + lightDirection.Y < 0)
            //    {
            //        lightDirection.Y = -lightDirection.Y;
            //        colIndex++;
            //    }
            //    colIndex %= cols.Length;
            //    ButtonOff(lightPosition.X, lightPosition.Y);
            //    lightPosition.X += lightDirection.X;
            //    lightPosition.Y += lightDirection.Y;
            //    ButtonOn(lightPosition.X, lightPosition.Y, (byte)cols[colIndex]);
            //    Thread.Sleep(100); 
            //done = Console.KeyAvailable;
            //}
            #endregion
            
            Console.WriteLine("Input device is listening for events. Press any key to exit...");
            Console.ReadLine();

           
        }

        private static void Launchpad_ButtonEvent(object? sender, ButtonEventArgs e)
        {
            Console.WriteLine(e);
        }
    }
}