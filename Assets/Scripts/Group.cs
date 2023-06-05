using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    // Time since last gravity tick
    private float _lastFall = 0;

    private void Start()
    {
        // Default position not valid? Then it's game over
        if (!IsValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        ManageInput();
    }

    private void ManageInput()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if it's valid
            if (IsValidGridPos())
                // It's valid. Update grid.
                UpdateGrid();
            else
                // Its not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }
        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if valid
            if (IsValidGridPos())
                // It's valid. Update grid.
                UpdateGrid();
            else
                // It's not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }
        // Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            // See if valid
            if (IsValidGridPos())
                // It's valid. Update grid.
                UpdateGrid();
            else
                // It's not valid. revert.
                transform.Rotate(0, 0, 90);
        }

        // Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (IsValidGridPos())
            {
                // It's valid. Update grid.
                UpdateGrid();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Map.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;
            }
        }
        // Move Downwards and Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                 Time.time - _lastFall >= 1)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (IsValidGridPos())
            {
                // It's valid. Update grid.
                UpdateGrid();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Map.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;
            }

            _lastFall = Time.time;
        }
    }

    private bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Map.roundVec2(child.position);

            // Not inside Border?
            if (!Map.isInsideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Map.grid[(int) v.x, (int) v.y] != null &&
                Map.grid[(int) v.x, (int) v.y].parent != transform)
                return false;
        }

        return true;
    }

    private void UpdateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Map.h; ++y)
        for (int x = 0; x < Map.w; ++x)
            if (Map.grid[x, y] != null)
                if (Map.grid[x, y].parent == transform)
                    Map.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Map.roundVec2(child.position);
            Map.grid[(int) v.x, (int) v.y] = child;
        }
    }
}