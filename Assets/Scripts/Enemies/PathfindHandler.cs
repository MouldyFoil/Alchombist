using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindHandler : MonoBehaviour
{
    AIPath path;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        path.maxSpeed = moveSpeed;
        path.destination = target.position;
    }
    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
