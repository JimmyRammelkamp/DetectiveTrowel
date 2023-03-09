using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diorama : MonoBehaviour
{
    [SerializeField] private Transform leftHandOffset;
    [SerializeField] private Transform rightHandOffset;
   
    public Transform GetLeftHandOffset() { return leftHandOffset; }
    public Transform GetRightHandOffset() { return rightHandOffset; }
}
