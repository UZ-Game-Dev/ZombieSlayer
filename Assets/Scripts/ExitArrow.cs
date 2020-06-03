using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArrow : MonoBehaviour
{
    public Transform[] exits;
    private Transform NearestExit;
    private Vector3 _virtualVector3;
    private bool _firstSearchFixed = false;

    private void Start()
    {
        Transform compass = GameObject.Find("Compass").transform;
        exits = new Transform[compass.childCount];
        for (int i = 0; i < compass.childCount; i++)
            exits[i] = compass.GetChild(i);
    }

    private void FixedUpdate()
    {
        if (_firstSearchFixed)
        {
            _virtualVector3 = Vector3.Lerp(_virtualVector3, NearestExit.transform.position, 8 * Time.deltaTime);

            transform.LookAt(_virtualVector3);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            if (transform.parent.transform.position.x == 0) transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);//linijka naprawiający błąd - kiedy gracz był na współrzędnej x=0, pozycja strzałki odwracały się o 90 stopni, przez co stawała się nie widoczna w kamerze.
        }
    }

    void Update()
    {

        for (int i = 0; i < exits.Length; i++)
        {
            Transform target = exits[i];

            Vector3 dirToTarget = (target.position - transform.position);

            float dstToTarget = Vector3.Distance(transform.position, target.position);
            if (NearestExit == null) NearestExit = exits[0];
            float dstToNearestExit = Vector3.Distance(transform.position, NearestExit.position);

            if (dstToTarget < dstToNearestExit)
            {
                NearestExit = target;
            }
        }
        if (_firstSearchFixed == false)
        {
            _virtualVector3 = NearestExit.transform.position;
            _firstSearchFixed = true;
            transform.LookAt(_virtualVector3);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        //Poniższe linijki to arcydzieło, szanuję xD
        /*if (gameObject.activeSelf)
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
        }*/
    }
}
