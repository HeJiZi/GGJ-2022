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

    [SerializeField]
    private GameObject _nextLevelUI;

    [SerializeField]
    private GameObject _endGameUI;


    static GameManager()
    {
        //GameObject go = new GameObject("GameManager");
            //DontDestroyOnLoad(go);
            //instance = go.AddComponent<GameManager>();
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
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
        _nextLevelUI.SetActive(false);
        SceneManager.LoadScene(this.GetNextLevel());
    }

    public void ReloadLevel()
    {
        _nextLevelUI.SetActive(false);
        _endGameUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void BackSelect()
    {
        SceneManager.LoadScene(backScene);
    }
 
    void Start () 
    {
        Debug.Log("Start");
        levels = new List<string>();
        levels.Add("Level1");
        levels.Add("Level2");
        levels.Add("Level3");
        levels.Add("Level4");
        levels.Add("Level5");
    }
    
    public void EndLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == levels[levels.Count - 1])
        {
            _endGameUI.SetActive(true);
        }
        else
        {
            _nextLevelUI.SetActive(true);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void Init()
    {

    }

}