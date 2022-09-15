using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBody : MonoBehaviour
{
    // This is the player's body. Make sure it's never null, 'useGravity' is turned off and 'isKinematic' is turned off!
	[SerializeField] private Rigidbody mainBody;

    private void Awake() {
        if (!mainBody) mainBody = GetComponent<Rigidbody>();

		mainBody.useGravity = false;
		mainBody.isKinematic = false;
	}

    private void FixedUpdate() {
		// Always apply gravity!
		GravityManager.Instance.ApplyGravityToTarget(mainBody);
	}

    /// <summary>
	/// Adds a FORCE of [move] to the player's main body using rigidbody.AddForce(Vector3 force, ForceMode forceMode) function.
	/// </summary>
	/// <param name="move">The force to add.</param>
    public void MoveMainBody(Vector3 move, float maxSpeed, ForceMode forceMode = ForceMode.Impulse){
		mainBody.AddForce(move, forceMode);

		LimitToMaxSpeed(maxSpeed);
	}

	// If this isn't called, the max speed test should fail when using MoveMainBody only to move the player.
	public void LimitToMaxSpeed(float maxSpeed){
		var speed = mainBody.velocity.magnitude;

		if (speed < maxSpeed) return;

		var speedDirection = mainBody.velocity.normalized;
		mainBody.velocity = speedDirection * maxSpeed;
	}

	// For Unit testing
	public Rigidbody GetMainBody() {
		return mainBody;
    }

	private void OnTriggerEnter(Collider other) {
		var gravityBody = other.GetComponentInParent<GravityBody>();
		
		if (gravityBody){
			GravityManager.Instance.UpdateGravityApplier(gravityBody);
			return;
		}
	}
}
