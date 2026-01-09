using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckScrewNutEmptyOut : MonoBehaviour
{
    private ScrewNut _screwNut;

    public void Start()
    {
        _screwNut = GetComponentInParent<ScrewNut>();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wood"))
        {
            _screwNut.empty = true;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wood"))
        {
            _screwNut.empty = false;
        }
    }
}
