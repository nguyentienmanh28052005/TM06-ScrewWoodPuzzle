using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Wood> layer1;
    public List<Wood> layer2;
    public List<Wood> layer3;

    public List<GameObject> screw;
    
    [SerializeField] private BoxCollider2D _myTriggerCollider;

    [SerializeField] private GameObject _vfx;

    private int countWood;
    private int completeCount;

    public Wood currentWood;

    public void Start()
    {
        countWood = layer1.Count + layer2.Count + layer3.Count;
        completeCount = -countWood;
        LevelSetupAsync().Forget();
    }
    
    private async UniTaskVoid LevelSetupAsync()
    {
        if (layer1.Count != 0)
        {
            foreach (var wood in layer1)
            {
                wood.gameObject.SetActive(true);
                wood.Active();
            }
            await UniTask.Delay(350);
        }
        if (layer2.Count != 0)
        {
            foreach (var wood in layer2)
            {
                wood.gameObject.SetActive(true);
                wood.Active();
            }
            await UniTask.Delay(350);
        }
        if (layer3.Count != 0)
        {
            foreach (var wood in layer3)
            {
                wood.gameObject.SetActive(true);
                wood.Active();
            }
            await UniTask.Delay(350);
        }
        foreach (var obj in screw)
        {
            obj.SetActive(true);
        }
        
        if (layer1.Count != 0)
        {
            foreach (var wood in layer1)
            {
                wood.AddCollider();
            }
        }
        if (layer2.Count != 0)
        {
            foreach (var wood in layer2)
            {
                wood.AddCollider();
            }
        }
        if (layer3.Count != 0)
        {
            foreach (var wood in layer3)
            {
                wood.AddCollider();
            }
        }
    }

    public void UpdateCountWood()
    {
        countWood = 0;
        foreach (var wood in layer1)
            if (wood.woodState == Wood.WoodStateEnum.Locked)
                countWood++;
        foreach (var wood in layer2)
            if (wood.woodState == Wood.WoodStateEnum.Locked)
                countWood++;
        foreach (var wood in layer3)
            if (wood.woodState == Wood.WoodStateEnum.Locked)
                countWood++;
        if(countWood == 0) MessageManager.Instance.SendMessage(ManhMessageType.OnComplete);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wood"))
        {
            currentWood = other.GetComponentInParent<Wood>();
            if (currentWood != null)
            {
                currentWood.woodState = Wood.WoodStateEnum.Falling;
                currentWood.gameObject.SetActive(false);
            }
            UpdateCountWood();
            Vector2 otherPosition = other.transform.position;
        
            Vector2 exitPoint = _myTriggerCollider.ClosestPoint(otherPosition); 

            Instantiate(_vfx, exitPoint, Quaternion.identity);
        }
    }
}
