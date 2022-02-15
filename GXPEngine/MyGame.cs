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
	ParticleEmitter test;
	ParticleEmitter dustCoulds;

	public MyGame() : base(1280, 720, false)
	{
		road = new AdvancedSprite("road3.png",
			new float[8]{
				0, 0,
				game.width, 0,
				game.width, game.height * 1.5f,
				0, game.height * 1.5f
			});
		AddChild(road);

		enemySpawner = new EnemySpawner();
		AddChild(enemySpawner);
		enemySpawner.SetXY(game.width / 2, game.height);

		obstacleSpawner = new ObstacleSpawner();
		AddChild(obstacleSpawner);
		
		player = new Player();
		AddChild(player);
		player.SetXY(game.width / 2, game.height / 2);

		stats = new EasyDraw(game.width, game.height, false);
		AddChild(stats);

		test = new ParticleEmitter(
			new string[] { "circle.png", "triangle.png" }, 20, 0, 0)
			.ConfigureMovement(0, 2, 10, 180, -0.1f)
			.ConfigureLifeTime(5, 5)
			.ConfigureGravity(Vector2.UP * 0.1f)
			.ConfigureAlpha(1, -0.005f)
			.ConfigureScaling(0.01f, 0.01f, 0, 1, 1)
			.ConfigureRotation(0, 5, -0.1f, 0, 360)
			.ConfigureSpawnArea(10, 10);

		AddChild(test);
		test.SetXY(game.width / 2, game.height / 2);

		dustCoulds = new ParticleEmitter(
			new string[] { "dust_6.png", "dust_7.png", "dust_8.png", "dust_9.png", "dust_10.png", "dust_11.png" }, 0, 0.1f, 0.5f)
			.ConfigureLifeTime(5, 5)
			.ConfigureMovement(0, 10, 10, 0, 0)
			.ConfigureAlpha(0.5f, 0)
			.ConfigureSpawnArea(200, 0)
			.ConfigureScaling(0, 0, 0, 0.1f, 0.5f)
			.ConfigureRotation(0, 0, 0, 0, 360);

		AddChild(dustCoulds);
		dustCoulds.SetXY(game.width / 2, -100);
		//dustCoulds.Emit();

	}


	void Update()
	{
		float delta = Time.deltaTime / 1000f;

		road.AddOffset(0, -Globals.scrollSpeed * delta);

		stats.ClearTransparent();
		stats.Text($"FPS:{currentFps}\n{GetDiagnostics()}");

		if (Input.GetKeyDown(Key.SPACE)) test.Emit();

	}

	static void Main()
	{
		new MyGame().Start();
	}
}