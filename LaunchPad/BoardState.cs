using System.Text;

namespace LaunchPad;
public class BoardState
{
    private ButtonColor[][] Buttons  { get; set; } = new ButtonColor[9][];
    public BoardState()
    {
        for (int i = 0; i < 9; i++)
        {
            Buttons[i] = new ButtonColor[9];
        }
        
    }
    public void ChangeButtonState(int col, int row, ButtonColor color)
    {
        Buttons[col][row] = color;
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
