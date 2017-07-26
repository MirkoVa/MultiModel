using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAndRotManager : MonoBehaviour {

    public Color selectorColor = new Color32(98, 99, 255, 100);
    public Color scaleColor = new Color32(98, 159, 255, 255);

    public float handleScale = 0.4f;
    public PrimitiveType handleType;

    GameObject selectorBox;
    GameObject scaleHandleBox;

    public static Material selectorMat;
    public static Material scaleMat;

    public static PrimitiveType myHandleType;
    public static float myHandleScale;

    void Start()
    {
        myHandleType = handleType;
        myHandleScale = handleScale;

        selectorBox = GameObject.Find("SelectorMaterial");
        scaleHandleBox = GameObject.Find("ScaleMaterial");

        selectorMat = selectorBox.GetComponent<Renderer>().material;
        selectorMat.color = selectorColor;

        scaleMat = scaleHandleBox.GetComponent<Renderer>().material;
        scaleMat.color = scaleColor;
    }

    void FindAllGameObjects()
    {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if(go.GetComponent<MeshFilter>() != null && go.tag == "Editable")
            {
                if(go.GetComponent<SelectForBoundingBox>() == null)
                {
                    go.AddComponent<SelectForBoundingBox>();
                    go.tag = "Bounding Box";
                    Debug.Log("Added BB script");
                }
            } else if (go.tag == "Bounding Menu")
            {
                if (go.GetComponent<BoundingMenuClickhandler>() == null)
                {
                    go.AddComponent<BoundingMenuClickhandler>();
                    go.tag = "Bounding Menu Done";
                }
            }
        }
    }

    /*private void FixedUpdate()
    {
        FindAllGameObjects();
    }*/
}
