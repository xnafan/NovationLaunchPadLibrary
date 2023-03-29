using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System.Drawing;

namespace LaunchPad;
public class NovationLaunchPad : IDisposable
{
    public event ButtonEventHandler? ButtonEvent;
    private BoardState _boardState = new BoardState();

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
        //fails intermittently, but the device exists and can be retrieved using InputDevice.GetAll()[0];
        _inputDevice = InputDevice.GetByName(deviceName);

        _inputDevice.EventReceived += OnEventReceived;
        _inputDevice.StartEventsListening();
        _outputDevice = OutputDevice.GetByName(deviceName);
    }

    public void ButtonOn(int col, int row, ButtonColor color)
    {
        if (_boardState.GetButtonColor(col, row).Equals(color)) { return; }
        int buttonValue = 0;
        if (row == 0)
        {
            buttonValue = 104 + col;
            _outputDevice.SendEvent(new ControlChangeEvent((SevenBitNumber)buttonValue, (SevenBitNumber)(byte)color));
        }
        else
        {
            buttonValue = 16 * (row - 1) + col;
            _outputDevice.SendEvent(new NoteOnEvent((SevenBitNumber)buttonValue, (SevenBitNumber)(byte)color));
        }
        _boardState.ChangeButtonState(col, row, color);
    }

    public void ButtonOff(int col, int row)
    {
        if (_boardState.GetButtonColor(col, row).Equals(ButtonColor.Off)) { return; }
        int buttonValue = 0;
        if (row == 0)
        {
            buttonValue = 104 + col;

            _outputDevice.SendEvent(new ControlChangeEvent((SevenBitNumber)buttonValue, (SevenBitNumber)0));
        }
        else
        {
            buttonValue = 16 * (row - 1) + col;
            _outputDevice.SendEvent(new NoteOffEvent((SevenBitNumber)buttonValue, (SevenBitNumber)0));

        }

        _boardState.ChangeButtonState(col, row, ButtonColor.Off);
    }

    public void AllOff()
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                ButtonOff(x, y);
            }
        }
    }
    public void AllOn(ButtonColor color)
    {
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                ButtonOn(x, y, color);
            }
        }
    }
    private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
    {

        // Console.WriteLine(  e.Event);
        var midiDevice = (MidiDevice)sender;

        if (e.Event is NoteOnEvent buttonEvent)
        {
            ButtonEventType buttonEventType = buttonEvent.Velocity == 0 ? ButtonEventType.Released : ButtonEventType.Pressed;
            int row = buttonEvent.NoteNumber / 16 + 1;
            int column = buttonEvent.NoteNumber % 16;

            ButtonEventArgs buttonArgs = new(ButtonType.GridButton, new Point(column, row), buttonEventType);
            if (buttonArgs.Position.X == 8)
            {
                buttonArgs.Button = RightColumnButtonIndexes[buttonArgs.Position.Y - 1];
            }
            ButtonEvent?.Invoke(this, buttonArgs);
        }
        else if (e.Event is ControlChangeEvent controlButtonEvent)
        {
            ButtonEventType buttonEventType = controlButtonEvent.ControlValue == 0 ? ButtonEventType.Released : ButtonEventType.Pressed;
            int row = 0;
            int column = controlButtonEvent.ControlNumber - 104;
            ButtonEventArgs buttonArgs = new(ButtonType.GridButton, new Point(column, row), buttonEventType);
            buttonArgs.Button = TopRowButtonIndexes[buttonArgs.Position.X];

            ButtonEvent?.Invoke(this, buttonArgs);
        }
    }

    public void Dispose()
    {
        (_inputDevice as IDisposable)?.Dispose();
        (_outputDevice as IDisposable)?.Dispose();
    }

    public override string ToString()
    {
        return _boardState.ToString();
    }

    public ButtonColor GetButtonColor(int x, int y)
    {
        return _boardState.GetButtonColor(x, y);
    }
}
