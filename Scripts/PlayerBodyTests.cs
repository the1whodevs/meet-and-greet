using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerBodyTests
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerBodyTestRigidbody()
    {
        // Create a test gravity applier, hopefully for the gravity manager to find
        var testGravityApplier = new GameObject("TEST GRAVITY APPLIER").AddComponent<GravityBody>();
        testGravityApplier.gravityCenter = testGravityApplier.gameObject.transform;
        testGravityApplier.gravityForce = 10;
        testGravityApplier.gravityBodyEnableTrigger = testGravityApplier.gameObject.AddComponent<SphereCollider>();
        testGravityApplier.gravityBodyEnableTrigger.isTrigger = true;

        // Create a test gravity manager, as the player needs it to apply gravity in FixedUpdate
        var gm = new GameObject("TEST GRAVITY MANAGER").AddComponent<GravityManager>();
        gm.UpdateGravityApplier(testGravityApplier);

        // Allow Awake() to run on gm
        yield return null;

        // Create test player.
        var playerBody = new GameObject("TEST PLAYER").AddComponent<PlayerBody>();

        yield return null;

        var playerRb = playerBody.GetMainBody();

        // Make sure playerBody has a rigidbody
        Assert.IsNotNull(playerRb);

        // Make sure playerBody rigidbody doesnt use gravity
        Assert.IsFalse(playerRb.useGravity, "GRAVITY TEST");

        // Make sure playerBody is not kinematic
        Assert.IsFalse(playerRb.isKinematic, "KINEMATIC TEST");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerBodyTestMaxSpeed()
    {
        // Create a test gravity applier, hopefully for the gravity manager to find
        var testGravityApplier = new GameObject("TEST GRAVITY APPLIER").AddComponent<GravityBody>();
        testGravityApplier.gravityCenter = testGravityApplier.gameObject.transform;
        testGravityApplier.gravityForce = 10;
        testGravityApplier.gravityBodyEnableTrigger = testGravityApplier.gameObject.AddComponent<SphereCollider>();
        testGravityApplier.gravityBodyEnableTrigger.isTrigger = true;

        // Create a test gravity manager, as the player needs it to apply gravity in FixedUpdate
        var gm = new GameObject("TEST GRAVITY MANAGER").AddComponent<GravityManager>();
        gm.UpdateGravityApplier(testGravityApplier);

        // Allow Awake() to run on gm
        yield return null;

        // Create test player.
        var playerBody = new GameObject("TEST PLAYER").AddComponent<PlayerBody>();

        yield return null;

        // Make sure max speed is properly enforced
        const float max_speed = 50.0f;
        playerBody.MoveMainBody(new Vector3(0, max_speed * 10, 0), max_speed, ForceMode.VelocityChange);

        // Give enough time to physics to update the rigidbody
        yield return new WaitForSeconds(1.0f);

        Assert.LessOrEqual(playerBody.GetMainBody().velocity.magnitude, max_speed);
    }
}
