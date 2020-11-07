using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchOff : MonoBehaviour
{
    public GameObject maskPrefab;
    private bool isPressed = false;
    

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 2;

        if (isPressed == true)
        {
            GameObject maskSprite = Instantiate(maskPrefab, mousePos, Quaternion.identity);
            maskSprite.transform.parent = gameObject.transform;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Invoke("reveal", 10);
            isPressed = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isPressed = false;
        }
        
    }

    void reveal()
    {
        Destroy(this.gameObject);
    }
}
