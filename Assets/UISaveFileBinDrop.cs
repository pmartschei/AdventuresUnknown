using AdventuresUnknown;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISaveFileBinDrop : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        UISaveFileDisplay sfd = eventData.pointerDrag.GetComponent<UISaveFileDisplay>();
        if (sfd == null) return;
        sfd.FileObject.Delete();
        eventData.Use();
    }

}
