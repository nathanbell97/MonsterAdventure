using System;
using SplashKitSDK;

public class Monster : GamePiece
{
    private const int Speed = 3;
    private const int TimeTillNewDirectionMin = 150;
    private const int TimeTillNewDirectionMax = 250;
    public double x {get; set;}
    public double y {get; set;}
    private int timeTillNewDirection;
    public Bitmap bitmap {get; set;}
    private Vector2D velocity;
    private Window gameWindow;
    public Monster(Window gamewindow)
    {
        int monsterGenerator = SplashKit.Rnd(0, 10);
        if (monsterGenerator >= 3)
        {
            bitmap = new Bitmap("Monster", "Monster.png");
        }
        else
        {
            bitmap = new Bitmap("Monster2", "Monster2.png");
        }
        x = SplashKit.Rnd(0, gamewindow.Width - bitmap.Width);
        y = SplashKit.Rnd(0, gamewindow.Height - bitmap.Height);
        this.gameWindow = gamewindow;
        SetNewDirection();
    }

    public void SetNewDirection()
    {
        velocity.X = SplashKit.Rnd(-Speed, Speed);
        velocity.Y = SplashKit.Rnd(-Speed, Speed);

        timeTillNewDirection = SplashKit.Rnd(TimeTillNewDirectionMin, TimeTillNewDirectionMax);
    }
    public bool CollidesWith(GamePiece gamePiece)
    {
        return SplashKit.BitmapCollision(this.bitmap, x, y, gamePiece.bitmap, gamePiece.x, gamePiece.y);
    }
    public void BounceOff()
    {
        velocity.X = -velocity.X;
        velocity.Y = -velocity.Y;
    }

    public void Update()
    {
        x += velocity.X;
        y += velocity.Y;
        timeTillNewDirection -= 1;
        if (timeTillNewDirection == 0)
        {
            SetNewDirection();
        }

        if (x < 0 || x > gameWindow.Width - bitmap.Width)
        {
            velocity.X = -velocity.X;
        }
        if (y < 0 || y > gameWindow.Height - bitmap.Height)
        {
            velocity.Y = -velocity.Y;
        }
    }
    public void Draw()
    {
        bitmap.Draw(x, y);
    }





    // public void StayOnWindow(Window gamewindow)
    // {
    //     if (X < 0)
    //     {
    //         X = 0;
    //     }
    //     if (X > gamewindow.Width - Width)
    //     {
    //         X = gamewindow.Width - Width;
    //     }
    //     if (Y < 0)
    //     {
    //         Y = 0;
    //     }
    //     if (Y > gamewindow.Height - Height)
    //     {
    //         Y = gamewindow.Height - Height;
    //     }
    // }
    // private void SetNewDestination()
    // {
    //     int destinationX = SplashKit.Rnd(0, (gameWindow.Width - alienBitmap.Width));
    //     int destinationY = SplashKit.Rnd(0, (gameWindow.Height - alienBitmap.Height));
    //     destinationCircle = SplashKit.CircleAt(destinationX, destinationY, DestinationRadius);

    //     Point2D fromPT = new Point2D()
    //     {
    //         X = this.x,
    //         Y = this.y
    //     };
    //     Point2D toPT = new Point2D()
    //     {
    //         X = destinationX,
    //         Y = destinationY
    //     };

    //     Vector2D dir;
    //     dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPT, toPT));
    //     velocity = SplashKit.VectorMultiply(dir, Speed);
    // }
}