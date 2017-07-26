using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirko.HoloToolkitExtensions;

#if WINDOWS_UWP
using HoloToolkit.Unity.InputModule;

public class BoundingMenuClickhandler : MonoBehaviour, IInputClickHandler {
#else
public class BoundingMenuClickhandler : MonoBehaviour
{
#endif

	// Use this for initialization
	void Start () {
		
	}

#if !WINDOWS_UWP
    void OnMouseDown()
    {
        OnSelect();
    }
    
    void OnSelect()
    {
        if (this.gameObject.transform.GetChild(0).name == "UIMovePrefab")
        {
            SpeechCommandExecuter.Move();
        }
        else if (this.gameObject.transform.GetChild(0).name == "UIRotatePrefab")
        {
            SpeechCommandExecuter.Rotate();
        }
        else if (this.gameObject.transform.GetChild(0).name == "UIScalePrefab")
        {
            SpeechCommandExecuter.Scale();
        }
        else if (this.gameObject.transform.GetChild(0).name == "UIRemovePrefab")
        {
            Destroy(this.gameObject.transform.parent.parent.gameObject);
        }
    }
#else
    public virtual void OnInputClicked(InputEventData eventData)
    {
        if (this.gameObject.transform.GetChild(0).name == "UIMovePrefab")
        {
            SpeechCommandExecuter.Move();
        }
        else if (this.gameObject.transform.GetChild(0).name == "UIRotatePrefab")
        {
            SpeechCommandExecuter.Rotate();
        } 
        else if (this.gameObject.transform.GetChild(0).name == "UIScalePrefab")
        {
            SpeechCommandExecuter.Scale();
        }
        else if (this.gameObject.transform.GetChild(0).name == "UIRemovePrefab")
        {
            Destroy(this.gameObject.transform.parent.parent.gameObject);
        }
    }
#endif
}
