using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera scratchCamera;
    public Camera gasCamera;

    //Call to switch to scratch cards.
    public void ShowScratchCamera()
    {
        gasCamera.enabled = false;
        scratchCamera.enabled = true;
    }

    //Call to switch to gas station
    public void ShowGasCam()
    {
        gasCamera.enabled = true;
        scratchCamera.enabled = false;
    }
}
