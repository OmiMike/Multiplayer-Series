using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    public static IKController instance;

    Animator anim;

    [Header("Right Hand IK")]
    [Range(0, 1)] public float rightHandWeight;
    public Transform rightHandObj = null;
    public Transform rightHandHint = null;

    [Header("Left Hand IK")]
    [Range(0, 1)] public float leftHandWeight;
    public Transform leftHandObj = null;
    public Transform leftHandHint = null;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if (anim)
        {
            #region RIGHT HAND IK

            if (rightHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
                anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            }

            if(rightHandHint != null)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
                anim.SetIKHintPosition(AvatarIKHint.RightElbow, rightHandHint.position);
            }

            #endregion

            #region LEFT HAND IK

            if (leftHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            }

            if (rightHandHint != null)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
                anim.SetIKHintPosition(AvatarIKHint.LeftElbow, leftHandHint.position);
            }

            #endregion
        }
    }
}
