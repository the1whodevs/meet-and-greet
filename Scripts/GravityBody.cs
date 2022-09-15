using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A body (i.e. a planet) that applies gravity to objects when it's the active gravityApplier in GravityManager.
/// </summary>
public class GravityBody : MonoBehaviour
{
    /// <summary>
	/// This points to the gravityCenter of the body, and not (necessarily) to the transform of the gameObject that has this component.
	/// </summary>
	public new Transform transform => gravityCenter;

    // Make sure this is never 0!
	public float gravityForce = 10.0f;

    // Make sure this is not null!
	public Transform gravityCenter;

    // Make sure this is not null (do we really care?)
	public Collider gravityBodyEnableTrigger;
}
