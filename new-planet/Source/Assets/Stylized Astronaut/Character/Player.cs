using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Player : MonoBehaviour
{

	public bool is_not_mobile = false;

	private Animator anim;
	private CharacterController controller;
	private Vector3 dest;
	private float destinationDistance;

	public float turnSpeed = 3.0f;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
		dest = transform.position;
	}

	void Update()
	{
		//if (is_not_mobile)
		if (!Application.isMobilePlatform)
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
		else
        {
			destinationDistance = Vector3.Distance(dest, transform.position);
			
			if (destinationDistance < .05f)
            {
				turnSpeed = 0;
				anim.SetInteger("AnimationPar", 0);
			}
			else if (destinationDistance > .05f)
            {
				turnSpeed = 3;
				anim.SetInteger("AnimationPar", 1);
			}

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Input.GetMouseButtonDown(0))
			{
				if (Physics.Raycast(ray, out hit))
				{
					dest = hit.point;
					Quaternion targetRotation = Quaternion.LookRotation(hit.point - transform.position);
					transform.rotation = targetRotation;
				}
			}

			if (destinationDistance > .05f)
				transform.position = Vector3.MoveTowards(transform.position, dest, turnSpeed * Time.deltaTime);
        }
	}
}
