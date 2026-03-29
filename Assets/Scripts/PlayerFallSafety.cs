using UnityEngine;

public class PlayerFallSafety : MonoBehaviour
{
    [SerializeField] private Vector3 resetPosition;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Hi");
		other.transform.parent.position = resetPosition;
	}
}
