using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Room room;

    [System.Serializable]

    public struct Grid
    {
        public int columns, rows;

        public float verticalOffset, horizontalOffset;
    }

    public Grid grid;

    public GameObject gridTile;

    public int WallThickness;

    public List<Vector2> availablePoints = new List<Vector2>();

    private void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.width - WallThickness * 2;
        grid.rows = room.height - WallThickness * 2;
        GenerateGrid();
    }

    public Collider2D[] CollidingTerrain = new Collider2D[0];
    public ContactFilter2D ContactFilter2D;
    public Vector2 SizeOfGridDetector = new Vector2(1, 1);
    private int tempTerrainDetectionResult = 0;

    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;

        for (int m = 0; m < grid.rows; m++)
        {
            for (int n = 0; n < grid.columns; n++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.transform.position = new Vector3(n - (grid.columns - grid.horizontalOffset)
                    , m - (grid.rows - grid.verticalOffset)
                    , 0);
                go.name = "X: " + n + ", Y " + m;

                // this step ought to be improved to exclude undesirable locations
                tempTerrainDetectionResult = Physics2D.OverlapBox(go.transform.position
                    , SizeOfGridDetector
                    , 0f
                    , ContactFilter2D
                    , CollidingTerrain);

                // the spot would only be available if there is no terrain on it
                if (tempTerrainDetectionResult == 0) availablePoints.Add(go.transform.position);
            }
        }

        // and spawn objects here after the grid generation is completed;
        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();

        // register all enemies spawned and deactivate them
        room.RegisterExistingEnemies();
        room.ActivateAllEnemies(false);

        // after the object spawning is completed, the grid can now be disabled
        gameObject.SetActive(false);
    }

}
