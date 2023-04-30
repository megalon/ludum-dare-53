using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseMovement : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(new Vector3(0, 0, -CourseManager.Instance.Speed) * Time.deltaTime);
    }
}
