using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartingPositionTransform;

    public int GetLevelNumber()
    {
        return levelNumber;
    }
    public Vector3 GetLanderStartingPosition()
    {
        return landerStartingPositionTransform.position;
    }
}
