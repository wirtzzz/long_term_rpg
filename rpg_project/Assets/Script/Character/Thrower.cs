using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public int NumberOfObject = 20;
    private List<GameObject> Throwables = new List<GameObject>();
    private int currentThrowable = 0;
    // Start is called before the first frame update
    void Start()
    {
        Throwables = CreatePool(NumberOfObject);

        StartCoroutine(LaunchThrowing());
    }

    private List<GameObject> CreatePool(int number)
    {
        List<GameObject> temp_Throwables = new List<GameObject>();

        for (int i = 0; i < number; i++)
        {
            temp_Throwables.Add(CreateRandomObject());
        }
        return temp_Throwables;
    }

    private GameObject CreateRandomObject()
    {
        int randomNb = Random.Range(0, 3);
        GameObject tmp_obj;

        switch (randomNb)
        {
            case 0:
                tmp_obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case 1:
                tmp_obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            case 2:
                tmp_obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                break;
            case 3:
                tmp_obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                break;

            default:
                Debug.LogError("Mauvais nombre mon poussin");
                tmp_obj = new GameObject();
                break;
        }
        tmp_obj.transform.SetParent(this.transform);
        tmp_obj.transform.position = this.transform.position;
        tmp_obj.AddComponent<Rigidbody>();
        tmp_obj.AddComponent<Throwable>();
        tmp_obj.SetActive(false);

        return tmp_obj;
    }

    private void ThrowObject()
    {

        Throwables[currentThrowable].SetActive(true);
        Throwables[currentThrowable].GetComponent<Rigidbody>().velocity = Vector3.zero;
        Throwables[currentThrowable].GetComponent<Rigidbody>().AddForce(this.transform.forward * 2f);
        StartCoroutine(Throwables[currentThrowable].GetComponent<Throwable>().LaunchDestroyCounter());


        //Set next throwable
        if (currentThrowable == Throwables.Count - 1)
        {
            currentThrowable = 0;
        }
        else
        {
            currentThrowable++;
        }
    }

    IEnumerator LaunchThrowing()
    {
        while (true)
        {
            ThrowObject();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
