using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MapTransform
{
    public Transform vrTarget;
    public Transform IKTarget;

    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void MapVRAvatar()
    {
        IKTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        IKTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class AvatarController : MonoBehaviour
{
    public MapTransform head;
    public MapTransform leftHand;
    public MapTransform rightHand;

    [SerializeField] private float turnSmoothness;

    public Transform IKHead;

    public Vector3 headBodyOffset;


    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - IKHead.position;
    }

    void LateUpdate()
    {
        transform.position = IKHead.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(IKHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);
       // transform.forward = Vector3.ProjectOnPlane(IKHead.up, Vector3.up).normalized;

        head.MapVRAvatar();
        leftHand.MapVRAvatar();
        rightHand.MapVRAvatar();
    }
}