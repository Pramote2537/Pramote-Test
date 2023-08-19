using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    public bool IsPlayer;
    bool isDead;
    int hp;
    public int atk_dmg = 10, atk_rang = 2;
    public float speed = 3;
    Vector3 pos;
    Animation animation;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)&&IsPlayer)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                pos = hit.point;
                //transform.position = pos;
                StartCoroutine(move_to(this.gameObject,pos));
            }
        }

        if (Input.GetMouseButtonDown(0) && IsPlayer)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                pos = hit.point;
                //transform.position = pos;
                StartCoroutine(moveTo_Atk(pos));                
            }
        }
    }

    public IEnumerator move_to(GameObject ob, Vector3 destination)
    {
        anim.SetFloat("speed", 1);
        //Vector3 _des = new Vector3(destination.x,) ;
        
        transform.LookAt(destination,Vector3.up);
        float duration = 0.5f * speed;
        var currentPos = ob.transform.localPosition;
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / duration;
        destination = new Vector3(destination.x, destination.y, destination.z);
        while (true)
        {
            percentageComplete += Time.deltaTime / duration;
            ob.transform.localPosition = Vector3.Lerp(currentPos, destination, percentageComplete);
            if (percentageComplete >= 1) break;
            yield return new WaitForFixedUpdate();
        }
        anim.SetFloat("speed",0);
    }


    IEnumerator moveTo_Atk(Vector3 des)
    {
        yield return StartCoroutine(move_to(this.gameObject, pos));
        anim.SetTrigger("attack");

    }
}
