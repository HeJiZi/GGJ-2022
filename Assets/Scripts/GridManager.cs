using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //private static Grid
    List<List<BaseBlock>> _grids;

    // Start is called before the first frame update
    void Awake()
    {
        InitGrids();
        InitGridRelation(_grids);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitGrids()
    {
        _grids = new List<List<BaseBlock>>();
        foreach (Transform lines in transform)
        {
            var line = new List<BaseBlock>();
            foreach (Transform block in lines)
            {
                line.Add(block.GetComponent<BaseBlock>());
            }
            _grids.Add(line);
        }
    }

    void InitGridRelation(List<List<BaseBlock>> grids)
    {
        int lineCount = grids.Count;
        int colCount = grids[0].Count;

        for(int i = 0; i < lineCount; i++)
        {
            if(grids[i].Count != colCount)
            {
                Debug.Log(string.Format("line{0} 列数量异常", i));
            }
            var line = grids[i];
            for(int j = 0; j < colCount; j++)
            {
                BlackBlock blb =null;
                line[j].gameObject.TryGetComponent(out blb);
                if (blb)
                {
                    if(i - 1 >= 0)
                    {
                        grids[i - 1][j].AddJoinBlock(blb);
                    }
                    if(i + 1 < lineCount)
                    {
                        grids[i + 1][j].AddJoinBlock(blb);
                    }
                    if(j - 1 >= 0)
                    {
                        grids[i][j - 1].AddJoinBlock(blb);
                    }
                    if(j + 1 < colCount)
                    {
                        grids[i][j + 1].AddJoinBlock(blb);
                    }
                }
            }
        }
    }
}
