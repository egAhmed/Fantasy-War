using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Pathfinding.RVO;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(RVOController))]
[RequireComponent(typeof(FunnelModifier))]
public class RTSGameUnitPathFindingController : MonoBehaviour
{
    public float repathRate = 1;

    private float nextRepath = 0;

    private Vector3 target;
    private bool canSearchAgain = true;

    private RVOController controller;

    Path path = null;

    List<Vector3> vectorPath;
    int wp;

    public float moveNextDist = 1;
    Seeker seeker;

    MeshRenderer[] rends;

    public void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    // Use this for initialization
    public void Start()
    {
        controller = GetComponent<RVOController>();
        SetTarget(transform.position);
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
        RecalculatePath();
    }

    public void RecalculatePath()
    {
        canSearchAgain = false;
        nextRepath = Time.time + repathRate * (Random.value + 0.5f);
        seeker.StartPath(transform.position, target, OnPathComplete);
    }

    public void OnPathComplete(Path _p)
    {
        ABPath p = _p as ABPath;
        //
        canSearchAgain = true;
        //
        if (path != null) path.Release(this);
        path = p;
        p.Claim(this);
        //
        if (p.error)
        {
            wp = 0;
            vectorPath = null;
            return;
        }
        //
        Vector3 p1 = p.originalStartPoint;
        Vector3 p2 = transform.position;
        p1.y = p2.y;
        float d = (p2 - p1).magnitude;
        wp = 0;
        //
        vectorPath = p.vectorPath;
        Vector3 waypoint;
        //
        for (float t = 0; t <= d; t += moveNextDist * 0.6f)
        {
            wp--;
            Vector3 pos = p1 + (p2 - p1) * t;

            do
            {
                wp++;
                waypoint = vectorPath[wp];
                waypoint.y = pos.y;
            } while ((pos - waypoint).sqrMagnitude < moveNextDist * moveNextDist && wp != vectorPath.Count - 1);
        }
    }

    public void Update()
    {
        //
        if (Time.time >= nextRepath && canSearchAgain)
        {
            RecalculatePath();
        }
        //
        Vector3 dir = Vector3.zero;
        Vector3 pos = transform.position;
        //
        if (vectorPath != null && vectorPath.Count != 0)
        {
            Vector3 waypoint = vectorPath[wp];
            waypoint.y = pos.y;

            while ((pos - waypoint).sqrMagnitude < moveNextDist * moveNextDist && wp != vectorPath.Count - 1)
            {
                wp++;
                waypoint = vectorPath[wp];
                waypoint.y = pos.y;
            }

            dir = waypoint - pos;
            float magn = dir.magnitude;
            if (magn > 0)
            {
                float newmagn = Mathf.Min(magn, controller.maxSpeed);
                dir *= newmagn / magn;
            }
            //dir = Vector3.ClampMagnitude (waypoint - pos, 1.0f) * maxSpeed;
        }
        //
        controller.Move(dir);
    }


}
