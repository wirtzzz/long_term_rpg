using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Vector3 initialPosition;

    private void OnEnable()
    {
        initialPosition = this.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.transform.position = initialPosition;
        this.gameObject.SetActive(false);
    }

    public IEnumerator LaunchDestroyCounter()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.transform.position = initialPosition;
        this.gameObject.SetActive(false);
    }
}
