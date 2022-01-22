using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    [System.Serializable]
    public enum DualityProerty
    {
        Empty,
        Life,
        Tempo,
    }

    [SerializeField]
    protected DualityProerty _property;

    protected bool _isBlack = false;

    public Vector2Int pos;

    private List<BlackBlock> _joinBlackBlocks = new List<BlackBlock>();

    public DualityProerty propery { get { return _property; } }

    public bool isBlack { get { return _isBlack; } }

    public void AddJoinBlock(BlackBlock block)
    {
        _joinBlackBlocks.Add(block);
    }

    public void DispJoinBlock(bool display)
    {
        foreach(var block in _joinBlackBlocks)
        {
            block.isDisplay = display;
        }
    }

    public void OnBlockClick()
    {
        GridManager.instance.TriggerRemove(GetComponent<BaseBlock>());

    }

    public void Clear()
    {
        _property = DualityProerty.Empty;
        gameObject.SetActive(false);
    }

}
