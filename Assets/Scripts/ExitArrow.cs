using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArrow : MonoBehaviour
{
    public Transform[] exits;
    private Transform NearestExit;
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (exits.Length == 1)
            {
                NearestExit = exits[0];

                transform.LookAt(NearestExit);
                transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            }
	    else if (exits.Length == 3)
            {
                float N = Vector2.Distance(gameObject.transform.position, exits[0].position);
                float E = Vector2.Distance(gameObject.transform.position, exits[1].position);
                float S = Vector2.Distance(gameObject.transform.position, exits[2].position);
 
                if (N < E && N < S) { NearestExit = exits[0]; }
                if (E < N && E < S) { NearestExit = exits[1]; }
                if (S < E && S < N) { NearestExit = exits[2]; }
 
                transform.LookAt(NearestExit);
                transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            }
            else
            {
                float N = Vector2.Distance(gameObject.transform.position, exits[0].position);
                float E = Vector2.Distance(gameObject.transform.position, exits[1].position);
                float S = Vector2.Distance(gameObject.transform.position, exits[2].position);
                float W = Vector2.Distance(gameObject.transform.position, exits[3].position);

                if (N < E && N < S && N < W) { NearestExit = exits[0]; }
                if (E < N && E < S && E < W) { NearestExit = exits[1]; }
                if (S < E && S < N && S < W) { NearestExit = exits[2]; }
                if (W < E && W < S && W < N) { NearestExit = exits[3]; }

                transform.LookAt(NearestExit);
                transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            }
        }
    }
}
