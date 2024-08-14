using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	#region Instance
	public static ObjectPool instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public BlockPeace blockPeacePrefab;
    public BlockSet[] blockSets;

    public Color[] blockColors;
    
}
