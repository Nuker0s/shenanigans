using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Antenna : MonoBehaviour
{

    public SplineContainer sc;
    public Transform tip; //tip of the antenna (must NOT use base as a parent)
    public Transform tiptarget; //where tip of the antenna should be when stationary
    public Rigidbody tiprb;
    public float force = 100f;
    public float verticalspeedmult = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        var endknot = sc.Spline.ToArray()[1];
        endknot.Position = sc.transform.InverseTransformPoint(tip.position);
        sc.Spline.SetKnot(1, endknot);

    }
    private void FixedUpdate()
    {
        Vector3 dir = tiptarget.position - tip.position;
        tiprb.AddForce(new Vector3(dir.x, dir.y * verticalspeedmult, dir.z) * force);
    }

}
