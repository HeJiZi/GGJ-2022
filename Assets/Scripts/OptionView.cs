using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionView : MonoBehaviour
{
    public Button btnBack;
    public Button btnNext;
    public Text textCurrentScene;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene ();
        textCurrentScene.text = scene.name;
        btnBack.onClick.AddListener(() =>
        {
            GameManager.instance.BackSelect();
        });

        btnNext.gameObject.SetActive(!string.IsNullOrEmpty(GameManager.instance.GetNextLevel())); 
        btnNext.onClick.AddListener(() =>
        {
            Debug.Log("next ");
            GameManager.instance.NextLevel();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
