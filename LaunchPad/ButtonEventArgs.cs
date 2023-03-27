using Melanchall.DryWetMidi.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchPad;
public partial class ButtonEventArgs : EventArgs
{
    public ButtonType Button { get; set; }
    public Point Position { get; set; }

    public ButtonEventType EventType { get; set; } 
    
public ButtonEventArgs(ButtonType button, Point position, ButtonEventType eventType)
    {
        Button = button;
        Position = position;
        EventType = eventType;
    }
    public override string ToString()
    {
        return $"ButtonEventArgs. Button: {Button}, Position: {Position}, EventType: {EventType}";
    }
}
