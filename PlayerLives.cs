using System;
using SplashKitSDK;

public class PlayerLives
{
    private int _life = 5;
    public int Life
    {
        get
        {
            return _life;
        }
        set
        {
            _life = value;
        }
    }
    private int HeartsX { get; set; }
    private int HeartsY { get; set; }
    private Bitmap _PlayerHearts = new Bitmap($"LifeHearts", "LifeHearts.png");

    public PlayerLives(Window playerwindow)
    {
        this.HeartsX = playerwindow.Width - 45;
        this.HeartsY = 10;
    }
    public void Draw()
    {
        int Offset = 0;
        for (int i = 0; i < Life; i++)
        {
            _PlayerHearts.Draw((HeartsX - Offset), HeartsY);
            Offset += 45;
        }
    }
    public bool Alive()
    {
        if (Life > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}