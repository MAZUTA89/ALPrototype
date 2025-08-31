using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTileMap;
    [SerializeField] private TileBase whiteTile;

    static Vector3 lastPoint;

    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject SpherePrefab;
    static GameObject s_spherePrefab;

    private PlaceableObject objectToPlace;


    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
        s_spherePrefab = SpherePrefab;
    }


    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            lastPoint = raycastHit.point;

            //Instantiate(s_spherePrefab, raycastHit.point, Quaternion.identity);

            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(new Vector3(0, 0, 0));

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);

        objectToPlace = obj.GetComponent<PlaceableObject>();

        obj.AddComponent<ObjectDrag>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction);

        //if (Input.GetMouseButton(0))
        //{
        //    if (Physics.Raycast(ray, out RaycastHit raycastHit))
        //    {
        //        Instantiate(s_spherePrefab, raycastHit.point, Quaternion.identity);
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.A))
        {
            InitializeWithObject(prefab1);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            InitializeWithObject(prefab2);
        }
    }
}
