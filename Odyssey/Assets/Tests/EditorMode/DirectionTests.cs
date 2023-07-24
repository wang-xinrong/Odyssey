using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DirectionTests
{
    // Right five directions
    [Test]
    public void RightFiveDirectionsNorth()
    {
        Assert.AreEqual(Vector2.up, Directions.RightFiveDirections[0]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void RightFiveDirectionsNortheast()
    {
        Assert.AreEqual(new Vector2(1, 1).normalized
            , Directions.RightFiveDirections[1]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void RightFiveDirectionsEast()
    {
        Assert.AreEqual(Vector2.right
            , Directions.RightFiveDirections[2]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void RightFiveDirectionsSoutheast()
    {
        Assert.AreEqual(new Vector2(1, -1).normalized
            , Directions.RightFiveDirections[3]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void RightFiveDirectionsSouth()
    {
        Assert.AreEqual(Vector2.down
            , Directions.RightFiveDirections[4]);
        // Use the Assert class to test conditions
    }




    // Left five directions
    [Test]
    public void LeftFiveDirectionsNorth()
    {
        Assert.AreEqual(Vector2.up, Directions.LeftFiveDirections[0]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void LeftFiveDirectionsNorthwest()
    {
        Assert.AreEqual(new Vector2(-1, 1).normalized
            , Directions.LeftFiveDirections[1]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void LeftFiveDirectionsWest()
    {
        Assert.AreEqual(Vector2.left
            , Directions.LeftFiveDirections[2]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void LeftFiveDirectionsSouthwest()
    {
        Assert.AreEqual(new Vector2(-1, -1).normalized
            , Directions.LeftFiveDirections[3]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void LeftFiveDirectionsSouth()
    {
        Assert.AreEqual(Vector2.down
            , Directions.LeftFiveDirections[4]);
        // Use the Assert class to test conditions
    }




    // Eight direction tests
    [Test]
    public void EightDirectionsWest()
    {
        Assert.AreEqual(Vector2.left, Directions.EightDirections[0]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void EightDirectionsNorthwest()
    {
        Assert.AreEqual(new Vector2(-1, 1).normalized
            , Directions.EightDirections[1]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void EightDirectionsUp()
    {
        Assert.AreEqual(Vector2.up, Directions.EightDirections[2]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void EightDirectionsNortheast()
    {
        Assert.AreEqual(new Vector2(1, 1).normalized
            , Directions.EightDirections[3]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void EightDirectionsEast()
    {
        Assert.AreEqual(Vector2.right, Directions.EightDirections[4]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void EightDirectionsSoutheast()
    {
        Assert.AreEqual(new Vector2(1, -1).normalized
            , Directions.EightDirections[5]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void EightDirectionsSouth()
    {
        Assert.AreEqual(Vector2.down, Directions.EightDirections[6]);
        // Use the Assert class to test conditions
    }

    [Test]
    public void EightDirectionsSouthwest()
    {
        Assert.AreEqual(new Vector2(-1, -1).normalized
            , Directions.EightDirections[7]);
        // Use the Assert class to test conditions
    }
}
