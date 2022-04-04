public class EggPool
{
	private float _timestamp;

	private EntityType _type;
	private int _poolSize;

	public EggPool(float timeStamp, EntityType type, int poolSize)
	{
		_timestamp = timeStamp;

		_type = type;
		_poolSize = poolSize;
	}

	public float Timestamp { get => _timestamp; set => _timestamp = value; }
	public EntityType Type { get => _type; set => _type = value; }
	public int PoolSize { get => _poolSize; set => _poolSize = value; }
}