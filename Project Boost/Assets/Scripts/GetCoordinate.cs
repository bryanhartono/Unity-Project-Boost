using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoordinate : MonoBehaviour
{
    [SerializeField] GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(GetXCoordinate(target), transform.position.y, transform.position.z);
    }

    float GetXCoordinate(GameObject obj)
    {
        float val = obj.transform.position.x;
        // Debug.Log(val);
        return val;
    }
}
