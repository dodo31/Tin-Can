using System;
using UnityEngine;
using UnityEngine.UI;

public class VegetalSpawnButton : MonoBehaviour
{
	public EntityType Type;

	public event Action<EntityType, Vector3> OnClick;

	protected void Awake()
	{
		Button button = this.GetComponent<Button>();
		button.onClick.AddListener(() =>
		{
			for (int i = 0; i < 1; i++)
			{
				Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0);
				OnClick?.Invoke(Type, spawnPosition);
			}
		});
	}
}
