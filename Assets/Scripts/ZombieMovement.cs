using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	Vector3 moveDirection = Vector3.left;

	private bool moving = true;

	private void OnTriggerEnter(Collider other)
	{
		// stop moving once the zombie reaches the players base
		if(other.CompareTag("Defense"))
		{
			moving = false;
		}
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	private void Update()
	{
		if (moving)
		{
			transform.Translate(moveDirection * Time.deltaTime * moveSpeed);
		}
	}

}
