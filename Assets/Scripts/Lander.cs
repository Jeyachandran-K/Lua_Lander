using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    private Rigidbody2D landerRigidbody2D;
    [SerializeField] private float speed;
    [SerializeField] private float torque;
    private void Awake()
    {
        landerRigidbody2D=GetComponent<Rigidbody2D>();
       
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>();
        if (Keyboard.current.upArrowKey.isPressed)
        {
            landerRigidbody2D.AddForce(transform.up * speed * Time.fixedDeltaTime);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            landerRigidbody2D.AddTorque(torque * Time.fixedDeltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            landerRigidbody2D.AddTorque(-1*torque * Time.fixedDeltaTime);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        float softLandingVelocityMagnitude = 3.5f;
        if (!collision2D.gameObject.TryGetComponent<LandingPad>(out LandingPad landingPad))
        {
            Debug.Log("We hit the object that is not landing pad");
            return;
        }
        float relativeVelocityMagnitude = collision2D.relativeVelocity.magnitude;
        if (relativeVelocityMagnitude > softLandingVelocityMagnitude)
        {
            Debug.Log("Lander Crashed");
            return;
        }
        float dotProduct = Vector2.Dot(transform.up, Vector2.up);
        float minDotProduct = 0.95f;
        if (dotProduct < minDotProduct)
        {
            Debug.Log("Too steep of Angle");
            return;
        }
        
        Debug.Log("Successful Landing");

        float maxScoreLandingAngle = 100f;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore= maxScoreLandingAngle-((1-dotProduct)*maxScoreLandingAngle*scoreDotVectorMultiplier);

        float maxScoreLandingSpeed = 100f;
        float landingSpeedScore = maxScoreLandingSpeed * (softLandingVelocityMagnitude - relativeVelocityMagnitude);

        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.getScoreMultiplier());

        Debug.Log("score :"+score);
    }
}
