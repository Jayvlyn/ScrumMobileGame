using System.Collections;
using UnityEngine;

// Should be placed on an individual flipper
public class FlipperController : MonoBehaviour
{
	[SerializeField] private float flipSpeed = 1000;
    [Tooltip("Key to control this flipper")]
    [SerializeField] private KeyCode activateKey = KeyCode.A;
	[SerializeField] private HingeJoint2D hinge;

	private void Start()
	{
		Deactivate();
		hinge.useMotor = true;
	}

	private void Update()
	{
		if (Input.GetKeyDown(activateKey))
		{
			Activate();
		}
		else if (Input.GetKeyUp(activateKey))
		{
			Deactivate();
		}
	}

	/// <summary>
	/// Activates individual flipper, hook up to button taps
	/// </summary>
	public void Activate()
    {
		SetMotorSpeed(-flipSpeed);
    }

	/// <summary>
	/// Deactivates flipper, causing it to rotate back to default state
	/// </summary>
	public void Deactivate()
	{
		SetMotorSpeed(flipSpeed);
	}

	public void SetMotorSpeed(float speed)
	{
		JointMotor2D newMotor = new JointMotor2D();
		newMotor.maxMotorTorque = 10000;
		newMotor.motorSpeed = speed;
		hinge.motor = newMotor;
	}

    
}
