using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager_V2 : MonoBehaviour {
    public Material[] tileMaterials;
    public GameObject[] terrainPlanes;
    public GameObject[] terrainObjects;
    public Transform dynamicFolder;
    public int tileSize = 5;
    public int tileCreationRadius = 1;
    public int amountOfObjectsPerTile = 5;
    
    private Dictionary<Vector3, GameObject> _planes = new Dictionary<Vector3, GameObject>();
    private GameManager_V2 _gameManager;
    private GameObject activePlane;
    
    private static TerrainManager_V2 _instance;
    public static TerrainManager_V2 Instance { get { return _instance; } }

    // Hack -- remove
    private Rigidbody _playerRigidbody;

    // Instantiate singleton pattern --
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Start () {
        _gameManager = GameManager_V2.Instance;

        // Hack -- remove
        _playerRigidbody = _gameManager.player.GetComponent<Rigidbody>();

        InstantiatePlane(new Vector3(0, -1, 0));
	}

    void FixedUpdate()
    {
        GetActivePlane();
    }

    // Adds a new plane to the scene. Creation is dependant on what already exists in `_planes`
    void InstantiatePlane(Vector3 position)
    {
        if (!_planes.ContainsKey(position))
        {
            // Testing with plane creation and tileSize
            //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
            
            GameObject go = Instantiate(terrainPlanes[Random.Range(0, terrainPlanes.Length - 1)]);

            // For default settings with the currently configured terrain objects and tileSize = 5
            go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // //go.transform.localScale = new Vector3(tileSize, 1, tileSize); <- To test with the PrimitiveType.Plane

            go.transform.parent = dynamicFolder;
            go.AddComponent<Plane_V2>();
            go.transform.localRotation = Quaternion.AngleAxis(-45, Vector3.left);
            go.transform.position = position;

            // Add objects to new terrain game object
            InstantiatePlaneObjects(go);

            // Adds positional data for key and the plane object for constant retrieval
            _planes.Add(position, go);
        }
    }
    
    // Use the player to raycast downwards so we know what plane the player is currently on
    void GetActivePlane()
    {
        Transform player = _gameManager.player;
        RaycastHit hit = new RaycastHit();
        
		if (Physics.Raycast(player.position, -transform.up, out hit))
        {
            // Hack -- remove
            if (hit.distance > 5)
                _playerRigidbody.AddForce(_gameManager.player.transform.up * -100);
            
            if (activePlane != hit.transform.gameObject || activePlane == null)
                CheckSurroundingPlanes(hit.collider.transform.position);

            activePlane = hit.transform.gameObject;
        }
    }

    // Not sure if running this as a coroutine really matters..
    void CheckSurroundingPlanes(Vector3 startingPosition, int step = 0)
    {
        // Finds the surrounding planes based on `startingPosition`
        // Will recursively call this function if the global `tileCreationRadius` is set to be greater than 1

        // [x] [x] [x]
        // [x] [o] [x]
        // [x] [x] [x]

        Dictionary<string, Vector3> planePositions = new Dictionary<string, Vector3>();

        // Looks like a planes size is just plane size * 10
        float xOffset = tileSize * 10;
        // TODO: Figure out best calculation - 0.7 is the magic number!
        float zOffset = xOffset * 0.7f;

        planePositions.Add("left", startingPosition + new Vector3(xOffset, 0, 0));
        planePositions.Add("right", startingPosition + new Vector3(-xOffset, 0, 0));
        planePositions.Add("up", startingPosition + new Vector3(0, zOffset, -zOffset));
        planePositions.Add("down", startingPosition + new Vector3(0, -zOffset, zOffset));
        planePositions.Add("up-left", startingPosition + new Vector3(xOffset, zOffset, -zOffset));
        planePositions.Add("up-right", startingPosition + new Vector3(-xOffset, zOffset, -zOffset));
        planePositions.Add("down-left", startingPosition + new Vector3(xOffset, -zOffset, zOffset));
        planePositions.Add("down-right", startingPosition + new Vector3(-xOffset, -zOffset, zOffset));
        
        if (step < tileCreationRadius - 1)
        {
            CheckSurroundingPlanes(planePositions["left"], ++step);
            CheckSurroundingPlanes(planePositions["right"], ++step);
            CheckSurroundingPlanes(planePositions["up"], ++step);
            CheckSurroundingPlanes(planePositions["down"], ++step);
            CheckSurroundingPlanes(planePositions["up-left"], ++step);
            CheckSurroundingPlanes(planePositions["up-right"], ++step);
            CheckSurroundingPlanes(planePositions["down-left"], ++step);
            CheckSurroundingPlanes(planePositions["down-right"], ++step);
        }

        foreach (Vector3 position in planePositions.Values)
            InstantiatePlane(position);
    }

    void InstantiatePlaneObjects(GameObject terrain)
    {
        for (int i = 0; i < amountOfObjectsPerTile; i++)
        {
            GameObject go = Instantiate(terrainObjects[Random.Range(0, terrainObjects.Length - 1)]);

            float xOffset = 0;//tileSize * 10 * 2.25f;
            float zOffset = 0;//xOffset / 2.25f;

            float x = Random.Range(0, -tileSize * 10 * 2);
            float z = Random.Range(0, tileSize * 10);

            go.transform.parent = terrain.transform;
            go.transform.localRotation = Quaternion.AngleAxis(135, Vector3.left);
            go.AddComponent<TerrainObject_V2>();
            go.transform.localPosition = new Vector3(x, -1, z);
        }
    }

    //void DestroyOldPlanes()
    //{
    //    int amountToDestroy = 100; // Update this to be dynamic
    //    Dictionary<Vector3, GameObject> memo = new Dictionary<Vector3, GameObject>();

    //    if (_planes.Keys.Count > amountToDestroy)
    //    {
    //        foreach (Vector3 key in _planes.Keys)
    //        {
    //            if (amountToDestroy * 1.2f > _planes.Keys.Count)
    //                memo.Add(key, _planes[key]);
    //            amountToDestroy--;
    //        }
    //    }

    //    foreach (Vector3 key in memo.Keys)
    //    {
    //        Debug.Log("REMOVING");
    //        _planes.Remove(key);
    //    }
    //}
}
