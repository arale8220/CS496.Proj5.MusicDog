using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class MainmenuBack : MonoBehaviour {

    public GameObject sqr;
    private Vector3 pos,pos1;
    private float delay = 2;

    private void Start()
    {
        pos = sqr.transform.position;
        pos1 = new Vector3(26.8f,1.775763f,-12.08406f);
    }

    public void FrontSquir()
    {
        sqr.GetComponent<NavMeshAgent>().SetDestination(pos);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);
        sqr.GetComponent<Animation>().Play("stand");
    }

    public void BackSquir()
    {
        sqr.GetComponent<NavMeshAgent>().SetDestination(pos1);
        sqr.GetComponent<Animation>().Play("run");
    }

}
