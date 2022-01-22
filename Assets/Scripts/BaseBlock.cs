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

    private List<BlackBlock> _joinBlackBlocks = new List<BlackBlock>();

    public DualityProerty propery { get { return _property; } }

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

}
