public class Egg : Entity
{
	protected override void FixedUpdate()
	{
		this.GrowIfRequired();
	}

	protected override bool IsAlive()
	{
		return false;
	}

	protected override void Die()
	{

	}
}