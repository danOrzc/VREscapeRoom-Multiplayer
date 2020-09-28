using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.XR.Interaction.Toolkit;

public class RealtimeGrab : MonoBehaviour
{

    private RealtimeTransform _realtimeTransform;
    private XRGrabInteractable _xRGrabInteractable;

    private void Awake()
    {
        _realtimeTransform = GetComponent<RealtimeTransform>();
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_xRGrabInteractable.isSelected)
        {
            _realtimeTransform.RequestOwnership();
        }
    }
}
