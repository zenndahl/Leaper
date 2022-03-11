using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    protected float countdown = 5;
    public int points;
    int i = 0;
    //protected float speed = 2;

    protected void Awake()
    {
        EventManager.Instance.onTargetCaptured += Pontuate;
        //EventManager.Instance.onGameOver += Clear;
    }

    protected void Clear()
    {
        Destroy(gameObject);
    }

    protected void OnDestroy()
    {
        EventManager.Instance.onTargetCaptured -= Pontuate;
    }

    // Update is called once per frame
    protected void Update()
    {
        if(gameObject != null)
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0) Destroy(gameObject);
        }
    }

    protected virtual void Pontuate()
    {
        FindObjectOfType<SoundManager>().Play("TargetHit");
        GameManager.Instance.AddPoints(points);
        EventManager.Instance.onTargetCaptured -= Pontuate;
        Destroy(gameObject);
    }
}
