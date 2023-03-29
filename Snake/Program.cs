using Snake.Controllers;

namespace Snake;
internal class Program
{
    static void Main(string[] args)
        {
        LaunchPad.NovationLaunchPad launchpad = new LaunchPad.NovationLaunchPad();
        SnakeGame snakeGame = new SnakeGame(new NesSnakeController(), launchpad);
        Console.ReadLine();
    }
}