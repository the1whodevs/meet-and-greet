using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// Make sure one always exists!
public class GravityManager : MonoBehaviour
{
    public static GravityManager Instance { get; private set; }

    // Use this to ensure no null reference!
	public static GravityBody GravityApplier => Instance.gravityApplier ? Instance.gravityApplier : Instance.defaultGravityApplier;

	[SerializeField] private GravityBody gravityApplier;

    // Make sure this is never null!
	[SerializeField] private GravityBody defaultGravityApplier;

	private void Awake() {
		Instance = this;

		if (!defaultGravityApplier) {
			defaultGravityApplier = FindObjectOfType<GravityBody>(true);
        }
	}

    public void ApplyGravityToTarget(Rigidbody target){
		// Make sure the gravityApplier isn't the target!
		if (target.transform == GravityApplier.transform){
			Debug.LogError("Can't apply gravity to self, unless you're trying to create a blackhole or something!");
			return;
		}

		var gravityDir = GravityApplier.transform.position - target.position;
		gravityDir.Normalize();

		target.AddForce(gravityDir * GravityApplier.gravityForce, ForceMode.Impulse);
	}

    public void UpdateGravityApplier(GravityBody newGravityApplier){
        // TODO: Should this call be allowed?
        if (newGravityApplier == gravityApplier) return;

		gravityApplier = newGravityApplier;
	}
}
