using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject area;
    public float range;

    void Update(){
        if (this.transform.position.y <= -1f){
            this.transform.position = new Vector3(Random.Range(-range, range), 2f, Random.Range(-range, range)) + area.transform.position;
        }
    }
    // Start is called before the first frame update
    public void Eaten(){
        this.transform.position = new Vector3(Random.Range(-range, range), 2f, Random.Range(-range, range)) + area.transform.position;
    }

}
