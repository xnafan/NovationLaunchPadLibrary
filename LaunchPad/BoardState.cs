using System.Runtime.CompilerServices;
using System.Text;

namespace LaunchPad;
public class BoardState
{
    private ButtonColor[][] Buttons  { get; set; } = new ButtonColor[9][];
    private Dictionary<ButtonType, ButtonColor> TopRowButtons { get; set; } = new Dictionary<ButtonType, ButtonColor>();
    private Dictionary<ButtonType, ButtonColor> RightColumnButtons { get; set; } = new Dictionary<ButtonType, ButtonColor>();
    public BoardState()
    {
        for (int i = 0; i <= 8; i++)
        {
            Buttons[i] = new ButtonColor[9];
        }
        TopRowButtons.Add(ButtonType.Up, ButtonColor.Off);
        TopRowButtons.Add(ButtonType.Down, ButtonColor.Off);
        TopRowButtons.Add(ButtonType.Left, ButtonColor.Off);
        TopRowButtons.Add(ButtonType.Right, ButtonColor.Off);
        TopRowButtons.Add(ButtonType.Session, ButtonColor.Off);
        TopRowButtons.Add(ButtonType.User1, ButtonColor.Off);
        TopRowButtons.Add(ButtonType.User2, ButtonColor.Off);
        TopRowButtons.Add(ButtonType.Mixer, ButtonColor.Off);

        RightColumnButtons.Add(ButtonType.Vol, ButtonColor.Off);
        RightColumnButtons.Add(ButtonType.Pan, ButtonColor.Off);
        RightColumnButtons.Add(ButtonType.SndA, ButtonColor.Off);
        RightColumnButtons.Add(ButtonType.SndB, ButtonColor.Off);
        RightColumnButtons.Add(ButtonType.Stop, ButtonColor.Off);
        RightColumnButtons.Add(ButtonType.TrkOn, ButtonColor.Off);
        RightColumnButtons.Add(ButtonType.Solo, ButtonColor.Off);
        RightColumnButtons.Add(ButtonType.Arm, ButtonColor.Off);
        
    }
    public void ChangeButtonState(int col, int row, ButtonColor color)
    {
        Buttons[col][row] = color;
    }

    public void ChangeControllerButtonState(ButtonType buttonType, ButtonColor color)
    {
        if (TopRowButtons.ContainsKey(buttonType))
        {
            TopRowButtons[buttonType] = color;
        }
        else if (RightColumnButtons.ContainsKey(buttonType))
        {
            RightColumnButtons[buttonType] = color;
        }
    }

    public override string ToString()
    {
        StringBuilder builder = new();
        for (int y = 0; y < Buttons[0].Length; y++)
        {
            for (int x = 0; x < Buttons.Length; x++)
            {
                builder.Append(Buttons[x][y].ToString());
                builder.Append('\t');
            }
            builder.Append(Environment.NewLine);
        }
        return builder.ToString();
    }

    internal ButtonColor GetButtonColor(int x, int y)
    {
        return Buttons[x][y];
    }
}
