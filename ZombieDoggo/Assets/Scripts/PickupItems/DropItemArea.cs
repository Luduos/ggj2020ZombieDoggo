﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using de.crystalmesh;

[RequireComponent(typeof(Collider))]
public class DropItemArea : MonoBehaviour
{
    [SerializeField]
    private Transform ZombieRootNode = null;
    [SerializeField]
    private Transform HatPosition = null;

    private ItemAttachmentPoint[] attachmentPoints;

    private PickupItem attachedHat = null;

    private void Awake()
    {
        attachmentPoints = ZombieRootNode.GetComponentsInChildren<ItemAttachmentPoint>();

    }

    private void OnTriggerEnter(Collider other)
    {
        PickupItem item = other.GetComponent<PickupItem>();
        if(null != item && item.IsCarried)
        {
            Hat hat = other.GetComponent<Hat>();
            if (null != hat)
                AttachHat(item);
            else
                AttachItem(item);
        }
    }

    private void AttachHat(PickupItem hat)
    {
        if(null == attachedHat)
        {
            attachedHat = hat;
            hat.AttachToZombie(HatPosition);
        }
        
    }

    public void DettachHat()
    {
        if(null != attachedHat)
        {
            attachedHat.DestroyItself();
            attachedHat = null;
        }
    }

    public void AttachItem(PickupItem item)
    {
        ItemAttachmentPoint attachmentPoint = FindRandomFreeAttachmentPoint();
        if (null != attachmentPoint)
        {
            attachmentPoint.AttachItem(item);
        }
    }

    public ItemAttachmentPoint GetRandomUsedAttachmentPoint()
    {
        List<ItemAttachmentPoint> used = new List<ItemAttachmentPoint>();
        foreach (ItemAttachmentPoint attachmentPoint in attachmentPoints)
        {
            if (attachmentPoint.HasAttachedItem)
                used.Add(attachmentPoint);
        }
        return Utilities.RandomFromList(used);
    }

    public PickupItem GetRandomAttachedItem()
    {
        PickupItem[] attachedItems = ZombieRootNode.GetComponentsInChildren<PickupItem>();
        return Utilities.RandomFromArray(attachedItems);
    }

    public ItemAttachmentPoint FindRandomFreeAttachmentPoint()
    {
        List<ItemAttachmentPoint> freeAttachmentPoints = new List<ItemAttachmentPoint>();
        foreach(ItemAttachmentPoint attachmentPoint in attachmentPoints)
        {
            if (!attachmentPoint.HasAttachedItem)
                freeAttachmentPoints.Add(attachmentPoint);
        }
         return Utilities.RandomFromList(freeAttachmentPoints);
    }
}
