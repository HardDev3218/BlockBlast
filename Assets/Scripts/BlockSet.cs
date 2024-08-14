using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSet : MonoBehaviour
{
    public Vector3[] blockPoints;
    public List<BlockPeace> blockPeaces = new List<BlockPeace>();



    public void Activate()
    {
        SpawnBlocks();
        gameObject.SetActive(true);
    }

    public void DisPatch(Transform parent)
    {
        for (int i = 0; i < blockPeaces.Count; ++i)
        {
            blockPeaces[i].transform.parent = parent;
        }
    }


    private void SpawnBlocks()
    {
        Color c = ObjectPool.instance.blockColors[Random.Range(0, ObjectPool.instance.blockColors.Length)];
        foreach (var point in blockPoints)
        {
            BlockPeace spawn = Instantiate(ObjectPool.instance.blockPeacePrefab);
            spawn.transform.parent = transform;
            spawn.transform.localPosition = point;
            spawn.SetColor(c);
            spawn.gameObject.SetActive(true);

            blockPeaces.Add(spawn);
        }
    }
}
