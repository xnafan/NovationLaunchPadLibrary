
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System.Drawing;

namespace Runner
{
    internal class Program
    {

        //static byte[] cols = new byte[9] { 1, 2, 3, 16, 32, 48, 17, 34, 51 };
        static byte[] cols = new byte[] { 3, 48, 51 };

        enum Color : byte
        {
            Off = 0,
            Red = 3,
            Green = 28,
            Amber = 31
        }
        private static IInputDevice _inputDevice;
        private static IOutputDevice _outputDevice;

        static void Main(string[] args)
        {
            _inputDevice = InputDevice.GetByName("Launchpad");
            _inputDevice.EventReceived += OnEventReceived;
            _inputDevice.StartEventsListening();
            _outputDevice = OutputDevice.GetByName("Launchpad");

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


            //ButtonOn(0, 0, 17);
            //Thread.Sleep(pause);
            //ButtonOn(0, 0, 34);
            //Thread.Sleep(pause);
            //ButtonOn(0, 0, 51);
            //Thread.Sleep(pause);

            //Thread.Sleep(10000);

            //for (int y = 0; y < 8; y++)
            //{
            //    for (int x = 0; x < 9; x++)
            //    {
            //        for (int colCounter = 0; colCounter < cols.Length; colCounter++)
            //        {
            //            ButtonOn(y, x, cols[colCounter]);
            //            Thread.Sleep(50);
            //            ButtonOff(y, x);
            //        }
            //    }
            //}
            AllOff();
            int colIndex = 0;
            bool done = false;
            Point lightPosition = new Point(3, 0);
            Point lightDirection = new Point(1, 1);
            while (!done)
            {
                if (lightPosition.X + lightDirection.X >= 8 || lightPosition.X + lightDirection.X < 0)
                {
                    lightDirection.X = -lightDirection.X;
                    colIndex++;
                }
                if (lightPosition.Y + lightDirection.Y >= 8 || lightPosition.Y + lightDirection.Y < 0)
                {
                    lightDirection.Y = -lightDirection.Y;
                    colIndex++;
                }
                colIndex %= cols.Length;
                ButtonOff(lightPosition.X, lightPosition.Y);
                lightPosition.X += lightDirection.X;
                lightPosition.Y += lightDirection.Y;
                ButtonOn(lightPosition.X, lightPosition.Y, cols[colIndex]);
                Thread.Sleep(100);
                done = Console.KeyAvailable;
            }
            
            Console.WriteLine("Input device is listening for events. Press any key to exit...");
            Console.ReadLine();

            (_inputDevice as IDisposable)?.Dispose();
            (_outputDevice as IDisposable)?.Dispose();
        }


        private static void ButtonOn(int col, int row, byte color)
        {
            //Console.WriteLine(Convert.ToString(intensity, 2).PadLeft(8, '0'));
            _outputDevice.SendEvent(new NoteOnEvent((SevenBitNumber)(16 * row + col), (SevenBitNumber)color));
            //_outputDevice.SendEvent(new ControlChangeEvent((SevenBitNumber)(104 + col), (SevenBitNumber)color));
        }

        private static void ButtonOff(int col, int row)
        {
            _outputDevice.SendEvent(new NoteOffEvent((SevenBitNumber)(16 * row + col), (SevenBitNumber)0));
            //_outputDevice.SendEvent(new ControlChangeEvent((SevenBitNumber)(104 + col), (SevenBitNumber)0));
        }
        
        private static void AllOff()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    ButtonOff(x,y);
                }
            }
        }
        private static void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            var midiDevice = (MidiDevice)sender;
            Console.WriteLine($"Event received from '{midiDevice.Name}' at {DateTime.Now}: {e.Event}");
        }
    }
}