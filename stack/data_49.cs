public Transform player1;
public Transform player2;

private const float DISTANCE_MARGIN = 1.0f;

private Vector3 middlePoint;
private float distanceFromMiddlePoint;
private float distanceBetweenPlayers;
private float cameraDistance;
private float aspectRatio;
private float fov;
private float tanFov;

void Start() {
    aspectRatio = Screen.width / Screen.height;
    tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
}

void Update () {
    // Position the camera in the center.
    Vector3 newCameraPos = Camera.main.transform.position;
    newCameraPos.x = middlePoint.x;
    Camera.main.transform.position = newCameraPos;

    // Find the middle point between players.
    Vector3 vectorBetweenPlayers = player2.position - player1.position;
    middlePoint = player1.position + 0.5f * vectorBetweenPlayers;

    // Calculate the new distance.
    distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
    cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;

    // Set camera to new position.
    Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
    Camera.main.transform.position = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);
}
public Transform player1;
public Transform player2;

private const float FOV_MARGIN = 15.0f;

private Vector3 middlePoint;
private float distanceFromMiddlePoint;
private float distanceBetweenPlayers;
private float aspectRatio;

void Start () {
    aspectRatio = Screen.width / Screen.height;
}

void Update () {
    // Find the middle point between players.
    middlePoint = player1.position + 0.5f * (player2.position - player1.position);

    // Position the camera in the center.
    Vector3 newCameraPos = Camera.main.transform.position;
    newCameraPos.x = middlePoint.x;
    Camera.main.transform.position = newCameraPos;

    // Calculate the new FOV.
    distanceBetweenPlayers = (player2.position - player1.position).magnitude;
    distanceFromMiddlePoint = (Camera.main.transform.position - middlePoint).magnitude;
    Camera.main.fieldOfView = 2.0f * Mathf.Rad2Deg * Mathf.Atan((0.5f * distanceBetweenPlayers) / (distanceFromMiddlePoint * aspectRatio));

    // Add small margin so the players are not on the viewport border.
    Camera.main.fieldOfView += FOV_MARGIN;
}
using UnityEngine;

public class MeleeCamera : MonoBehaviour
{
    public Transform[] targets;
    public float padding = 15f; // amount to pad in world units from screen edge

    Camera _camera;
    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void LateUpdate() // using LateUpdate() to ensure camera moves after everything else has
    {
        Bounds bounds = FindBounds();

        // Calculate distance to keep bounds visible. Calculations from:
        //     "The Size of the Frustum at a Given Distance from the Camera": https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        //     note: Camera.fieldOfView is the *vertical* field of view: https://docs.unity3d.com/ScriptReference/Camera-fieldOfView.html
        float desiredFrustumWidth = bounds.size.x + 2 * padding;
        float desiredFrustumHeight = bounds.size.z + 2 * padding;

        float distanceToFitHeight = desiredFrustumHeight * 0.5f / Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float distanceToFitWidth = desiredFrustumWidth * 0.5f / Mathf.Tan(_camera.fieldOfView * _camera.aspect * 0.5f * Mathf.Deg2Rad);

        float resultDistance = Mathf.Max(distanceToFitWidth, distanceToFitHeight);

        // Set camera to center of bounds at exact distance to ensure targets are visible and padded from edge of screen 
        _camera.transform.position = bounds.center + Vector3.up * resultDistance;
    }

    private Bounds FindBounds()
    {
        if (targets.Length == 0)
        {
            return new Bounds();
        }

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform target in targets)
        {
            if (target.gameObject.activeSelf) // if target not active
            {
                bounds.Encapsulate(target.position);
            }
        }

        return bounds;
    }
}