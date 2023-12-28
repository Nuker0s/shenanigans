using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Antenna : MonoBehaviour
{

    public SplineContainer sc;
    public Transform tip; //tip of the antenna (must NOT use base as a parent)
    public Transform tiptarget; //where tip of the antenna should be when stationary
    public float lerpvalue = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        tip.position = Vector3.Lerp(tip.position, tiptarget.position, lerpvalue * Time.deltaTime);
        SplineContainer MySpline = sc;


        var firstKnot = MySpline.Spline.ToArray()[1];

        firstKnot.Position = MySpline.transform.InverseTransformPoint(tip.position);
        //firstKnot.Rotation = Quaternion.Inverse(MySpline.transform.rotation) * KnotTarget.rotation;


        MySpline.Spline.SetKnot(1, firstKnot);
        tip.rotation = firstKnot.Rotation;
        

    }

}
