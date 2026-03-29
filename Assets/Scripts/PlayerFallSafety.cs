using UnityEngine;

public class PlayerFallSafety : MonoBehaviour
{
	[SerializeField] private GameObject loseScreen;

	private void OnTriggerEnter(Collider other)
	{
		loseScreen.SetActive(true);
	}
}
