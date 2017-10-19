using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour {

    public delegate bool delAIBuild(Vector3 pos, string path);
    //AI建造委托
    public delAIBuild DelAIBuild;
    protected virtual void Initialize() { }
    protected virtual void FSMUpdate() { }
    protected virtual void FSMFixedUpdate() { }

    void Start()
    {
        Initialize();
    }
    
    void Update()
    {
        FSMUpdate();
    }

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}
