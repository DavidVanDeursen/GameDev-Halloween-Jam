
using System.Collections;
using UnityEngine;

public class StaticController : MonoBehaviour
{
    Vector3 TargetScale;
    float time = 0;
    public float TimeLimit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetScale = transform.localScale;
        transform.localScale = Vector3.zero;
        StartCoroutine(Grow());
    }

    // Update is called once per frame
    void Update()
    {
        if(time > TimeLimit)
        {
            time = 0;
            StartCoroutine(Fade());
        }
        else
        {
            time += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        Debug.Log(other.gameObject.GetComponent<IRewindable>());
        other.gameObject.GetComponent<IRewindable>()?.StartRewind();
    }

    IEnumerator Grow()
    {
        for (int i = 0; i < 100; i++) {
            transform.localScale += TargetScale/100;
            yield return null;
        }
    }

    IEnumerator Fade()
    {
        for (int i = 0; i < 20; i++)
        {
            if(i == 19) Destroy(transform.gameObject);
            transform.localScale -= TargetScale / 20;
            yield return null;
        }
        
    }
}
