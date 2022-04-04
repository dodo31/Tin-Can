using System.Collections.Generic;

public static class EggsSequenceFactory
{
	public static Queue<EggPool> CreateNewSequence()
	{
		Queue<EggPool> _sequence = new Queue<EggPool>();
		_sequence.Enqueue(new EggPool(2, EntityType.CHIKEN_1, 5));
		_sequence.Enqueue(new EggPool(5, EntityType.RABBIT_1, 5));
		_sequence.Enqueue(new EggPool(12, EntityType.TREE_1, 5));
		
		return _sequence;
	}
}