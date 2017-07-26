using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirko.HoloToolkitExtensions;


public class SelectForBoundingBox : MonoBehaviour {
    // Use public class SelectForBoundingBox : MonoBehaviour, IInputClickHandler { this for initialization
    private GameObject sarObject;
    Renderer rend;
    Bounds sarBounds;
    public GameObject menu;
    public Material sarSelectorMat;
    public Material sarScaleMat;
    public GameObject menuPrefab;
    public GameObject holoCamera;
    public Vector2 closestFace;
    public int closestFaceRotateY = 0;

    //GameObject menu;
    GameObject move;
    GameObject rotate;
    GameObject scale;

    public bool isDrawn = false;

	void Start () {
        sarBounds = new Bounds();
    }

    public void DrawSelectionBox()
    {
        // Create Box
        GameObject sarObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Set position to objects position TODO: pre-rotated objects not boxed correctly
        sarObject.transform.position = gameObject.transform.position;
        //sarObject.transform.localRotation = gameObject.transform.localRotation;

        //sarObject.GetComponent<MeshFilter>().mesh.bounds = gameObject.GetComponent<MeshFilter>().mesh.bounds;

        // Destroy collider of bounding box
        Destroy(sarObject.GetComponent<Collider>());

        // Reference to Box Renderer
        //rend = sarObject.GetComponent<Renderer>();

        // Reference to object bounds
        if (this.gameObject.GetComponent<MeshCollider>())
        {
            sarBounds = this.gameObject.GetComponent<MeshCollider>().bounds;
        }
        else if (this.gameObject.GetComponent<SphereCollider>())
        {
            sarBounds = this.gameObject.GetComponent<SphereCollider>().bounds;
        }
        else if (this.gameObject.GetComponent<BoxCollider>())
        {
            sarBounds = this.gameObject.GetComponent<BoxCollider>().bounds;
        }
        else if (this.gameObject.GetComponent<CapsuleCollider>())
        {
            sarBounds = this.gameObject.GetComponent<CapsuleCollider>().bounds;
        }
        else
        {
            Debug.Assert(true, "Could not find collider of selected object");
        }

        // Force to FindFace 1 time, its variables are needed
        FindFace();
        // Position the menu
        Vector3 menuPos = new Vector3(closestFace.x, sarBounds.min.y, closestFace.y);

        // Create HoloMenu
        CreateMenu(menuPos);

        // Create and apply material to the boxes
        //rend.material = sarSelectorMat;

        // Set boxes parent to this object
        //sarObject.transform.SetParent(this.gameObject.transform);

        Destroy(sarObject);

    }

    void CreateEndPoints(Vector3 position)
    {
        PrimitiveType pt = ScaleAndRotManager.myHandleType;
        GameObject handle = GameObject.CreatePrimitive(pt);
        Destroy(handle.GetComponent<Collider>());
        float hScale = ScaleAndRotManager.myHandleScale;
        handle.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f) * hScale;
        handle.transform.position = position;
        handle.transform.SetParent(this.transform);
        handle.tag = "Bounding Box";
        handle.GetComponent<Renderer>().material = sarScaleMat;

    }

    void CreateMenu(Vector3 postition)
    {
        //Create the menu overlay
        menu = Instantiate(menuPrefab);

        //Posistion the menu overlay
        menu.transform.Rotate(0, menu.transform.localRotation.y - closestFaceRotateY, 0);
        menu.transform.position = postition;
        menu.transform.parent = this.transform;
        menu.transform.name = "Menu";
    }

    void FindFace()
    {
        // Init the 4 faces of the object
        Vector2 frontFace = new Vector2(sarBounds.center.x, sarBounds.center.z - sarBounds.extents.z - 0.1f );
        Vector2 backFace = new Vector2(sarBounds.center.x, sarBounds.center.z + sarBounds.extents.z + 0.1f);
        Vector2 rightFace = new Vector2(sarBounds.center.x + sarBounds.extents.x + 0.1f, sarBounds.center.z);
        Vector2 leftFace = new Vector2(sarBounds.center.x - sarBounds.extents.x - 0.1f, sarBounds.center.z);
        // Init camera position
        Vector2 cameraPos = new Vector2(holoCamera.transform.position.x, holoCamera.transform.position.z);

        // Calculate closest face
        float frontDist = Vector2.Distance(frontFace, cameraPos);
        float backDist = Vector2.Distance(backFace, cameraPos);
        float rightDist = Vector2.Distance(rightFace, cameraPos);
        float leftDist = Vector2.Distance(leftFace, cameraPos);

        if(frontDist < backDist && frontDist < rightDist && frontDist < leftDist)
        {
            closestFace = frontFace;
            closestFaceRotateY = 0;
        }
        else if (backDist < frontDist && backDist < rightDist && backDist < leftDist)
        {
            closestFace = backFace;
            closestFaceRotateY = 180;

        }
        else if (rightDist < frontDist && rightDist < backDist && backDist < leftDist)
        {
            closestFace = rightFace;
            closestFaceRotateY = -90;
        }
        else if (leftDist < frontDist && leftDist < backDist && leftDist < rightDist)
        {
            closestFace = leftFace;
            closestFaceRotateY = 90;
        }
    }

    void Update()
    {
        /*if (isDrawn)
        {
            FindFace();
            menu.transform.position = new Vector3(closestFace.x, sarBounds.min.y, closestFace.y);
            menu.transform.rotation = Quaternion.AngleAxis(closestFaceRotateY, Vector3.up);
        }*/
        /*if (isDrawn && BaseAppStateManager.Instance.SelectedGameObject != gameObject)
        {
            Destroy(menu);
            isDrawn = false;
        }*/
    }
}