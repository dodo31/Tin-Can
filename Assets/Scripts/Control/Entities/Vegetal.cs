using UnityEngine;

public class Vegetal : Entity
{
	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();
	}

	public override void Die()
	{
		this.PublishDeath();
	}
}
