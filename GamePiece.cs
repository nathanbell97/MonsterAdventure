using SplashKitSDK;

public interface GamePiece
{
    Bitmap bitmap {get;}
    double x {get;}
    double y {get;}
    void Draw();
    bool CollidesWith(GamePiece gamePiece);
}