using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	public EntityType Type;
	public EntityPreset Preset;

	public float Vitality;

	protected virtual void Awake()
	{

	}

	protected virtual void Start()
	{

	}

	protected virtual void Update()
	{
		
	}
}
