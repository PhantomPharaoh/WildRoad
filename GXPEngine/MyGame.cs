using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game
{

	AdvancedSprite road;
	Player player;
	EnemySpawner enemySpawner;
	ObstacleSpawner obstacleSpawner;

	public MyGame() : base(1280, 720, false)
	{
	
		road = new AdvancedSprite("road.png",
			new float[8]{
				game.width*0.3f, 0,
				game.width*0.7f, 0,
				game.width*0.7f, game.height,
				game.width*0.3f, game.height
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

		ParticleSpawner test = new ParticleSpawner(
			new string[] { "circle.png" })
			.ConfigureMovement(0, 1, 1, 0, 0, 0);

		AddChild(test);
		test.SetXY(game.width / 2, game.height / 2);
		test.Emit();
	}


	void Update()
	{
		float delta = Time.deltaTime / 1000f;

		road.AddOffset(0, -Globals.scrollSpeed * delta);
	}

	static void Main()
	{
		new MyGame().Start();
	}
}