using UnityEngine;

public abstract class EntitiesFactory : MonoBehaviour
{
	protected EntityPresetBase _presetBase;

	protected virtual void Awake()
	{
		_presetBase = EntityPresetBase.GetInstance();
		_presetBase.CreatePresets();
	}
}