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
        float softLandingVelocityMagnitude = 3f;
        if (collision2D.relativeVelocity.magnitude > softLandingVelocityMagnitude)
        {
            Debug.Log("Lander Crashed");
            return;
        }
        Debug.Log("Successful Landing");

    }
}
