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
    public string backScene;

    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene ();
        textCurrentScene.text = scene.name;
        btnBack.onClick.AddListener(() =>
        {
            Debug.Log("back "+backScene);
            SceneManager.LoadScene(backScene);
        });

        btnNext.gameObject.SetActive(!string.IsNullOrEmpty(nextScene)); 
        btnNext.onClick.AddListener(() =>
        {
            Debug.Log("next "+nextScene);
            SceneManager.LoadScene(nextScene);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
