using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalSmoothCamera2D : MonoBehaviour {

    [SerializeField]
    public float DampTime = 0.25f;
    [SerializeField]
    Transform Target;
    [SerializeField]
    bool AdhereToMapBoundry = true;
    [SerializeField] [Tooltip("Must use an Edge Collider 2D. Script will calculate the 4 outermost points of the collider to bound the camera's view to the level. Script will still run if no collider is provided, however, the camera will not bound to the level.")]
    EdgeCollider2D MapCollider;

    private Vector3 Velocity = Vector3.zero;

    struct Bounds 
    {
        public float MinX;
        public float MaxX;
        public float MinY;
        public float MaxY;
    }

    Bounds MapBounds;
    float VertCamExtent;
    float HorzCamExtent;
    bool bLevelBoundsInitialized = false;

    // Use this for initialization
    void Start ()
    {
        InitializeLevelBounds();   
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    private void FixedUpdate()
    {
        if (Target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(Target.position);
            Vector3 delta = Target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;

            if (AdhereToMapBoundry && MapCollider)
            {
                if (bLevelBoundsInitialized)
                {
                    if (destination.x + HorzCamExtent > MapBounds.MaxX) { destination.x = MapBounds.MaxX - HorzCamExtent; }
                    if (destination.x - HorzCamExtent < MapBounds.MinX) { destination.x = MapBounds.MinX + HorzCamExtent; }
                    if (destination.y + VertCamExtent > MapBounds.MaxY) { destination.y = MapBounds.MaxY - VertCamExtent; }
                    if (destination.y - VertCamExtent < MapBounds.MinY) { destination.y = MapBounds.MinY + VertCamExtent; }
                }
                else { InitializeLevelBounds(); }
            }

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref Velocity, DampTime);
        }
    }

    void InitializeLevelBounds()
    {
        if (AdhereToMapBoundry && MapCollider)
        {
            float minX = MapCollider.points[0].x;
            float maxX = MapCollider.points[0].x;
            float minY = MapCollider.points[0].y;
            float maxY = MapCollider.points[0].y;

            for (int i = 0; i < MapCollider.points.Length; i++)
            {
                minX = Mathf.Min(minX, MapCollider.points[i].x);
                maxX = Mathf.Max(maxX, MapCollider.points[i].x);
                minY = Mathf.Min(minY, MapCollider.points[i].y);
                maxY = Mathf.Max(maxY, MapCollider.points[i].y);
            }

            MapBounds.MinX = minX;
            MapBounds.MaxX = maxX;
            MapBounds.MinY = minY;
            MapBounds.MaxY = maxY;

            VertCamExtent = Camera.main.orthographicSize;
            HorzCamExtent = VertCamExtent * Screen.width / Screen.height;

            bLevelBoundsInitialized = true;
        }
    }
}
