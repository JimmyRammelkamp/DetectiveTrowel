using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diorama : MonoBehaviour
{
    [SerializeField] private Transform leftHandOffset;
    [SerializeField] private Transform rightHandOffset;

    private void Start()
    {
        StartCoroutine(DisableDioramas());
    } 

    IEnumerator DisableDioramas()
    {
        yield return new WaitForSeconds(0.1f);
        if (transform.parent.name == "Dioramas") gameObject.SetActive(false);
    }

    public Transform GetLeftHandOffset() { return leftHandOffset; }
    public Transform GetRightHandOffset() { return rightHandOffset; }
}
