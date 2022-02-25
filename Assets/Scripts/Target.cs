using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float countdown = 5;
    protected float speed = 2;
    public int points;
    protected PlayerController playerController;

    void Start()
    {
        EventManager.Instance.onTargetCaptured += Pontuate;
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnDestroy()
    {
        EventManager.Instance.onTargetCaptured -= Pontuate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0) Destroy(gameObject);
    }

    protected virtual void Pontuate()
    {
        FindObjectOfType<PlayerController>().AddPoints(points);
        EventManager.Instance.onTargetCaptured -= Pontuate;
    }
}
