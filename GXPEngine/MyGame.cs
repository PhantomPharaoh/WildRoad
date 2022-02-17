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
	EasyDraw ammoDisplay;
	EasyDraw scoreDisplay;

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
			0, 0.02f, 0.04f)
			.ConfigureGravity(Vector2.UP)
			.ConfigureAlpha(1, -0.01f)
			.ConfigureScaling(0.01f, 0.01f, 0, 0.1f, 0.2f)
			.ConfigureMovement(0, 0, 1, 180, 0);
		AddChild(dust);
		//dust.Emit();

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

		ammoDisplay = new EasyDraw(game.width, game.height, false);
		AddChild(ammoDisplay);
		ammoDisplay.SetXY(1200, -155);

		scoreDisplay = new EasyDraw(game.width, game.height, false);
		AddChild(scoreDisplay);
		scoreDisplay.SetXY(495, 300);
		scoreDisplay.TextAlign(CenterMode.Center, CenterMode.Center);

		music = new Sound("music.mp3", true, true).Play();
		music.Volume = 0.15f;
	}


	void Update()
	{
		float delta = Time.deltaTime / 1000f;

		Globals.score += delta * 6;

		road.AddOffset(0, -Globals.scrollSpeed * delta);

		playerHeatlhBar.SetScaleXY(15, MathUtils.Map(player.playerHealth, 0, 100, 0, 85));
		playerUnderHealthBar.SetScaleXY(playerHeatlhBar.scaleX, MathUtils.Lerp(playerUnderHealthBar.scaleY, playerHeatlhBar.scaleY, 0.05f * delta * 60));

		dust.SetXY(player.x, player.y+50);

		ammoDisplay.ClearTransparent();
		ammoDisplay.Fill(Color.LightGreen);
		ammoDisplay.TextSize(40);
		ammoDisplay.Text(player.playerAmmoCount.ToString());

		scoreDisplay.ClearTransparent();
		scoreDisplay.Fill(Color.LightGreen);
		scoreDisplay.TextSize(40);
		scoreDisplay.Text(Math.Floor(Globals.score).ToString());
	}

	static void Main()
	{
		new MyGame().Start();
	}
}