using System;
using SplashKitSDK;

public class Obstacle : GamePiece
{
    public double x { get; set; }
    public double y { get; set; }
    public Bitmap bitmap { get; set; }
    public Obstacle(Window gamewindow)
    {
        int obstaclepicker = SplashKit.Rnd(1, 6);
        if (obstaclepicker <= 2)
        {
            bitmap = new Bitmap("obstacle1", "obstacle1.png");
        }
        if (obstaclepicker <= 3 && obstaclepicker > 2)
        {
            bitmap = new Bitmap("obstacle2", "obstacle2.png");
        }
        if (obstaclepicker <= 4 && obstaclepicker > 3)
        {
            bitmap = new Bitmap("obstacle3", "obstacle3.png");
        }
        if (obstaclepicker <= 5 && obstaclepicker > 4)
        {
            bitmap = new Bitmap("spikes", "spikes.png");
        }

        x = SplashKit.Rnd(0, (gamewindow.Width - bitmap.Width));
        y = SplashKit.Rnd(0, (gamewindow.Height - bitmap.Height));
    }
    public bool CollidesWith(GamePiece gamePiece)
    {
        return SplashKit.BitmapCollision(this.bitmap, x, y, gamePiece.bitmap, gamePiece.x, gamePiece.y);
    }


    public void Draw()
    {
        bitmap.Draw(x, y);
    }
}
