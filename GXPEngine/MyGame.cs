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
	EasyDraw ammoDisplay;
	EasyDraw scoreDisplay;
	EasyDraw insertCoinDisplay;

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
		Globals.enemySpawner = enemySpawner;

		player = new Player();
		AddChild(player);
		player.SetXY(game.width / 2, game.height + 100);
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

		insertCoinDisplay = new EasyDraw(game.width, game.height, false);
		AddChild(insertCoinDisplay);
		Globals.inserCoinDisplay = insertCoinDisplay;
		insertCoinDisplay.TextAlign(CenterMode.Center, CenterMode.Center);
		insertCoinDisplay.Fill(Color.Black);
		insertCoinDisplay.TextSize(40);
		insertCoinDisplay.Text("insert coin to start");

		music = new Sound("music.mp3", true, true).Play();
		music.Volume = 0.15f;
	}


	void Update()
	{
		float delta = Time.deltaTime / 1000f;

		if (Globals.gameState == Globals.States.InsertCoin && Input.GetKeyDown(Key.SPACE))
			StartGame();

		if (Globals.gameState == Globals.States.InGame)
			Globals.score += delta * 6;

		road.AddOffset(0, -Globals.scrollSpeed * delta);

		playerHeatlhBar.SetScaleXY(15, MathUtils.Map(player.playerHealth, 0, 100, 0, 85));
		playerUnderHealthBar.SetScaleXY(playerHeatlhBar.scaleX, MathUtils.Lerp(playerUnderHealthBar.scaleY, playerHeatlhBar.scaleY, 0.05f * delta * 60));

		ammoDisplay.ClearTransparent();
		ammoDisplay.Fill(Color.LightGreen);
		ammoDisplay.TextSize(40);
		ammoDisplay.Text(player.playerAmmoCount.ToString());

		scoreDisplay.ClearTransparent();
		scoreDisplay.Fill(Color.LightGreen);
		scoreDisplay.TextSize(40);
		scoreDisplay.Text(Math.Floor(Globals.score).ToString());
	}

	void StartGame()
    {
		Globals.gameState = Globals.States.InGame;
		player.Start();
		obstacleSpawner.Start();
		enemySpawner.Start();
		Globals.score = 0;
		insertCoinDisplay.visible = false;
    }

	static void Main()
	{
		new MyGame().Start();
	}
}