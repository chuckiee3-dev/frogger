using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDangerTrigger : MonoBehaviour
{
    private Collider2D _collider2D;
    private float _enableDelay = 2f;
    private WaitForSeconds _enableWait;
    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _enableWait = new WaitForSeconds(_enableDelay);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("a");
        if (other.CompareTag("Danger"))
        {Debug.Log("b");
            _collider2D.enabled = false;
            StartCoroutine(EnableTriggerWithDelay());
            GameActions.PlayerLoseLife();
        }
    }

    private IEnumerator EnableTriggerWithDelay()
    {
        yield return _enableWait;
        _collider2D.enabled = true;
    }
}
