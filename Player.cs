using System;
using SplashKitSDK;

public class Player : GamePiece
{
    public int speed = 3;
    public Bitmap bitmap {get; set;}
    public double x {get; set;}
    public double y {get; set;}
    public bool movingLeft;
    public bool movingRight;
    public bool movingUp;
    public bool movingDown;


    public Player(Window window)
    {
        bitmap = new Bitmap("HeroLeft", "HeroLeft.png");
        x = (window.Width / 2) - (bitmap.Width / 2);
        y = window.Height - bitmap.Height;
    }

    public Circle SafetyCircle()
    {
        return SplashKit.CircleAt(x + bitmap.Width / 2, y + bitmap.Height / 2, 150);
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
        if (movingLeft)
        {
            bitmap = new Bitmap("HeroLeft", "HeroLeft.png");
        }
        if (movingRight)
        {
            bitmap = new Bitmap("HeroRight", "HeroRight.png");
        }
        bitmap.Draw(x, y);
    }
    public void MoveUp()
    {
        y -= speed;
        movingUp = true;
        movingDown = false;
    }
    public void MoveDown()
    {
        y += speed;
        movingDown = true;
        movingUp = false;
    }
    public void MoveLeft()
    {
        x -= speed;
        movingLeft = true;
        movingRight = false;
    }
    public void MoveRight()
    {
        x += speed;
        movingRight = true;
        movingLeft = false;
    }


}