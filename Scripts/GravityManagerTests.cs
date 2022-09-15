using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GravityManagerTests
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestGravityManager()
    {
        var gm = new GameObject("TEST GM").AddComponent<GravityManager>();
 
        yield return null;

        Assert.IsNotNull(GravityManager.Instance, "GravityManager Instance is null!");
    }
}
