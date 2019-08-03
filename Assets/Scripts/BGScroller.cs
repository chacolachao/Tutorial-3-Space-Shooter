using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ;
    private Vector3 startPosition;

    private float scrollPosition;
    void Start()
    {
        startPosition = transform.position;
        scrollPosition = 0;
    }

    void Update()
    {
        scrollPosition = Mathf.Repeat (scrollPosition + Time.deltaTime * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * scrollPosition;
    }
}
