using System;
using SplashKitSDK;

public class Program
{
    public static bool Quit = false;
    public static bool TimerTicks(Timer timer)
    {
        return timer.Ticks % 250 == 0;
    }
    public static void Main()
    {
        Window w = new Window("Alien Fun", 1400, 850);
        MonsterAdventure monsterAdventure = new MonsterAdventure(w);
        PlayerLives playerLives = new PlayerLives(w);
        Timer timer = new Timer("Game Timer");

        Font font = new Font("Roboto", "Roboto-Regular.ttf");
        int Score = 0;

        timer.Start();
        while (!w.CloseRequested && !Quit && playerLives.Life > 0)
        {
            if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                Quit = true;
            }

            w.Clear(Color.White);
            
            SplashKit.ProcessEvents();
            monsterAdventure.Update();
            monsterAdventure.Draw();
            monsterAdventure.HandleInput(w);
            monsterAdventure.MonsterCollision(playerLives);
            playerLives.Draw();

            if (TimerTicks(timer))
            {
                Score += 1;
            }
            w.DrawText($"SCORE: {Convert.ToString(Score)}", Color.Black, font, 11, w.Width - 60, 70);

            if (monsterAdventure.player.CollidesWith(monsterAdventure.portal))
            {
                monsterAdventure = new MonsterAdventure(w);
                Score += 5;
            }
            w.Refresh(60);
        }
    }
}

// public class MonsterAdventure
// {
//     public List<Monster> monsters = new List<Monster>();
//     private List<Obstacle> obstacles = new List<Obstacle>();
//     public Portal portal;
//     public Player player;
//     public MonsterAdventure(Window gamewindow)
//     {
//         portal = new Portal(gamewindow);
//         player = new Player(gamewindow);
//         SpawnObstacles(gamewindow);
//         SpawnMonsters(gamewindow);
//     }
//     public void SpawnObstacles(Window gamewindow)
//     {
//         int rndnumber = SplashKit.Rnd(3, 8);
//         var count = 0;
//         for (int i = 0; i < rndnumber; i++)
//         {
//             var newObstacle = new Obstacle(gamewindow);
//             bool collidedWithObstacle = false;
//             bool insidePortalZone = false;
//             bool insidePlayerSafetyCircle = false;
//             foreach (Obstacle obstacle in obstacles)
//             {
//                 if (newObstacle.CollidesWith(obstacle))
//                 {
//                     collidedWithObstacle = true;
//                 }
//             }
//             if (portal.CircleCollision(newObstacle))
//             {
//                 insidePortalZone = true;
//             }
//             if (player.CircleCollision(newObstacle))
//             {
//                 insidePlayerSafetyCircle = true;
//             }
//             if (!collidedWithObstacle && !insidePortalZone && !insidePlayerSafetyCircle)
//             {
//                 obstacles.Add(newObstacle);
//             }
//             count += 1;
//         }
//     }

//     public void SpawnMonsters(Window gamewindow)
//     {
//         int rndnumber = SplashKit.Rnd(3, 10);
//         for (int i = 0; i < rndnumber; i++)
//         {
//             var newMonster = new Monster(gamewindow);
//             bool collidedWithMonster = false;
//             bool collidedWithObstacle = false;
//             bool insidePortalZone = false;
//             bool insidePlayerSafetyCircle = false;
//             foreach (Monster monster in monsters)
//             {
//                 if (newMonster.CollidesWith(monster))
//                 {
//                     collidedWithMonster = true;
//                 }
//             }
//             foreach (Obstacle obstacle in obstacles)
//             {
//                 if (newMonster.CollidesWith(obstacle))
//                 {
//                     collidedWithObstacle = true;
//                 }
//             }
//             if (portal.CircleCollision(newMonster))
//             {
//                 insidePortalZone = true;
//             }
//             if (player.CircleCollision(newMonster))
//             {
//                 insidePlayerSafetyCircle = true;
//             }
//             if (!collidedWithMonster && !collidedWithObstacle && !insidePortalZone && !insidePlayerSafetyCircle)
//             {
//                 monsters.Add(newMonster);
//             }
//         }
//     }

//     public void Update()
//     {
//         foreach (Monster monster in monsters)
//         {
//             if (monster.CollidesWith(portal))
//             {
//                 monster.BounceOff();
//             }
//             foreach (Obstacle obstacle in obstacles)
//             {
//                 if (monster.CollidesWith(obstacle))
//                 {
//                     monster.BounceOff();
//                 }

//                 if (player.CollidesWith(obstacle) && player.movingUp)
//                 {
//                     player.y += player.speed;
//                 }
//                 if (player.CollidesWith(obstacle) && player.movingDown)
//                 {
//                     player.y -= player.speed;
//                 }
//                 if (player.CollidesWith(obstacle) && player.movingRight)
//                 {
//                     player.x -= player.speed;
//                 }
//                 if (player.CollidesWith(obstacle) && player.movingLeft)
//                 {
//                     player.x += player.speed;
//                 }
//             }
//             foreach (Monster othermonster in monsters)
//             {
//                 if (monster.CollidesWith(othermonster) && monster != othermonster)
//                 {
//                     monster.BounceOff();
//                 }
//             }
//             monster.Update();
//         }
//     }

//     public void MonsterCollision(PlayerLives playerLives)
//     {
//         Monster monsterCollision = null;
//         foreach (Monster monster in monsters)
//         {
//             if (player.CollidesWith(monster))
//             {
//                 monsterCollision = monster;
//                 playerLives.Life -= 1;
//             }
//         }
//         if (monsterCollision != null)
//         {
//             monsters.Remove(monsterCollision);
//         }
//     }

//     public void Draw()
//     {
//         portal.Draw();
//         player.Draw();
//         foreach (Obstacle obstacle in obstacles)
//         {
//             obstacle.Draw();
//         }
//         foreach (Monster a in monsters)
//         {
//             a.Draw();
//         }
//     }
//     public void HandleInput(Window gamewindow)
//     {
//         if (SplashKit.KeyDown(KeyCode.WKey) || SplashKit.KeyDown(KeyCode.UpKey))
//         {
//             player.MoveUp();
//         }
//         if (SplashKit.KeyDown(KeyCode.SKey) || SplashKit.KeyDown(KeyCode.DownKey))
//         {
//             player.MoveDown();
//         }
//         if (SplashKit.KeyDown(KeyCode.AKey) || SplashKit.KeyDown(KeyCode.LeftKey))
//         {
//             player.MoveLeft();
//         }
//         if (SplashKit.KeyDown(KeyCode.DKey) || SplashKit.KeyDown(KeyCode.RightKey))
//         {
//             player.MoveRight();
//         }

//         if (player.x <= 0)
//         {
//             player.x = 0;
//         }
//         if (player.x >= gamewindow.Width - player.bitmap.Width)
//         {
//             player.x = gamewindow.Width - player.bitmap.Width;
//         }
//         if (player.y <= 0)
//         {
//             player.y = 0;
//         }
//         if (player.y >= gamewindow.Height - player.bitmap.Height)
//         {
//             player.y = gamewindow.Height - player.bitmap.Height;
//         }
//     }
// }

// public class Player : GamePiece
// {
//     public int speed = 3;
//     public Bitmap bitmap {get; set;}
//     public double x {get; set;}
//     public double y {get; set;}
//     public bool movingLeft;
//     public bool movingRight;
//     public bool movingUp;
//     public bool movingDown;


//     public Player(Window window)
//     {
//         bitmap = new Bitmap("HeroLeft", "HeroLeft.png");
//         x = (window.Width / 2) - (bitmap.Width / 2);
//         y = window.Height - bitmap.Height;
//     }

//     public Circle SafetyCircle()
//     {
//         return SplashKit.CircleAt(x + bitmap.Width / 2, y + bitmap.Height / 2, 150);
//     }

//     public bool CircleCollision(GamePiece gamePiece)
//     {
//         return SplashKit.BitmapCircleCollision(gamePiece.bitmap, gamePiece.x, gamePiece.y, SafetyCircle());
//     }

//     public bool CollidesWith(GamePiece gamePiece)
//     {
//         return SplashKit.BitmapCollision(this.bitmap, x, y, gamePiece.bitmap, gamePiece.x, gamePiece.y);
//     }

//     public void Draw()
//     {
//         if (movingLeft)
//         {
//             bitmap = new Bitmap("HeroLeft", "HeroLeft.png");
//         }
//         if (movingRight)
//         {
//             bitmap = new Bitmap("HeroRight", "HeroRight.png");
//         }
//         bitmap.Draw(x, y);
//     }
//     public void MoveUp()
//     {
//         y -= speed;
//         movingUp = true;
//         movingDown = false;
//     }
//     public void MoveDown()
//     {
//         y += speed;
//         movingDown = true;
//         movingUp = false;
//     }
//     public void MoveLeft()
//     {
//         x -= speed;
//         movingLeft = true;
//         movingRight = false;
//     }
//     public void MoveRight()
//     {
//         x += speed;
//         movingRight = true;
//         movingLeft = false;
//     }


// }

// public class Obstacle : GamePiece
// {
//     public double x { get; set; }
//     public double y { get; set; }
//     public Bitmap bitmap { get; set; }
//     public Obstacle(Window gamewindow)
//     {
//         int obstaclepicker = SplashKit.Rnd(1, 6);
//         if (obstaclepicker <= 2)
//         {
//             bitmap = new Bitmap("obstacle1", "obstacle1.png");
//         }
//         if (obstaclepicker <= 3 && obstaclepicker > 2)
//         {
//             bitmap = new Bitmap("obstacle2", "obstacle2.png");
//         }
//         if (obstaclepicker <= 4 && obstaclepicker > 3)
//         {
//             bitmap = new Bitmap("obstacle3", "obstacle3.png");
//         }
//         if (obstaclepicker <= 5 && obstaclepicker > 4)
//         {
//             bitmap = new Bitmap("spikes", "spikes.png");
//         }

//         x = SplashKit.Rnd(0, (gamewindow.Width - bitmap.Width));
//         y = SplashKit.Rnd(0, (gamewindow.Height - bitmap.Height));
//     }
//     public bool CollidesWith(GamePiece gamePiece)
//     {
//         return SplashKit.BitmapCollision(this.bitmap, x, y, gamePiece.bitmap, gamePiece.x, gamePiece.y);
//     }


//     public void Draw()
//     {
//         bitmap.Draw(x, y);
//     }
// }


// public class Portal : GamePiece
// {
//     public Bitmap bitmap {get; set;}
//     public double x {get; set;}
//     public double y {get; set;}

//     public Portal(Window window)
//     {
//         bitmap = new Bitmap("Portal", "Portal.png");
//         x = (window.Width / 2) - (bitmap.Width / 2);
//         y = (window.Height / 8) - bitmap.Height;
//     }

//     public Circle SafetyCircle()
//     {
//         return SplashKit.CircleAt(x + (bitmap.Width / 2), y + (bitmap.Height / 2), 150);
//     }

//     public bool CircleCollision(GamePiece gamePiece)
//     {
//         return SplashKit.BitmapCircleCollision(gamePiece.bitmap, gamePiece.x, gamePiece.y, SafetyCircle());
//     }

//     public bool CollidesWith(GamePiece gamePiece)
//     {
//         return SplashKit.BitmapCollision(this.bitmap, x, y, gamePiece.bitmap, gamePiece.x, gamePiece.y);
//     }

//     public void Draw()
//     {
//         bitmap.Draw(x, y);        
//     }


// }


// public class Monster : GamePiece
// {
//     private const int Speed = 3;
//     private const int TimeTillNewDirectionMin = 150;
//     private const int TimeTillNewDirectionMax = 250;
//     public double x {get; set;}
//     public double y {get; set;}
//     private int timeTillNewDirection;
//     public Bitmap bitmap {get; set;}
//     private Vector2D velocity;
//     private Window gameWindow;
//     public Monster(Window gamewindow)
//     {
//         int monsterGenerator = SplashKit.Rnd(0, 10);
//         if (monsterGenerator >= 3)
//         {
//             bitmap = new Bitmap("Monster", "Monster.png");
//         }
//         else
//         {
//             bitmap = new Bitmap("Monster2", "Monster2.png");
//         }
//         x = SplashKit.Rnd(0, gamewindow.Width - bitmap.Width);
//         y = SplashKit.Rnd(0, gamewindow.Height - bitmap.Height);
//         this.gameWindow = gamewindow;
//         SetNewDirection();
//     }

//     public void SetNewDirection()
//     {
//         velocity.X = SplashKit.Rnd(-Speed, Speed);
//         velocity.Y = SplashKit.Rnd(-Speed, Speed);

//         timeTillNewDirection = SplashKit.Rnd(TimeTillNewDirectionMin, TimeTillNewDirectionMax);
//     }
//     public bool CollidesWith(GamePiece gamePiece)
//     {
//         return SplashKit.BitmapCollision(this.bitmap, x, y, gamePiece.bitmap, gamePiece.x, gamePiece.y);
//     }
//     public void BounceOff()
//     {
//         velocity.X = -velocity.X;
//         velocity.Y = -velocity.Y;
//     }

//     public void Update()
//     {
//         x += velocity.X;
//         y += velocity.Y;
//         timeTillNewDirection -= 1;
//         if (timeTillNewDirection == 0)
//         {
//             SetNewDirection();
//         }

//         if (x < 0 || x > gameWindow.Width - bitmap.Width)
//         {
//             velocity.X = -velocity.X;
//         }
//         if (y < 0 || y > gameWindow.Height - bitmap.Height)
//         {
//             velocity.Y = -velocity.Y;
//         }
//     }
//     public void Draw()
//     {
//         bitmap.Draw(x, y);
//     }

// }

// public class PlayerLives
// {
//     private int _life = 5;
//     public int Life
//     {
//         get
//         {
//             return _life;
//         }
//         set
//         {
//             _life = value;
//         }
//     }
//     private int HeartsX { get; set; }
//     private int HeartsY { get; set; }
//     private Bitmap _PlayerHearts = new Bitmap($"LifeHearts", "LifeHearts.png");

//     public PlayerLives(Window playerwindow)
//     {
//         this.HeartsX = playerwindow.Width - 45;
//         this.HeartsY = 10;
//     }
//     public void Draw()
//     {
//         int Offset = 0;
//         for (int i = 0; i < Life; i++)
//         {
//             _PlayerHearts.Draw((HeartsX - Offset), HeartsY);
//             Offset += 45;
//         }
//     }
//     public bool Alive()
//     {
//         if (Life > 0)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }
// }

// public interface GamePiece
// {
//     Bitmap bitmap {get;}
//     double x {get;}
//     double y {get;}
//     void Draw();
//     bool CollidesWith(GamePiece gamePiece);
// }