namespace LaunchPad;
public class BoardState
{
    public ButtonColor[][] Buttons  { get; set; } = new ButtonColor[8][];
    public BoardState()
    {
        for (int i = 0; i < 8; i++)
        {
            Buttons[i] = new ButtonColor[8];
        }
    }
}
