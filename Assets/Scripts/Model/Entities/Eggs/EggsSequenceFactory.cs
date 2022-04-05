using System.Collections.Generic;

public static class EggsSequenceFactory
{
	public static Queue<EggPool> CreateNewSequence()
	{
		Queue<EggPool> _sequence = new Queue<EggPool>();
		_sequence.Enqueue(new EggPool(0, EntityType.GRASS_1, 10));
		_sequence.Enqueue(new EggPool(30, EntityType.RABBIT_1, 5));
		_sequence.Enqueue(new EggPool(60, EntityType.TREE_1, 5));
		_sequence.Enqueue(new EggPool(90, EntityType.CHIKEN_1, 5));
		_sequence.Enqueue(new EggPool(120, EntityType.COW_1, 4));
		_sequence.Enqueue(new EggPool(150, EntityType.GRASS_1, 20));
		_sequence.Enqueue(new EggPool(200, EntityType.FOX_1, 8));
		_sequence.Enqueue(new EggPool(250, EntityType.BUSH_1, 20));
		_sequence.Enqueue(new EggPool(300, EntityType.SNAKE_1, 8));

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