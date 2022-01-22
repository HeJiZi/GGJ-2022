using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager :MonoBehaviour
{
    public static GameManager instance;
    public List<string> levels;
    public string backScene = "LevelSelect";
 
    static GameManager()
    {
        GameObject go = new GameObject("GameManager");
        DontDestroyOnLoad(go);
        instance = go.AddComponent<GameManager>();
    }
    
    public string GetNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene ();
        for (int i = 0; i < levels.Count-1; i++)
        {
            if (scene.name == levels[i])
            {
                return levels[i + 1];
            }
        }

        return "";
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(this.GetNextLevel());
    }
    
    public void BackSelect()
    {
        SceneManager.LoadScene(backScene);
    }
 
    void Start () 
    {
        Debug.Log("Start");
        levels = new List<string>();
        levels.Add("1");
        levels.Add("2");
        
    }
 
}