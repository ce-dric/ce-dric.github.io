using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	private Animator anim;
	private CharacterController controller;

	public float turnSpeed = 3.0f;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	void Update()
	{
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		if (movement != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
			anim.SetInteger("AnimationPar", 1);
		}
		else
			anim.SetInteger("AnimationPar", 0);

		transform.Translate(movement * turnSpeed * Time.deltaTime, Space.World);
		
	}
}
