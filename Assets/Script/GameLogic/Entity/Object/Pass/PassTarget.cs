using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassTarget : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PhysicEntity>().enabled = false;
            col.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            TimelineManager.Instance.endTimeline.Play();
            StartCoroutine(WaitForAction(LoadNextScene));
        }
    }
    private void LoadNextScene()=>SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    private IEnumerator WaitForAction(Action myAction)
    {
        yield return new WaitForSeconds(2f);
        myAction.Invoke();
    }
}
