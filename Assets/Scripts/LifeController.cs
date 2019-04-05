using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [HideInInspector] public Cell[][] cells;


    private List<Cell> liveCells;// A list that holds all live to simulate in next Step
    private List<Cell> activeCells;//It used to store all alive cells and cells with neighbours in ongoing step


    private bool isSimulating;
    public float simulationSpeed; //set the simulation speed inspector

    void Start()
    {
        activeCells = new List<Cell>();
        liveCells = new List<Cell>();
        isSimulating = false;
    }
    /*
     *This method simulates the current step/generation
     * It has 2 major sub steps
     */
    private void Step()
    {
        activeCells.Clear();
        /* SUB STEP 1 :
         * loop every cell in livecells and add one point to all its neighbouring cells
         * and store current cell and all its neighbouring cells in active cells
         */

        foreach (Cell cell in liveCells)
        {
            AddToNeighbours(cell);
        }
        /*SUB STEP 2 :
         * 
         * loop activecells and apply "Game of life" condtions to make it alive or dead
         * Store all alive cells new livecells list  * 
         */
        List<Cell> newLiveCells = new List<Cell>();
        foreach (Cell cell in activeCells)
        {
            if (cell.neighboursAlive == 2 )
            {
                if (cell.isAlive)
                {
                    cell.SetState(true);
                    newLiveCells.Add(cell);
                }
                
            }else if (cell.neighboursAlive == 3)
            {
                cell.SetState(true);
                newLiveCells.Add(cell);
            }
            else
            {
                cell.SetState(false);
            }
            cell.neighboursAlive = 0;
        }
        /*
         * replace livecells list with new live cells list
         */
        liveCells = newLiveCells;
    }
    private void AddToNeighbours(Cell cell)
    {
        List<int> xPoints = GetValidPointsOnAxis(cell.gridPosition.x, cells.Length);// neighbour points on x-axis
        List<int> yPoints = GetValidPointsOnAxis(cell.gridPosition.y, cells.Length);// neighbour points on y-axis
        for (int i = 0; i < yPoints.Count; i++)
        {
            for (int j = 0; j < xPoints.Count; j++)
            {
                Cell neighbour = cells[yPoints[i]][xPoints[j]];
                if (cell != neighbour)
                {
                    neighbour.neighboursAlive++;
                }
                if (!activeCells.Contains(neighbour))
                {
                    activeCells.Add(neighbour);
                }
            }
        }
        
    }
    private List<int> GetValidPointsOnAxis(int n, int gridSize)
    {
        List<int> points = new List<int>();
        for (int i = -1; i <= 1; i++)
        {
            if (n + i >= 0 && n + i < gridSize)
            {
                points.Add(n + i);
            }
        }
        return points;
    }
    

    public void OnCellStateChange(Cell cell)
    {
        if (cell.isAlive)
        {
            if (!liveCells.Contains(cell))
            {
                liveCells.Add(cell);
            }
        }else
        {
            if (liveCells.Contains(cell))
            {
                liveCells.Remove(cell);
            }
        }        
    } 

    IEnumerator Simulate()
    {
        while (isSimulating)
        {
            yield return new WaitForSeconds(simulationSpeed);
            Step();
        }       
       
    }

    public void StartSimulate()
    {
        isSimulating = true;
        StartCoroutine(Simulate());
    }
    public void StopSimulate()
    {
        isSimulating = false;
        ResetLife();
    }
    public void ResetLife()
    {
        liveCells.Clear();
        activeCells.Clear();
        int gridSize = cells.Length;
        foreach (Cell[] vCells in cells)
        {
            foreach (Cell hCell in vCells)
            {
                hCell.Reset();
            }
        }
    }
}
