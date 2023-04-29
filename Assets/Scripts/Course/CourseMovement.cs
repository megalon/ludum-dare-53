using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseMovement : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(CourseManager.Instance.Movement * Time.deltaTime);
    }
}
