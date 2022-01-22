using System.Collections.Generic;
using UnityEngine;

public class LevelSelectView : MonoBehaviour
{
    public RectTransform root;
    public List<string> levelNames;

    public GameObject elementPrefab;
    // Start is called before the first frame update
    void Start()
    {
        levelNames = GameManager.instance.levels;
        for (int i = levelNames.Count-1; i >= 0; i--)
        {
            var go = Instantiate(elementPrefab, root, true); 
            var element = go.GetComponent<LevelSelectElement>();
            element.Refresh(levelNames[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
