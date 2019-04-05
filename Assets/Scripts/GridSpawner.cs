using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int gridSize;
    [SerializeField] private Cell cellPrefab;   
    [SerializeField] private LifeController lifeController;   
    void Start()
    {
        SetGrid();
    }

    private void SetGrid()
    {
        float gridHeight = Camera.main.orthographicSize*2;
        float cellSize =  gridHeight / gridSize;
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(Vector2.zero);
        lifeController.cells = new Cell[gridSize][];
        for (int i = 0; i < gridSize; i++)
        {
            lifeController.cells[(int)i] = new Cell[gridSize];
            for (int j = 0; j < gridSize; j++)
            {
                Cell cell = Instantiate(cellPrefab, transform);
                cell.Init(new Vector2Int(j, i), lifeController);
                cell.GetComponent<SpriteRenderer>().size = new Vector2(cellSize, cellSize);
                cell.transform.position = bottomLeft + new Vector2(cellSize * (float)j+ cellSize/2, cellSize * (float)i + cellSize / 2);
                lifeController.cells[i][j] = cell;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
