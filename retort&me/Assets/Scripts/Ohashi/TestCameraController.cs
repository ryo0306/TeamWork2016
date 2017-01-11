using UnityEngine;
using System.Collections;

public class TestCameraController : MonoBehaviour
{

	[SerializeField]
	Camera camera;

	[SerializeField]
	float mov;

	[SerializeField]
	float scale;

	void Update()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.position += Vector3.up * mov;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.position += Vector3.down * mov;
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position += Vector3.left * mov;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += Vector3.right * mov;
		}

		if (0.01f < Input.GetAxis("Mouse ScrollWheel") )
		{
			camera.orthographicSize += scale;
		}
		if (-0.01f > Input.GetAxis("Mouse ScrollWheel"))
		{
			camera.orthographicSize -= scale;
		}

	}
}
