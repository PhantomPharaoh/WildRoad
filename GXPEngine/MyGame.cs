using System;
using GXPEngine;
using System.Drawing;
using GXPEngine.Core;

public class MyGame : Game
{

	AdvancedSprite road;
	Player player;
	EnemySpawner enemySpawner;
	ObstacleSpawner obstacleSpawner;
	EasyDraw stats;
	Sprite playerHeatlhBar;
	Sprite playerUnderHealthBar;

	public MyGame() : base(1366, 768, true)
	{
		road = new AdvancedSprite("road3.png",
			new float[8]{
				0, 0,
				game.width, 0,
				game.width, game.height * 1.5f,
				0, game.height * 1.5f
			});
		AddChild(road);
		
		obstacleSpawner = new ObstacleSpawner();
		AddChild(obstacleSpawner);

		enemySpawner = new EnemySpawner();
		AddChild(enemySpawner);
		enemySpawner.SetXY(game.width / 2, game.height);

		player = new Player();
		AddChild(player);
		player.SetXY(game.width / 2, game.height / 2);
		Globals.player = player;

		Pivot bulletHolder = new Pivot();
		AddChild(bulletHolder);
		Globals.bulletHolder = bulletHolder;

		stats = new EasyDraw(game.width, game.height, false);
		AddChild(stats);

		Sprite healthBarBackground = new Sprite("healthBarBackground.png", false, false);
		AddChild(healthBarBackground);
		healthBarBackground.SetXY(75, 717);
		healthBarBackground.rotation = 180;
		healthBarBackground.SetScaleXY(15, 85);

		playerUnderHealthBar = new Sprite("underHealthBar.png", false, false);
		AddChild(playerUnderHealthBar);
		playerUnderHealthBar.SetXY(75, 717);
		playerUnderHealthBar.rotation = 180;

		playerHeatlhBar = new Sprite("healthBar.png", false, false);
		AddChild(playerHeatlhBar);
		playerHeatlhBar.SetXY(75, 717);
		playerHeatlhBar.rotation = 180;

		AdvancedSprite dashboard = new AdvancedSprite("dashboard.png",
			new float[8]{
				0, game.height * 0.65f,
				game.width * 0.35f, game.height * 0.65f,
				game.width * 0.35f, game.height,
				0, game.height
			});
		AddChild(dashboard);

	}


	void Update()
	{
		float delta = Time.deltaTime / 1000f;

		road.AddOffset(0, -Globals.scrollSpeed * delta);

		stats.ClearTransparent();
		stats.Text($"FPS:{currentFps}\n{GetDiagnostics()}");

		playerHeatlhBar.SetScaleXY(15, MathUtils.Map(player.playerHealth, 0, 100, 0, 85));
		playerUnderHealthBar.SetScaleXY(playerHeatlhBar.scaleX, MathUtils.Lerp(playerUnderHealthBar.scaleY, playerHeatlhBar.scaleY, 0.05f * delta * 60));
	}

	static void Main()
	{
		new MyGame().Start();
	}
}