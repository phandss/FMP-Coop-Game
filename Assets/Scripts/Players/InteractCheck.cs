using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InteractCheck : MonoBehaviour
{
    //private HumanInteraction _interact;
    //private readonly List<IInteractable> _nearbyObjects = new List<IInteractable>();
    //private IInteractable _currentClosest;

    //private void Awake()
    //{
    //    _interact = GetComponentInParent<HumanInteraction>();

    //    Collider col = GetComponent<BoxCollider>();

    //}

    //private void Update()
    //{
    //    UpdateClosest();
    //}

    //private void UpdateClosest()
    //{
    //    IInteractable closest = FindClosest();

    //    if(closest == _currentClosest)
    //    {
    //        return;
    //    }

    //    _currentClosest = closest;
    //    _interact?.SetClosestInteractable(_currentClosest);
    //}

    //private IInteractable FindClosest()
    //{
    //    if(_nearbyObjects.count == 0)
    //    {
    //        return null;
    //    }

    //    IInteractable closest = null;
    //    float closestSqrDist = Mathf.Infinity;
    //    Vector3 currentPos = transform.position;

    //    for (int i = _nearbyObjects.count - 1; i >= 0; i--)
    //    {
    //        IInteractable obj = _nearbyObjects[i];

    //        if(obj == null)
    //        {
    //            _nearbyObjects.RemoveAt(i);
    //            continue;
    //        }

    //        MonoBehaviour mb = obj as MonoBehaviour;
    //        float sqrDist = (mb.transform.position - currentPos).sqrMagnitude;

    //        if (sqrDist < closestSqrDist)
    //        {
    //            closestSqrDist = sqrDist;
    //            closest = obj;
    //        }
    //    }
    //    return closest;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log($"[InteractTrigger] OnTriggerEnter - '{other.name}' entered trigger zone.");

    //    IInteractable inter = other.GetComponentInParent<IInteractable>();
    //    _nearbyObjects.add(inter);
    //    Debug.Log($"object by name of'{other.name}'added to list");
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    IInteractable inter = other.GetComponentInParent<IInteractable>();

    //    _nearbyObjects.remove(inter);
    //    Debug.Log($"object by name of'{other.name}'removed from list");
    //}
}
