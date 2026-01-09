using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ScrewNut : MonoBehaviour, IPointerDownHandler
{
    public bool empty = true;
    public CircleCollider2D collier;
    public void Start()
    {
        collier = GetComponent<CircleCollider2D>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (empty && GameManager.Instance.currentScrew != null && !GameManager.Instance.currentScrew.screwed  && !GameManager.Instance.busy)
        {
            if (GameManager.Instance.currentScrewNut != this)
            {
                UndoManager.Instance.RecordMove(
                    GameManager.Instance.currentScrew, 
                    GameManager.Instance.currentScrewNut, 
                    this     
                );
                
                GameManager.Instance.busy = true;
                GameManager.Instance.currentScrewNut.empty = true;
                GameManager.Instance.currentScrewNut.collier.isTrigger = true;
                GameManager.Instance.currentScrewNut = this;
                GameManager.Instance.currentScrew.transform.DOMove(transform.position, 0.15f).OnComplete(() =>
                {
                    GameManager.Instance.currentScrew.collier.isTrigger = false;
                    GameManager.Instance.currentScrewNut.empty = false;
                    GameManager.Instance.currentScrew.transform.SetParent(GameManager.Instance.currentScrewNut.transform);
                    GameManager.Instance.currentScrew.transform.DOScale(1f, 0.15f);
                    GameManager.Instance.currentScrew.transform.DORotate(new Vector3(0f, 0f, 0f), 0.15f).OnComplete(() =>
                    {
                        GameManager.Instance.currentScrew.particalEffect.Clear();
                        GameManager.Instance.currentScrew.particalEffect.Play();
                        GameManager.Instance.currentScrewNut.empty = false;
                        GameManager.Instance.currentScrewNut.collier.isTrigger = true;
                        collier.isTrigger = false;
                        GameManager.Instance.currentScrew = null;
                        GameManager.Instance.busy = false;
                    });
                });
            }
        }
    }
    
    
}
