using System;
using UnityEngine;

public class Vegetal : Entity
{
	private VegetalState _currentState;

	protected override void Awake()
	{
		base.Awake();

		_currentState = VegetalState.Idle;
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		if (this.IsAlive())
		{
			this.GrowIfRequired();
			this.ManageNormalLife();
		}
	}

	public void ManageNormalLife()
	{
		if (Vitality == VegetalPreset.MaxVitality)
		{
			this.Reproduce();
		}
	}

	private void Reproduce()
	{
		Vector2 radiusRange = VegetalPreset.BirthRadiusRange;
		float birthRadius = UnityEngine.Random.Range(radiusRange.x, radiusRange.y);
		float birthAngle = UnityEngine.Random.Range(0, Mathf.PI * 2);

		float birthPosX = transform.position.x + Mathf.Cos(birthAngle) * birthRadius;
		float birthPosY = transform.position.x + Mathf.Sin(birthAngle) * birthRadius;

		Vector3 birthPosition = new Vector3(birthPosX, birthPosY, 0);

		this.PublishBirth(birthPosition);
		this.OffsetVitality(-VegetalPreset.ReproductionCost);
	}

	protected override bool IsAlive()
	{
		return _currentState != VegetalState.Dead;
	}

	protected override void Die()
	{
		this.PublishDeath();
	}

	private VegetalPreset VegetalPreset
	{
		get
		{
			return (VegetalPreset)Preset;
		}
	}
}
