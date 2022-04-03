using UnityEngine;

public class Human : Entity
{
	private HumanState _currentState;
	
	protected override void Awake()
	{
		base.Awake();
		
		_currentState = HumanState.Idle;
		transform.localScale = Vector3.one;
	}

	protected override void Update()
	{
		base.Update();

		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.position += Vector3.up * HumandPreset.MoveSpeed * Time.timeScale;
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.position += Vector3.down * HumandPreset.MoveSpeed * Time.timeScale;
		}
		
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position += Vector3.left * HumandPreset.MoveSpeed * Time.timeScale;
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += Vector3.right * HumandPreset.MoveSpeed * Time.timeScale;
		}
	}

	protected override bool IsAlive()
	{
		return _currentState != HumanState.Dead;
	}

	protected override void Die()
	{
		this.PublishDeath();
	}

	private HumanPreset HumandPreset
	{
		get
		{
			return (HumanPreset)Preset;
		}
	}
}