using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCam : MonoBehaviour
{

	public Ball ball;
	public float cameraSensitivity = 90;
	public float climbSpeed = 4;
	public float normalMoveSpeed = 10;
	public float slowMoveFactor = 0.25f;
	public float fastMoveFactor = 3;
	public bool canMove = false;

	private float rotationX = 0.0f;
	private float rotationY = 0.0f;

	void Start()
	{
		Screen.lockCursor = true;

	}

	void Update()
	{

		if (canMove)
		{
			rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
			rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
			rotationY = Mathf.Clamp(rotationY, -90, 90);

			transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
			float camY = transform.position.y;
			float camX = transform.position.x;
			float camZ = transform.position.z;
			if (camY >= 104 && camY <= 160 && camX <= 456 && camX >= 50 && camZ >= 40 && camZ <= 440)
			{
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
					transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
				}
				else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
				{
					transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
					transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
				}
				else
				{
					transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
					transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
				}
				if (Input.GetKey(KeyCode.Q)) { transform.position += transform.up * climbSpeed * Time.deltaTime; }
				if (Input.GetKey(KeyCode.E)) { transform.position -= transform.up * climbSpeed * Time.deltaTime; }
				if (Input.GetKeyDown(KeyCode.End))
				{
					Screen.lockCursor = (Screen.lockCursor == false) ? true : false;
				}
			}
			else if (camY < 104)
			{
				transform.position = new Vector3(transform.position.x, 104, transform.position.z);
			}
			else if (camY > 160)
			{
				transform.position = new Vector3(transform.position.x, 160, transform.position.z);
			}
			else if (camX > 456)
			{
				transform.position = new Vector3(456, transform.position.y, transform.position.z);
			}
			else if (camX < 50)
			{
				transform.position = new Vector3(50, transform.position.y, transform.position.z);
			}
			else if (camZ < 40)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, 40);
			}
			else if (camZ >= 440)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, 440);
			}
		}
	}
}
