using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MousePos = Input.mousePosition;
        Vector3 calculatedScreenPos = Camera.main.ScreenToWorldPoint(new Vector3(MousePos.x, MousePos.y, transform.position.z - Camera.main.transform.position.z));
        if (Time.timeScale == 0.0f) return;
        float z = Mathf.Atan2((calculatedScreenPos.y - transform.position.y),
            (calculatedScreenPos.x - transform.position.x)) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.RotateTowards(
            this.transform.rotation, 
            Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, z), 
            Time.deltaTime * 360.0f);
    }
}
