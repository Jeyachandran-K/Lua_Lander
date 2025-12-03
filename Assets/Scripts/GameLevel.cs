using UnityEngine;

public class GameLevel : MonoBehaviour
{
    

    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartingPositionTransform;
    [SerializeField] private Transform cameraStaringPositionTransform;
    [SerializeField] private float zoomOutOrthographicSize;

    public int GetLevelNumber()
    {
        return levelNumber;
    }
    public Vector3 GetLanderStartingPosition()
    {
        return landerStartingPositionTransform.position;
    }
    public Transform GetCameraStartingPositionTransform()
    {
        return cameraStaringPositionTransform;
    }
    public float GetZoomOutOrthographicSize()
    {
        return zoomOutOrthographicSize;
    }
    
}
