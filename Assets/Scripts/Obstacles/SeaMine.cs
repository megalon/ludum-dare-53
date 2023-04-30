using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMine : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SeaMine entered trigger area!");
        if (other.tag.Equals("RiseUpTrigger"))
        {
            Debug.Log("RiseUpTrigger!");
            _animator.SetTrigger("RiseUp");
        }
    }
}
