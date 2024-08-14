using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class PlacementSystem : MonoBehaviour
{
    public GridData gridData;
    public Grid Grid;
    public Vector2Int GridScale;


    [SerializeField] Vector3 offset;
    [SerializeField] Transform[] points;
    [SerializeField] Transform Container;
    [SerializeField] List<BlockSet> blockSets = new List<BlockSet>();

    [Space(5)]
    [SerializeField] RowColumeBreaker Breaker;

    private BlockSet selectedBlockSet;
    bool Holded;
    private void Start()
    {
        gridData = new GridData(GridScale, offset);
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;



        if (!Holded) PointerChecks();

        if (selectedBlockSet != null)
        {
            if (Input.GetMouseButton(0))
            {
                Holded = true;

                selectedBlockSet.transform.position = GetMouseOnGrid_Clamped();
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (gridData.CanPlaceObejctsAt(selectedBlockSet.blockPoints.ToList(), GetMouseOnGrid_Clamped()))
                {
                    selectedBlockSet.DisPatch(Container);

                    gridData.AddObjectsAt(selectedBlockSet.blockPoints.ToList(), selectedBlockSet.blockPeaces.ToList(), GetMouseOnGrid_Clamped());

                    blockSets.Remove(selectedBlockSet);
                    Destroy(selectedBlockSet.gameObject);

                    Breaker.CalucateBrackables(gridData, GridScale);

                    selectedBlockSet = null;
                }
                else
                {
                    int index = blockSets.IndexOf(selectedBlockSet);

                    selectedBlockSet.transform.position = points[index].position;
                }

                Holded = false;
            }
        }


        if (blockSets.Count == 0)
        {

            foreach (var item in points)
            {
                BlockSet RefferanceSet = ObjectPool.instance.blockSets[Random.Range(0, ObjectPool.instance.blockSets.Length)];
                BlockSet blockSet = Instantiate(RefferanceSet);

                blockSet.Activate();
                blockSet.transform.position = item.position;

                blockSets.Add(blockSet);
            }
        }
        if (blockSets.Count != 0)
        {
            bool b =
                   blockSets.Count > 0 && Breaker.IsPlaceable(blockSets[0], gridData, GridScale) 
                || blockSets.Count > 1 && Breaker.IsPlaceable(blockSets[1], gridData, GridScale) 
                || blockSets.Count > 2 && Breaker.IsPlaceable(blockSets[2], gridData, GridScale);

            print(b);
            ScoreManager.instance.TryEnddingGame(b);

        }
    }

    private void PointerChecks()
    {
        Vector3 pointer = GetMouseOnGrid();
        pointer.z = -10;

        RaycastHit2D hit = Physics2D.Raycast(pointer, Vector3.forward, 25);


        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out selectedBlockSet))
            {

            }
            else
            {
                selectedBlockSet = null;
            }
        }
        else
        {
            selectedBlockSet = null;
        }
    }

    private Vector3 GetMouseOnGrid_Clamped()
    {
        Vector3 mouseGridPos = GetMouseOnGrid() + this.offset;


        Vector3 offsetMax = this.offset + VectorMax(selectedBlockSet.blockPoints.ToList());
        Vector3 offsetMini = this.offset + VectorMini(selectedBlockSet.blockPoints.ToList());

        if (mouseGridPos.x >( GridScale.x / 2) - offsetMax.x)
        {
            mouseGridPos.x = (GridScale.x / 2) - offsetMax.x;
        }
        if (mouseGridPos.x < (-GridScale.x / 2) + offsetMini.x)
        {
            mouseGridPos.x = (-GridScale.x / 2) + offsetMini.x;
        }

        if (mouseGridPos.y > (GridScale.y / 2) - offsetMax.y)
        {
            mouseGridPos.y = (GridScale.y / 2) - offsetMax.y;
        }
        if (mouseGridPos.y < (-GridScale.y / 2) + offsetMini.y)
        {
            mouseGridPos.y = (-GridScale.y / 2) + offsetMini.y;
        }

        return mouseGridPos;
    }
    private Vector3 GetMouseOnGrid()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mouseGridPos = Grid.WorldToCell(mousePos);
        mouseGridPos.z = 0;
        return mouseGridPos;
    }


    private Vector3 VectorMax(List<Vector3> vectors)
    {
        Vector3 _return = Vector3.zero;

        foreach (var item in vectors)
        {
            if (item.x > _return.x)
            {
                _return.x = item.x;
            }
            if (item.y > _return.y)
            {
                _return.y = item.y;
            }
        }


        return _return;
    }  
    private Vector3 VectorMini(List<Vector3> vectors)
    {
        Vector3 _return = Vector3.positiveInfinity;

        foreach (var item in vectors)
        {
            if (item.x < _return.x)
            {
                _return.x = item.x;
            }
            if (item.y < _return.y)
            {
                _return.y = item.y;
            }
        }


        return _return;
    }
}
