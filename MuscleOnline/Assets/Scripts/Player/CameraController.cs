using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject aim;
    // Start is called before the first frame update
    void Start()
    {
         this.transform.position = new Vector3(this.transform.position.x, aim.transform.position.y,this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, aim.transform.position.y, this.transform.position.z);
    }
}
