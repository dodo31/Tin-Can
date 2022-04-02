using System;
using UnityEngine;
using UnityEngine.UI;

public class VegetalSpawnButton : MonoBehaviour
{
	public EntityType Type;

	public event Action<EntityType> OnClick;

	protected void Awake()
	{
		Button button = this.GetComponent<Button>();
		button.onClick.AddListener(() => { OnClick?.Invoke(Type); });
	}
}
