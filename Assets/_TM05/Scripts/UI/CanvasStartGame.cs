using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasStartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneController.Instance.LoadScene("GameMenu", false, false);
    }
}
