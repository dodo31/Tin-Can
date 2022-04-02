using System.Collections.Generic;

public class Animal : Entity
{
	public List<Entity> Preys = new List<Entity>();
	public List<Animal> Predators = new List<Animal>();

	protected override void Awake()
	{
		
	}

	protected override void Start()
	{

	}

	protected override void Update()
	{

	}
}
