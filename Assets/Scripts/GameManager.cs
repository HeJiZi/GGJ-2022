using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager :MonoBehaviour
{
    public static GameManager instance;
    public List<string> levels;
    public string backScene = "LevelSelect";

    [SerializeField]
    private GameObject _nextLevelUI;

    [SerializeField]
    private GameObject _endGameUI;

    [SerializeField]
    private Text _levelText;

    [SerializeField]
    private AudioSource _audio;


    static GameManager()
    {
        //GameObject go = new GameObject("GameManager");
            //DontDestroyOnLoad(go);
            //instance = go.AddComponent<GameManager>();
    }
    private void Awake()
    {
        Screen.SetResolution(1280, 720, false);
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
        string name = this.GetNextLevel();
        SceneManager.LoadScene(name);
        _levelText.text = name;
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
        levels.Add("Level 1");
        levels.Add("Level 2");
        levels.Add("Level 3");
        levels.Add("Level 4");
        levels.Add("Level 5");
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

    public void PlayTriggerClip()
    {
        _audio.Play();
    }
    public void Init()
    {

    }

}