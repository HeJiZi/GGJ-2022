using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionView : MonoBehaviour
{
    public Button btnBack;

    public string backScene;
    // Start is called before the first frame update
    void Start()
    {
        btnBack.onClick.AddListener(() =>
        {
            Debug.LogError(backScene);
            SceneManager.LoadScene(backScene);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
