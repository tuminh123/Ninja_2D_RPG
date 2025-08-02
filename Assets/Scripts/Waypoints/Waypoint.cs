using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Vector3[] points;
    public Vector3[] Points => points;

    public Vector3 entityPosition { get; private set; }

    private bool gameStart;

    private void Start()
    {
        entityPosition = transform.position;
        gameStart = true;
    }

    private void OnDrawGizmos()
    {
        if (gameStart == false && transform.hasChanged)
        {
            entityPosition = transform.position;
        }
    }
    public Vector3 GetPosition(int pointIndex)
    {
        return entityPosition + points[pointIndex];
    }

}
