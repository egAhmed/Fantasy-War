using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Pathfinding.RVO;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(RVOController))]
[RequireComponent(typeof(FunnelModifier))]
public class RTSGameUnitPathFindingController : AIPath
{
    private float nextRepath = 1.0f;
    Vector3 targetPos;
    List<Vector3> vectorPath;
    int wp;
    public float moveNextDist = 1;
    MeshRenderer[] rends;
    protected override void Start()
    {
        base.Start();
        //
        SetTarget(GetFeetPosition());
        repathRate = 2f;
    }

    public void SetTarget(Vector3 target)
    {
       this.targetPos = target;
       //
        RecalculatePath();
        // SearchPath();
    }

    public void RecalculatePath()
    {
       canSearchAgain = false;
       nextRepath = Time.time + repathRate * (Random.value + 0.5f);
        ShouldIdle = false;
        //
       seeker.StartPath(GetFeetPosition(),targetPos, OnPathComplete);
       //
    }
    //
    public override void OnPathComplete(Path _p)
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
       Vector3 p2 = GetFeetPosition();
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

    public override void Update()
    {
       //
       if (Time.time >= nextRepath && canSearchAgain)
       {
           RecalculatePath();
       }
       //
       Vector3 dir = Vector3.zero;
       Vector3 pos = GetFeetPosition();
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
               float newmagn = Mathf.Min(magn, rvoController.maxSpeed);
               dir *= newmagn / magn;
           }
       }
       //
        RotateTowards(targetPos-pos);
       //
       rvoController.Move(dir);
    }

    const float ReachedDistance = 1f;

    public bool ShouldIdle
    {
        get;
        set;
    }

    /** Minimum velocity for moving */
    public float sleepVelocity = 0.4F;

    /** Point for the last spawn of #endOfPathEffect */
    protected Vector3 lastTarget;

    /**
     * Called when the end of path has been reached.
     * An effect (#endOfPathEffect) is spawned when this function is called
     * However, since paths are recalculated quite often, we only spawn the effect
     * when the current position is some distance away from the previous spawn-point
    */
    public override void OnTargetReached()
    {
        base.OnTargetReached();
        //
        Debug.LogError("Fuck here");
        //
        Vector3 pos = GetFeetPosition();
        //
        if (Vector3.Distance(pos, targetPos) <= ReachedDistance)
        {
            //
            ShouldIdle = true;
            //
        }
        //
        // if (Vector3.Distance(pos, lastTarget) > ReachedDistance)
        // {
        //     lastTarget = pos;
        // }
        //
    }

    /** Requests a path to the target */
    // public override void SearchPath()
    // {
    //     lastRepath = Time.time;
    //     canSearchAgain = false;
    //     ShouldIdle = false;
    //     seeker.StartPath(GetFeetPosition(), targetPos);
    // }

    // public void SetTarget(Vector3 pos)
    // {
    //     targetPos = pos;
    //     SearchPath();
    // }

    // protected new void Update()
    // {
    //     //Get velocity in world-space
    //     Vector3 velocity;
    //     if (canMove)
    //     {
    //         //Calculate desired velocity
    //         Vector3 dir = CalculateVelocity(GetFeetPosition());
    //         //
    //         //Rotate towards targetDirection (filled in by CalculateVelocity)
    //         RotateTowards(targetDirection);
    //         //
    //         dir.y = 0;
    //         if (dir.sqrMagnitude > sleepVelocity * sleepVelocity)
    //         {
    //             //If the velocity is large enough, move
    //         }
    //         else
    //         {
    //             //Otherwise, just stand still (this ensures gravity is applied)
    //             dir = Vector3.zero;
    //         }
    //         //
    //         if (rvoController != null)
    //         {
    //             rvoController.Move(dir);
    //             velocity = rvoController.velocity;
    //         }
    //         else if (controller != null)
    //         {
    //             controller.SimpleMove(dir);
    //             velocity = controller.velocity;
    //         }
    //         else
    //         {
    //             Debug.LogWarning("No NavmeshController or CharacterController attached to GameObject");
    //             velocity = Vector3.zero;
    //         }
    //     }
    //     else
    //     {
    //         velocity = Vector3.zero;
    //     }
    // }
}

