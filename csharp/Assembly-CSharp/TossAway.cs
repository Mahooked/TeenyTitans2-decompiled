using UnityEngine;

public class TossAway : MonoBehaviour
{
	public float gravity;

	public float xSpeed;

	public float ySpeed;

	public float rotationRate;

	public float killZone;

	private void Update()
	{
		Vector3 position = transform.position;
		position.x += xSpeed * Time.deltaTime;
		position.y += ySpeed * Time.deltaTime;
		transform.position = position;
		Vector3 eulerAngles = transform.localRotation.eulerAngles;
		eulerAngles.z += rotationRate;
		transform.localRotation = Quaternion.Euler(eulerAngles);
		ySpeed += gravity * Time.deltaTime;
		if (position.y < killZone)
		{
			Object.Destroy(gameObject);
		}
	}

	public void Init(float gravity, float xSpeed, float ySpeed, float rotationRate, float killZone)
	{
		this.gravity = gravity;
		this.xSpeed = xSpeed;
		this.ySpeed = ySpeed;
		this.rotationRate = rotationRate;
		this.killZone = killZone;
	}
}
