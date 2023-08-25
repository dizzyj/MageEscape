using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum directions{
    forward,
    back,
    left,
    right
}
public class slidebox : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public bool playerTouch, blocked, triggered;
    public directions dir;
    public GameObject op, ui;
    Transform parent;
    

    void Start()
    {
        anim = transform.parent.gameObject.GetComponent<Animator>();
        parent = transform.parent;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTouch = true;
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().DisplayE();
        }
        else
        {
            triggered = true;
            op.GetComponent<slidebox>().WallHit();
        }
    }
    private void Update()
    {
        if (playerTouch && Input.GetKeyDown(KeyCode.E) && !blocked)
        {
            switch (dir)
            {
                case directions.forward:
                    //anim.SetBool("forward", true);
                    Vector3 target = new Vector3(parent.position.x + 1.97f, parent.position.y, parent.position.z);
                    StartCoroutine(moveBox(target));
                    break;
                case directions.back:
                    target = new Vector3(parent.position.x - 1.97f, parent.position.y, parent.position.z);
                    StartCoroutine(moveBox(target));
                    break;
                case directions.left:
                    target = new Vector3(parent.position.x, parent.position.y, parent.position.z + 1.97f);
                    StartCoroutine(moveBox(target));
                    break;
                case directions.right:
                    target = new Vector3(parent.position.x, parent.position.y, parent.position.z - 1.97f);
                    StartCoroutine(moveBox(target));
                    break;
            }
            blocked = true;
        }
    }
    IEnumerator moveBox(Vector3 target)
    {
        Vector3 startPos;
        float elapsedTime, t;
        float desiredDuration = 1f;
        elapsedTime = 0f;
        startPos = parent.position;
        while (elapsedTime < desiredDuration)
        {
            if (op.GetComponent<slidebox>().triggered)
            {
                StopAllCoroutines();
            }
            t = elapsedTime / desiredDuration;
            parent.position = Vector3.Lerp(startPos, target, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        blocked = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTouch = false;
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().HideE();
        }
        else
        {
            op.GetComponent<slidebox>().blocked = false;
            triggered = false;
        }
    }
    public void WallHit()
    {
        StopAllCoroutines();
        op.GetComponent<slidebox>().blocked = true;
    }

}
