using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive;
    public Vector2Int gridPosition;    
    public int neighboursAlive;
    private LifeController lifeController;

    
    public void Init(Vector2Int gp,LifeController lc)
    {
        gridPosition = gp;
        lifeController = lc;
        neighboursAlive  = 0;
        Kill();
    }

    private void Kill()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        isAlive = false;

    }

    private void Birth()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        isAlive = true;
    }
    
    
    public void SetState(bool state)
    {
        if (isAlive!= state)
        {
            if (state)
            {
                Birth();
            }
            else
            {
                Kill();
            }
        }
        
    }

    private void OnMouseDown()
    {
        SetState(!isAlive);
        lifeController.OnCellStateChange(this);
    }
    public void Reset()
    {
        SetState(false);
        neighboursAlive = 0;
    }
}
