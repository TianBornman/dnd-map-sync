using TMPro;
using Unity.Netcode;
using UnityEngine;

[GenerateSerializationForType(typeof(string))]
public class Entity : NetworkBehaviour
{
	public TextMeshProUGUI nameText;

	#region Properties

	private string entityName;

	[SerializeField]
	public string EntityName
	{
		get { return entityName; }
		set
		{
			entityName = value;
			nameText.text = entityName;

			if (IsServer)
				SetClientNameClientRpc(entityName);
		}
	}

	#endregion

	[ClientRpc]
	public void SetClientNameClientRpc(string name)
	{
		if (IsServer)
			return;

		EntityName = name;
	}
}
