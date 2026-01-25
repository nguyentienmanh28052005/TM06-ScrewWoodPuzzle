using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;


public class CanvasGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseImage;
    [SerializeField] private GameObject pausePanel;
    
    [SerializeField] private GameObject completeParticle;
    [SerializeField] private GameObject completeImage;
    [SerializeField] private GameObject completePanel;
    
    public void RestartGame()
    {
        SceneController.Instance.LoadScene("Level" + GameManager.Instance.level, false, false);
    }

    public void OutLevel()
    {
        SceneController.Instance.LoadScene("GameMenu", false, false);
    }
    

    public void OnEnable()
    {
        MessageManager.Instance.AddSubscriber(ManhMessageType.OnComplete, Complete);
        //MessageManager.Instance.AddSubscriber(ManhMessageType.OnUnScrewTool, UnScrewTool);
    }
    
    public void OnDisable()
    {
        MessageManager.Instance.RemoveSubscriber(ManhMessageType.OnComplete, Complete);
        //MessageManager.Instance.RemoveSubscriber(ManhMessageType.OnUnScrewTool, UnScrewTool);
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        pauseImage.transform.DOScale(1.1f, 0.2f).OnComplete(() =>
        {
            pauseImage.transform.DOScale(1, 0.1f);
        });
    }

    public void ClosePause()
    {
        //pausePanel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        pauseImage.transform.DOScale(1.1f, 0.15f).OnComplete(() =>
        {
            pauseImage.transform.DOScale(0.1f, 0.2f).OnComplete(() =>
            {
                pausePanel.SetActive(false);
            });
        });
    }
    
    public void Complete(object data)
    {
        completePanel.SetActive(true);
        completeParticle.SetActive(true);
        completeImage.transform.DOScale(1.1f, 0.2f).OnComplete(() =>
        {
            completeImage.transform.DOScale(1, 0.1f);
        });
    }

    public void UnScrewTool()
    {
        if (GameManager.Instance.currentScrew != null)
        {
            if (!GameManager.Instance.currentScrew.screwed)
            {
                GameManager.Instance.currentScrew.SetStatusScrew(true, 0.2f);
            }
        }
        
        GameManager.Instance.gameState = GameManager.GameState.UnScrew;
        GameManager.Instance.unScrewToolPanel.SetActive(true);
    }
    
}
