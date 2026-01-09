using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameMenu : MonoBehaviour
{
    public List<GameObject> levels;
    public float maxLevel;
    void Start()
    {
        maxLevel = GameManager.Instance.data.Value(1);
        for(int i = 1; i <= maxLevel; i++)
        {
            levels[i].GetComponent<Button>().interactable = true;
        }
    }
    
    void Update()
    {
        
    }
}
