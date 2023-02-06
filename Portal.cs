using System;
using SplashKitSDK;

public class Portal : GamePiece
{
    public Bitmap bitmap {get; set;}
    public double x {get; set;}
    public double y {get; set;}

    public Portal(Window window)
    {
        bitmap = new Bitmap("Portal", "Portal.png");
        x = (window.Width / 2) - (bitmap.Width / 2);
        y = (window.Height / 8) - bitmap.Height;
    }

    public Circle SafetyCircle()
    {
        return SplashKit.CircleAt(x + (bitmap.Width / 2), y + (bitmap.Height / 2), 150);
    }

    public bool CircleCollision(GamePiece gamePiece)
    {
        return SplashKit.BitmapCircleCollision(gamePiece.bitmap, gamePiece.x, gamePiece.y, SafetyCircle());
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