using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RowColumeBreaker : MonoBehaviour
{
    public bool IsPlaceable(BlockSet blockSet,GridData gridData, Vector2 GridSize)
    {
        for (float i = (-GridSize.x / 2); i < GridSize.x / 2; i++)
        {
            for (float j = (-GridSize.y / 2); j < GridSize.y / 2; j++)
            {
                Vector3 xy = Clamp(new(i, j), GridSize);

                //print(xy.ToString() + ($"  :  {i}"));

                if (gridData.CanPlaceObejctsAt(blockSet.blockPoints.ToList(), xy))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }
    public void CalucateBrackables(GridData gridData, Vector2 GridSize)
    {
        for (float i = (-GridSize.x / 2); i < GridSize.x / 2; i++)
        {
            List<Vector3> row = new List<Vector3>();
            for (float j = (-GridSize.y / 2); j < GridSize.y /2 ; j++)
            {
                Vector3 xy = Clamp(new(i, j), GridSize);


                if (gridData.CanPlaceObejctAt(xy))
                {
                    row.Clear();
                }
                else
                {
                    row.Add(xy);

                    if (row.Count >= GridSize.x)
                    {
                        gridData.RemoveObjectsAt(row);
                        row.Clear();

                        ScoreManager.instance.AddScore();
                    }
                }
            }
        }
        for (float i = (-GridSize.y / 2); i < GridSize.y / 2; i++)
        {
            List<Vector3> col = new List<Vector3>();

            for (float j = (-GridSize.x / 2); j < GridSize.x / 2; j++)
            {
                Vector3 xy = Clamp(new(j, i), GridSize);


                if (gridData.CanPlaceObejctAt(xy))
                {
                    col.Clear();
                }
                else
                {
                    col.Add(xy);

                    if (col.Count >= GridSize.y)
                    {
                        gridData.RemoveObjectsAt(col);
                        col.Clear();

                        ScoreManager.instance.AddScore();
                    }
                }
            }
        }
    }
    public Vector3 Clamp(Vector3 gridPosition, Vector2 GridScale)
    {
        Vector3 offset = Vector2.one / 2;
        Vector3 rt = gridPosition + offset;


        if (rt.x > (GridScale.x / 2) - offset.x)
        {
            rt.x = (GridScale.x / 2) - offset.x;
        }
        if (rt.x < (-GridScale.x / 2) + offset.x)
        {
            rt.x = (-GridScale.x / 2) + offset.x;
        }

        if (rt.y > (GridScale.y / 2) - offset.y)
        {
            rt.y = (GridScale.y / 2) - offset.y;

        }
        if (rt.y < (-GridScale.y / 2) + offset.y)
        {
            rt.y = (-GridScale.y / 2) + offset.y;

        }

        return rt;
    }
}

