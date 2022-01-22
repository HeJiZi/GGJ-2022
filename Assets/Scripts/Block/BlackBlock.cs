using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackBlock : BaseBlock
{
    [SerializeField]
    private bool _isDisplay;

    private GameObject _propertyImg;

    public bool isDisplay 
    { 
        get 
        { return _isDisplay; } 
        set 
        {
            _isDisplay = value;
            _propertyImg.SetActive(value);
        } 
    }

    private void Awake()
    {
        _isBlack = true;
        _propertyImg = transform.GetChild(0).gameObject;
        isDisplay = false;
    }
}
