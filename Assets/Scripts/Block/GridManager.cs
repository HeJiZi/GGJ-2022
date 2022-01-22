using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance;
    public static GridManager instance { get { return _instance; } }

    List<List<BaseBlock>> _grids;

    private int _blockCount = 0;

    public int blockCount
    {
        get { return _blockCount; }
        set
        {
            _blockCount = value;
            if(value == 0)
            {
                Debug.Log("下一关dd");
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        _blockCount = 0;
        InitGrids();
    }
    private void Start()
    {
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
                var b = block.GetComponent<BaseBlock>();
                if (b.propery!= BaseBlock.DualityProerty.Empty)
                {
                    blockCount++;
                }
                line.Add(b);
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
                line[j].pos = new Vector2Int(i, j);
                if (line[j].isBlack)
                {
                    BlackBlock blb = line[j].gameObject.GetComponent<BlackBlock>();

                    if (i - 1 >= 0)
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


    private BaseBlock _lastSelect;
    public void TriggerRemove(BaseBlock target)
    {
        if (_lastSelect)
        {
            _lastSelect.DispJoinBlock(false);
            if(target.isBlack != _lastSelect.isBlack && target.propery == _lastSelect.propery)
            {
                if(canRemove(_lastSelect, target))
                {
                    var blackBlock = target.isBlack ? target : _lastSelect;
                    blackBlock.DispJoinBlock(true);

                    blockCount -= 2;
                    _lastSelect.Clear();
                    target.Clear();
                }
                _lastSelect = null;
            }
            else
            {
                _lastSelect = target;
                if (!target.isBlack)
                {
                    target.DispJoinBlock(true);
                }
            }
        }
        else
        {
            _lastSelect = target;
            if(!target.isBlack)
            {
                target.DispJoinBlock(true);
            }
        }
    }


 /****
  * 连通性判断
 */
    public bool canRemove(BaseBlock b1, BaseBlock b2)
    {
        HashSet<BaseBlock> HS = new HashSet<BaseBlock>(); // 0拐点水平直接相连
        HashSet<BaseBlock> VS = new HashSet<BaseBlock>(); // 0拐点垂直直接相连
        HashSet<BaseBlock> L1HS = new HashSet<BaseBlock>(); // 1拐点水平直接相连
        HashSet<BaseBlock> L1VS = new HashSet<BaseBlock>(); // 1拐点垂直直接相连

        if (FindStraightJoinNodes(HS, VS, b1, b2))
        {
            return true;
        }
        if (FindNextJoinNodesV(VS, L1HS, b2))
        {
            return true;
        }
        if (FindNextJoinNodesH(HS, L1VS, b2))
        {
            return true;
        }

        if (FindNextJoinNodesV(L1VS, new HashSet<BaseBlock>(), b2))
        {
            return true;
        }
        if (FindNextJoinNodesH(L1HS, new HashSet<BaseBlock>(), b2))
        {
            return true;
        }

        return false;
    }

    //垂直结点的下一个拐点必然是水平的
    public bool FindNextJoinNodesV(HashSet<BaseBlock> VS, HashSet<BaseBlock> NextSet, BaseBlock target)
    {
        int cols = _grids[0].Count;
        foreach (var block in VS)
        {
            int x = block.pos.x;
            int y = block.pos.y;
            for(int j = y - 1; j >=0 ; j--)
            {
                var gridBlock = _grids[x][j];
                if (gridBlock.propery == BaseBlock.DualityProerty.Empty && !NextSet.Contains(gridBlock))
                {
                    NextSet.Add(gridBlock);
                }
                if (x == target.pos.x && j == target.pos.y)
                {
                    return true;
                }

                if (gridBlock.propery != BaseBlock.DualityProerty.Empty)
                {
                    break;
                }
            }
            for (int j = y + 1; j < cols; j++)
            {
                var gridBlock = _grids[x][j];
                if (gridBlock.propery == BaseBlock.DualityProerty.Empty && !NextSet.Contains(gridBlock))
                {
                    NextSet.Add(gridBlock);
                }
                if (x == target.pos.x && j == target.pos.y)
                {
                    return true;
                }

                if (gridBlock.propery != BaseBlock.DualityProerty.Empty)
                {
                    break;
                }
            }
        }
        return false;
    }
    public bool FindNextJoinNodesH(HashSet<BaseBlock> HS, HashSet<BaseBlock> NextSet, BaseBlock target)
    {
        int lines = _grids.Count;
        foreach (var block in HS)
        {
            int x = block.pos.x;
            int y = block.pos.y;
            for (int i = x - 1; i >= 0; i--)
            {
                var gridBlock = _grids[i][y];
                if (gridBlock.propery == BaseBlock.DualityProerty.Empty && !NextSet.Contains(gridBlock))
                {
                    NextSet.Add(gridBlock);
                }
                if (i == target.pos.x && y == target.pos.y)
                {
                    return true;
                }

                if (gridBlock.propery != BaseBlock.DualityProerty.Empty)
                {
                    break;
                }
            }
            for (int i = x + 1; i < lines; i++)
            {
                var gridBlock = _grids[i][y];
                if (gridBlock.propery == BaseBlock.DualityProerty.Empty && !NextSet.Contains(gridBlock))
                {
                    NextSet.Add(gridBlock);
                }
                if (i == target.pos.x && y == target.pos.y)
                {
                    return true;
                }

                if (gridBlock.propery != BaseBlock.DualityProerty.Empty)
                {
                    break;
                }
            }
        }
        return false;
    }

    public bool FindStraightJoinNodes(HashSet<BaseBlock> HS, HashSet<BaseBlock> VS, BaseBlock b1, BaseBlock b2)
    {
        int cols = _grids[0].Count;
        int lines = _grids.Count;
        int x = b1.pos.x;
        int y = b1.pos.y;
        for (int i = x - 1; i >= 0; i--)
        {
            var gridBlock = _grids[i][y];
            if (gridBlock.propery == BaseBlock.DualityProerty.Empty && !VS.Contains(gridBlock))
            {
                VS.Add(gridBlock);
            }
            if (i == b2.pos.x && y == b2.pos.y)
            {
                return true;
            }

            if(gridBlock.propery != BaseBlock.DualityProerty.Empty)
            {
                break;
            }
        }
        for (int i = x + 1; i < lines; i++)
        {
            var gridBlock = _grids[i][y];
            if (gridBlock.propery == BaseBlock.DualityProerty.Empty && !VS.Contains(gridBlock))
            {
                VS.Add(gridBlock);
            }
            if (i == b2.pos.x && y == b2.pos.y)
            {
                return true;
            }
            if (gridBlock.propery != BaseBlock.DualityProerty.Empty)
            {
                break;
            }
        }

        for (int j = y - 1; j >= 0; j--)
        {
            var gridBlock = _grids[x][j];
            if (gridBlock.propery == BaseBlock.DualityProerty.Empty && !HS.Contains(gridBlock))
            {
                HS.Add(gridBlock);
            }
            if (x == b2.pos.x && j == b2.pos.y)
            {
                return true;
            }
            if (gridBlock.propery != BaseBlock.DualityProerty.Empty)
            {
                break;
            }
        }
        for (int j = y + 1; j < cols; j++)
        {
            var gridBlock = _grids[x][j];
            if (gridBlock.propery == BaseBlock.DualityProerty.Empty && !HS.Contains(gridBlock))
            {
                HS.Add(gridBlock);
            }

            if (x == b2.pos.x && j == b2.pos.y)
            {
                return true;
            }
            if (gridBlock.propery != BaseBlock.DualityProerty.Empty)
            {
                break;
            }
        }
        return false;
    }
}
