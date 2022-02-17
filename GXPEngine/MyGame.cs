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
	Sprite playerHeatlhBar;
	Sprite playerUnderHealthBar;
	ParticleEmitter dust;

	SoundChannel music;

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

		dust = new ParticleEmitter(
			new string[] { "smoke_07.png" },
			0, 0.1f, 0.2f)
			.ConfigureGravity(Vector2.UP);
		AddChild(dust);
		dust.Emit();

		player = new Player();
		AddChild(player);
		player.SetXY(game.width / 2, game.height / 2);
		Globals.player = player;

		Pivot bulletHolder = new Pivot();
		AddChild(bulletHolder);
		Globals.bulletHolder = bulletHolder;

		Sprite healthBarBackground = new Sprite("healthBarBackground.png", false, false);
		AddChild(healthBarBackground);
		healthBarBackground.SetXY(962, 720);
		healthBarBackground.rotation = 180;
		healthBarBackground.SetScaleXY(15, 85);

		playerUnderHealthBar = new Sprite("underHealthBar.png", false, false);
		AddChild(playerUnderHealthBar);
		playerUnderHealthBar.SetXY(healthBarBackground.x, healthBarBackground.y);
		playerUnderHealthBar.rotation = 180;

		playerHeatlhBar = new Sprite("healthBar.png", false, false);
		AddChild(playerHeatlhBar);
		playerHeatlhBar.SetXY(healthBarBackground.x, healthBarBackground.y);
		playerHeatlhBar.rotation = 180;

		AdvancedSprite dashboard = new AdvancedSprite("dashboard.png",
			new float[8]{
				game.width * 0.65f, game.height * 0.65f,
				game.width, game.height * 0.65f,
				game.width, game.height,
				game.width * 0.65f, game.height
			});
		AddChild(dashboard);

		music = new Sound("music.mp3", true, true).Play();
		music.Volume = 0.15f;
	}


	void Update()
	{
		float delta = Time.deltaTime / 1000f;

		road.AddOffset(0, -Globals.scrollSpeed * delta);

		playerHeatlhBar.SetScaleXY(15, MathUtils.Map(player.playerHealth, 0, 100, 0, 85));
		playerUnderHealthBar.SetScaleXY(playerHeatlhBar.scaleX, MathUtils.Lerp(playerUnderHealthBar.scaleY, playerHeatlhBar.scaleY, 0.05f * delta * 60));

		dust.SetXY(player.x, player.y);
	}

	static void Main()
	{
		new MyGame().Start();
	}
}