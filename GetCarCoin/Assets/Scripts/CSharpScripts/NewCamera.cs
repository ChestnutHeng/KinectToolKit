using UnityEngine;
using System.Collections;

public class NewCamera : MonoBehaviour
{
	public Transform target;
	public float distance = 10;
	
	public float xSpeed = 250;
	public float ySpeed = 120;
	
	public float yMinLimit = -20;
	public float yMaxLimit = 80;
	
	private float x = 0;
	private float y = 0;
	
	void Start ()
	{
		var angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	void LateUpdate ()
	{
		if (target)
		{
			x += Time.deltaTime * Input.GetAxis("Mouse X") * xSpeed;
			y -= Time.deltaTime * Input.GetAxis("Mouse Y") * ySpeed;
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.EulerAngles(y * Mathf.Deg2Rad, x * Mathf.Deg2Rad, 0), Time.deltaTime * 5);
			transform.position = transform.rotation * new Vector3(0, 0, -distance) + target.position;
		}
	}
	
	float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
