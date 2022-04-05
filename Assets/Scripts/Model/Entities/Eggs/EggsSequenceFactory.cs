using System.Collections.Generic;

public static class EggsSequenceFactory
{
	public static Queue<EggPool> CreateNewSequence()
	{
		Queue<EggPool> _sequence = new Queue<EggPool>();
		float speed = 2/3f;
		_sequence.Enqueue(new EggPool(10 * speed, EntityType.GRASS_1, 10));
		_sequence.Enqueue(new EggPool(40 * speed, EntityType.RABBIT_1, 5));
		_sequence.Enqueue(new EggPool(65 * speed, EntityType.TREE_1, 5));
		_sequence.Enqueue(new EggPool(90 * speed, EntityType.CHIKEN_1, 5));
		_sequence.Enqueue(new EggPool(120 * speed, EntityType.COW_1, 4));
		_sequence.Enqueue(new EggPool(150 * speed, EntityType.GRASS_1, 20));
		_sequence.Enqueue(new EggPool(200 * speed, EntityType.FOX_1, 8));
		_sequence.Enqueue(new EggPool(250 * speed, EntityType.BUSH_1, 20));
		_sequence.Enqueue(new EggPool(300 * speed, EntityType.SNAKE_1, 8));
		_sequence.Enqueue(new EggPool(350 * speed, EntityType.FLOWER_1, 15));
		_sequence.Enqueue(new EggPool(450 * speed, EntityType.GOAT_1, 15));
		_sequence.Enqueue(new EggPool(550 * speed, EntityType.MOSQUITO_1, 15));
		_sequence.Enqueue(new EggPool(600 * speed, EntityType.FROG_1, 8));
		_sequence.Enqueue(new EggPool(750 * speed, EntityType.TERMITE_1, 10));
		_sequence.Enqueue(new EggPool(850 * speed, EntityType.TAPIR_1, 8));
		_sequence.Enqueue(new EggPool(1000 * speed, EntityType.BEE_HIVE_1, 15));
		_sequence.Enqueue(new EggPool(1100 * speed, EntityType.BEAR_1, 5));
		_sequence.Enqueue(new EggPool(1400 * speed, EntityType.DIPLODOCUS_1, 4));
		_sequence.Enqueue(new EggPool(1600 * speed, EntityType.T_REX_1, 3));

		/*
		 * 	
	
	RABBIT_1,
	CHIKEN_1,
	COW_1,
	FOX_1,
	SNAKE_1,
	GOAT_1,
	FROG_1,
	TERMITE_1,
	TAPIR_1,
	BEAR_1,
	DIPLODOCUS_1,
	T_REX_1,
	
	GRASS_1,
	TREE_1,
	BUSH_1,
	FLOWER_1,
	MOSQUITO_1,
	BEE_HIVE_1,

		 * 
		 */
		return _sequence;
	}
}