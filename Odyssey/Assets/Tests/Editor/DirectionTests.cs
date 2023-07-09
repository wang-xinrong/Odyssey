using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DirectionTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void DirectionTestsSimplePasses()
    {


        Assert.AreEqual(Vector2.up, Directions.RightFiveDirections[0]);
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play de. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DirectionTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
