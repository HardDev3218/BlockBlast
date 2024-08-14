using System;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;




[Serializable]
public class GridData
{
     public List<Data> placedObjects = new();


    [Serializable]
    public class Data
    {
        public Vector3 blockPos;
        public BlockPeace block;

        public Data(Vector3 blockPos, BlockPeace block) 
        {
            this.blockPos = blockPos;
            this.block = block;
        }
    }

    Vector2Int GridScale;
    Vector3 offset;
    public GridData(Vector2Int GridScale, Vector3 offset) 
    {
        this.GridScale = GridScale; 
        this.offset = offset; 
    }


    #region AddObjectAt
    public void AddObjectsAt(List<Vector3> gridPositions_Local, List<BlockPeace> blockPeaces, Vector3 gridPosition_world)
    {
        if (gridPositions_Local.Count != blockPeaces.Count)
            throw new Exception($"List does not have the number of value {gridPositions_Local}&&{blockPeaces}");

        for (int i = 0; i < gridPositions_Local.Count; i++)
        {
            AddObjectAt(gridPositions_Local[i] + gridPosition_world, blockPeaces[i]);
        }
    }

    public void AddObjectAt(Vector3 gridPosition, BlockPeace blockPeace)
    {
        if (CanPlaceObejctAt(gridPosition))
        {
            placedObjects.Add(new(gridPosition, blockPeace));
        }
        else
        {
            throw new Exception($"List already contains this cell position {gridPosition}&&{blockPeace}");
        }
    }
    #endregion

    #region CanPlaceObejctAt
    public bool CanPlaceObejctsAt(List<Vector3> gridPositions_Local, Vector3 gridPosition_world)
    {
        foreach (var gridPosition in gridPositions_Local)
        {
            if (!CanPlaceObejctAt(gridPosition + gridPosition_world))
            {
                return false;
            }
        }
        return true;
    }
    public bool CanPlaceObejctAt(Vector3 gridPosition)
    {

        if (gridPosition.x > (GridScale.x / 2) - offset.x)
        {
            return false;
        }
        if (gridPosition.x < (-GridScale.x / 2) + offset.x)
        {
            return false;
        }

        if (gridPosition.y > (GridScale.y / 2) - offset.y)
        {
            return false;
        }
        if (gridPosition.y < (-GridScale.y / 2) + offset.y)
        {
            return false;
        }

        if (placedObjects.Any(b => b.blockPos == gridPosition))
            return false;
        return true;
    }


    #endregion

    public void RemoveObjectsAt(List<Vector3> gridPositions)
    {
        foreach (var item in gridPositions)
        {
            RemoveObjectAt(item);

        }
    }   
    public void RemoveObjectAt(Vector3 gridPosition)
    {
        placedObjects.Find(b => b.blockPos == gridPosition).block.DestroySelf();
        placedObjects.Remove(placedObjects.Find(b => b.blockPos == gridPosition));
    }
}

