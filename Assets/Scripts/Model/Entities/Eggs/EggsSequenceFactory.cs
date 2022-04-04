using System.Collections.Generic;

public static class EggsSequenceFactory
{
	public static Queue<EggPool> CreateNewSequence()
	{
		Queue<EggPool> _sequence = new Queue<EggPool>();
		_sequence.Enqueue(new EggPool(1, EntityType.CHIKEN_1, 1));
		_sequence.Enqueue(new EggPool(2, EntityType.COW_1, 1));
		_sequence.Enqueue(new EggPool(3, EntityType.DIPLODOCUS_1, 1));
		_sequence.Enqueue(new EggPool(4, EntityType.FOX_1, 1));
		_sequence.Enqueue(new EggPool(5, EntityType.FROG_1, 1));
		_sequence.Enqueue(new EggPool(6, EntityType.GOAT_1, 1));
		_sequence.Enqueue(new EggPool(7, EntityType.RABBIT_1, 1));
		_sequence.Enqueue(new EggPool(8, EntityType.SNAKE_1, 1));
		_sequence.Enqueue(new EggPool(9, EntityType.T_REX_1, 1));
		_sequence.Enqueue(new EggPool(10, EntityType.TAPIR_1, 1));
		_sequence.Enqueue(new EggPool(11, EntityType.TERMITE_1, 1));
		_sequence.Enqueue(new EggPool(12, EntityType.BEAR_1, 1));
		_sequence.Enqueue(new EggPool(13, EntityType.BEE_HIVE_1, 1));
		_sequence.Enqueue(new EggPool(14, EntityType.BUSH_1, 1));
		_sequence.Enqueue(new EggPool(15, EntityType.FLOWER_1, 1));
		_sequence.Enqueue(new EggPool(16, EntityType.MOSQUITO_1, 1));
		_sequence.Enqueue(new EggPool(17, EntityType.TREE_1, 1));
		_sequence.Enqueue(new EggPool(18, EntityType.GRASS_1, 1));
		return _sequence;
	}
}