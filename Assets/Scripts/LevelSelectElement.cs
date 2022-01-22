using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectElement : MonoBehaviour
{
    public Button button;
    public Text text;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Refresh(string sceneName)
    {
        this.sceneName = sceneName;
        text.text = sceneName;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { SceneManager.LoadScene(sceneName); });
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
