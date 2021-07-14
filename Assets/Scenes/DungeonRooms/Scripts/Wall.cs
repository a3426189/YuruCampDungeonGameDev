using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    [SerializeField]
    private GameObject wall_Left;
    [SerializeField]
    private GameObject wall_Right;
    [SerializeField]
    private GameObject wall_Up;
    [SerializeField]
    private GameObject wall_Down;

    private void Awake()
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        wall_Left.transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, minScreenBounds.x, minScreenBounds.x),
            Mathf.Clamp(transform.position.y, minScreenBounds.y, maxScreenBounds.y)
            , transform.position.z
            );

        wall_Right.transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, maxScreenBounds.x, maxScreenBounds.x),
            Mathf.Clamp(transform.position.y, minScreenBounds.y, maxScreenBounds.y)
            , transform.position.z
            );

        wall_Down.transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, minScreenBounds.x, minScreenBounds.x),
            Mathf.Clamp(transform.position.y, minScreenBounds.y, minScreenBounds.y)
            , transform.position.z
            );
        wall_Up.transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, minScreenBounds.x, minScreenBounds.x),
            Mathf.Clamp(transform.position.y, maxScreenBounds.y, maxScreenBounds.y)
            , transform.position.z
            );
    }

}
