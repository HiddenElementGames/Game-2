using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	Vector3 moveDirection = Vector3.left;

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	private void Update()
	{
		transform.Translate(moveDirection *  Time.deltaTime * moveSpeed);
	}

}
