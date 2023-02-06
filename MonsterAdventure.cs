using System;
using SplashKitSDK;
using System.Collections.Generic;

public class MonsterAdventure
{
    public List<Monster> monsters = new List<Monster>();
    private List<Obstacle> obstacles = new List<Obstacle>();
    public Portal portal;
    public Player player;
    public MonsterAdventure(Window gamewindow)
    {
        portal = new Portal(gamewindow);
        player = new Player(gamewindow);
        SpawnObstacles(gamewindow);
        SpawnMonsters(gamewindow);
    }
    public void SpawnObstacles(Window gamewindow)
    {
        int rndnumber = SplashKit.Rnd(3, 8);
        var count = 0;
        for (int i = 0; i < rndnumber; i++)
        {
            var newObstacle = new Obstacle(gamewindow);
            bool collidedWithObstacle = false;
            bool insidePortalZone = false;
            bool insidePlayerSafetyCircle = false;
            foreach (Obstacle obstacle in obstacles)
            {
                if (newObstacle.CollidesWith(obstacle))
                {
                    collidedWithObstacle = true;
                }
            }
            if (portal.CircleCollision(newObstacle))
            {
                insidePortalZone = true;
            }
            if (player.CircleCollision(newObstacle))
            {
                insidePlayerSafetyCircle = true;
            }
            if (!collidedWithObstacle && !insidePortalZone && !insidePlayerSafetyCircle)
            {
                obstacles.Add(newObstacle);
            }
            count += 1;
        }
    }

    public void SpawnMonsters(Window gamewindow)
    {
        int rndnumber = SplashKit.Rnd(3, 10);
        for (int i = 0; i < rndnumber; i++)
        {
            var newMonster = new Monster(gamewindow);
            bool collidedWithMonster = false;
            bool collidedWithObstacle = false;
            bool insidePortalZone = false;
            bool insidePlayerSafetyCircle = false;
            foreach (Monster monster in monsters)
            {
                if (newMonster.CollidesWith(monster))
                {
                    collidedWithMonster = true;
                }
            }
            foreach (Obstacle obstacle in obstacles)
            {
                if (newMonster.CollidesWith(obstacle))
                {
                    collidedWithObstacle = true;
                }
            }
            if (portal.CircleCollision(newMonster))
            {
                insidePortalZone = true;
            }
            if (player.CircleCollision(newMonster))
            {
                insidePlayerSafetyCircle = true;
            }
            if (!collidedWithMonster && !collidedWithObstacle && !insidePortalZone && !insidePlayerSafetyCircle)
            {
                monsters.Add(newMonster);
            }
        }
    }

    public void Update()
    {
        foreach (Monster monster in monsters)
        {
            if (monster.CollidesWith(portal))
            {
                monster.BounceOff();
            }
            foreach (Obstacle obstacle in obstacles)
            {
                if (monster.CollidesWith(obstacle))
                {
                    monster.BounceOff();
                }

                if (player.CollidesWith(obstacle) && player.movingUp)
                {
                    player.y += player.speed;
                }
                if (player.CollidesWith(obstacle) && player.movingDown)
                {
                    player.y -= player.speed;
                }
                if (player.CollidesWith(obstacle) && player.movingRight)
                {
                    player.x -= player.speed;
                }
                if (player.CollidesWith(obstacle) && player.movingLeft)
                {
                    player.x += player.speed;
                }
            }
            foreach (Monster othermonster in monsters)
            {
                if (monster.CollidesWith(othermonster) && monster != othermonster)
                {
                    monster.BounceOff();
                }
            }
            monster.Update();
        }
    }

    public void MonsterCollision(PlayerLives playerLives)
    {
        Monster monsterCollision = null;
        foreach (Monster monster in monsters)
        {
            if (player.CollidesWith(monster))
            {
                monsterCollision = monster;
                playerLives.Life -= 1;
            }
        }
        if (monsterCollision != null)
        {
            monsters.Remove(monsterCollision);
        }
    }

    public void Draw()
    {
        portal.Draw();
        player.Draw();
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.Draw();
        }
        foreach (Monster a in monsters)
        {
            a.Draw();
        }
    }
    public void HandleInput(Window gamewindow)
    {
        if (SplashKit.KeyDown(KeyCode.WKey) || SplashKit.KeyDown(KeyCode.UpKey))
        {
            player.MoveUp();
        }
        if (SplashKit.KeyDown(KeyCode.SKey) || SplashKit.KeyDown(KeyCode.DownKey))
        {
            player.MoveDown();
        }
        if (SplashKit.KeyDown(KeyCode.AKey) || SplashKit.KeyDown(KeyCode.LeftKey))
        {
            player.MoveLeft();
        }
        if (SplashKit.KeyDown(KeyCode.DKey) || SplashKit.KeyDown(KeyCode.RightKey))
        {
            player.MoveRight();
        }

        if (player.x <= 0)
        {
            player.x = 0;
        }
        if (player.x >= gamewindow.Width - player.bitmap.Width)
        {
            player.x = gamewindow.Width - player.bitmap.Width;
        }
        if (player.y <= 0)
        {
            player.y = 0;
        }
        if (player.y >= gamewindow.Height - player.bitmap.Height)
        {
            player.y = gamewindow.Height - player.bitmap.Height;
        }
    }
}