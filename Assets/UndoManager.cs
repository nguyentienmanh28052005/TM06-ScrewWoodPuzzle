using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public interface ICommand
{
    void Undo();
}



[System.Serializable]
public struct WoodState
{
    public Wood woodScript;
    public Wood.WoodStateEnum woodStateEnum;
    public Vector3 position;
    public Quaternion rotation;
    public Vector2 velocity;
    public float angularVelocity;
    public float gravityScale;
    public bool isHitActive;
}

public class MoveScrewCommand : ICommand
{
    private Screw _screw;
    private ScrewNut _oldNut;
    private ScrewNut _newNut;
    private List<WoodState> _woodStates;

    public MoveScrewCommand(Screw screw, ScrewNut oldNut, ScrewNut newNut)
    {
        _screw = screw;
        _oldNut = oldNut;
        _newNut = newNut;
        _woodStates = new List<WoodState>();
        
        var allWoods = GameObject.FindObjectsOfType<Wood>();
        foreach (var wood in allWoods)
        {
            Rigidbody2D rb = wood.GetComponent<Rigidbody2D>();
            _woodStates.Add(new WoodState
            {
                woodScript = wood,
                woodStateEnum = wood.woodState,
                position = wood.transform.position,
                rotation = wood.transform.rotation,
                velocity = rb.velocity,
                angularVelocity = rb.angularVelocity,
                gravityScale = rb.gravityScale,
                isHitActive = wood.hit.activeSelf
            });
        }
    }

    public void Undo()
    {
        if(_screw.gameObject.activeSelf == false) _screw.gameObject.SetActive(true);
        _screw.transform.DOKill();

        _screw.transform.SetParent(_oldNut.transform);
        _screw.transform.position = _oldNut.transform.position;
        _screw.transform.rotation = Quaternion.identity;
        _screw.transform.localScale = Vector3.one;
        
        _screw.screwed = true; 
        _screw.collier.isTrigger = false;
        
        _oldNut.empty = false;
        _oldNut.collier.isTrigger = true;

        _newNut.empty = true;
        _newNut.collier.isTrigger = true;
        
        foreach (var state in _woodStates)
        {
            Wood wood = state.woodScript;
            Rigidbody2D rb = wood.GetComponent<Rigidbody2D>();

            if (wood.woodState == Wood.WoodStateEnum.Falling && state.woodStateEnum == Wood.WoodStateEnum.Locked)
            {
                wood.gameObject.SetActive(true);
                wood.woodState = Wood.WoodStateEnum.Locked;
            }
            
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.isKinematic = true; 
            
            wood.transform.position = state.position;
            wood.transform.rotation = state.rotation;
            
            rb.gravityScale = state.gravityScale;
            wood.hit.SetActive(state.isHitActive);
            
            rb.isKinematic = false;
            
            rb.velocity = state.velocity;
            rb.angularVelocity = state.angularVelocity;
        }
    }
}

public class UndoManager : MonoBehaviour
{
    public static UndoManager Instance;
    private Stack<ICommand> _history = new Stack<ICommand>();

    private void Awake()
    {
        Instance = this;
    }

    public void Undo()
    {
        if (GameManager.Instance.busy) return;
        
        if (_history.Count > 0)
        {
            ICommand cmd = _history.Pop();
            cmd.Undo();
        }
        else
        {
            Debug.Log("Hết lượt!");
        }
    }
    
    public void RecordMove(Screw screw, ScrewNut oldNut, ScrewNut newNut)
    {
        ICommand cmd = new MoveScrewCommand(screw, oldNut, newNut);
        _history.Push(cmd);
    }

    
}