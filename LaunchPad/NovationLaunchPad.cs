using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchPad;
public class NovationLaunchPad
{
    public event ButtonEventHandler? ButtonEvent;

    private readonly static Dictionary<int, ButtonType> RightColumnButtonIndexes = new Dictionary<int, ButtonType>() { {0, ButtonType.Vol },
    {1, ButtonType.Pan },
    {2, ButtonType.SndA },
    {3, ButtonType.SndB },
    {4, ButtonType.Stop },
    {5, ButtonType.TrkOn },
    {6, ButtonType.Solo },
    {7, ButtonType.Arm }};


    private readonly static Dictionary<int, ButtonType> TopRowButtonIndexes = new Dictionary<int, ButtonType>() { {0, ButtonType.Up },
    {1, ButtonType.Down},
    {2, ButtonType.Left },
    {3, ButtonType.Right },
    {4, ButtonType.Session},
    {5, ButtonType.User1 },
    {6, ButtonType.User2},
    {7, ButtonType.Mixer }};


    private static IInputDevice _inputDevice;
    private static IOutputDevice _outputDevice;

    public NovationLaunchPad(string deviceName = "Launchpad")
    {
        _inputDevice = InputDevice.GetByName(deviceName);
        _inputDevice.EventReceived += OnEventReceived;
        _inputDevice.StartEventsListening();
        _outputDevice = OutputDevice.GetByName(deviceName);
    }

    private static void ButtonOn(int col, int row, ButtonColor color)
    {
        _outputDevice.SendEvent(new NoteOnEvent((SevenBitNumber)(16 * row + col), (SevenBitNumber)(byte)color));
    }

    private static void ButtonOff(int col, int row)
    {
        _outputDevice.SendEvent(new NoteOffEvent((SevenBitNumber)(16 * row + col), (SevenBitNumber)0));
    }

    private static void AllOff()
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                ButtonOff(x, y);
            }
        }
    }
    private static void AllOn(ButtonColor color)
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                ButtonOn(x, y, color);
            }
        }
    }
    private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
    {
        var midiDevice = (MidiDevice)sender;

        if (e.Event is NoteOnEvent noteOnEvent)
        {
            var buttonEvent = (NoteOnEvent)noteOnEvent;
            ButtonEventType buttonEventType = buttonEvent.Velocity == 0 ? ButtonEventType.Released : ButtonEventType.Pressed;
            int row = buttonEvent.NoteNumber / 16;
            int column = buttonEvent.NoteNumber % 16;

            ButtonEventArgs buttonArgs = new (ButtonType.GridButton, new Point(column, row), buttonEventType);
            if(buttonArgs.Position.X == 8)
            {
                buttonArgs.Button = RightColumnButtonIndexes[buttonArgs.Position.Y];
            }
            ButtonEvent?.Invoke(this, buttonArgs);
        }
        else if(e.Event is ControlChangeEvent controlChangeEvent) 
        {
            var buttonEvent = (ControlChangeEvent)controlChangeEvent;
            ButtonEventType buttonEventType = buttonEvent.ControlValue == 0 ? ButtonEventType.Released : ButtonEventType.Pressed;
            int row = -1;
            int column = buttonEvent.ControlNumber -104;
            ButtonEventArgs buttonArgs = new (ButtonType.GridButton, new Point(column, row), buttonEventType);
            buttonArgs.Button = TopRowButtonIndexes[buttonArgs.Position.X];
            
            ButtonEvent?.Invoke(this, buttonArgs);
        }
    }

    //(_inputDevice as IDisposable)?.Dispose();
    //(_outputDevice as IDisposable)?.Dispose();
}
