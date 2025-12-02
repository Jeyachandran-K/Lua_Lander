using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    public static Lander Instance {  get; private set; }

    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;
    public event EventHandler OnCoinPickUp;
    public event EventHandler<OnLandingEventArgs> OnLanding;
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;

    public class OnStateChangeEventArgs : EventArgs
    {
        public State state;
    }

    public class OnLandingEventArgs : EventArgs {
        public LandingType landingType;
        public int score;
        public float dotProduct;
        public float landingSpeed;
        public float scoreMultiplier;
    }

    public enum LandingType
    {
        Success,
        WrongLandingArea,
        TooSteepAngle,
        TooFastLanding
    }
    public enum State { 
        WaitingToStart,
        Normal,
        GameOver,
    }


    private Rigidbody2D landerRigidbody2D;
    private float fuelAmount;
    private float fuelAmountMax = 10f;
    private const float GRAVITY_NORMAL = 0.7f;
    private State state;

    [SerializeField] private float speed;
    [SerializeField] private float torque;



    private void Awake()
    {
        Instance = this;
        fuelAmount = fuelAmountMax;
        landerRigidbody2D=GetComponent<Rigidbody2D>();
        landerRigidbody2D.gravityScale = 0;
        state = State.WaitingToStart;
    }
    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);

        switch (state)
        {
            default:
                break;
            case State.WaitingToStart:
                if (Keyboard.current.upArrowKey.isPressed ||
                    Keyboard.current.rightArrowKey.isPressed ||
                    Keyboard.current.leftArrowKey.isPressed)
                {
                    landerRigidbody2D.gravityScale = GRAVITY_NORMAL;
                    state=State.Normal;
                    OnStateChange?.Invoke(this, new OnStateChangeEventArgs { state=state });

                }
                break;
            case State.Normal:
                if (fuelAmount <= 0)
                {
                    return;
                }


                if (Keyboard.current.upArrowKey.isPressed ||
                    Keyboard.current.rightArrowKey.isPressed ||
                    Keyboard.current.leftArrowKey.isPressed)
                {
                    ConsumeFuel();
                }
        
                if (Keyboard.current.upArrowKey.isPressed)
                {
                    landerRigidbody2D.AddForce(transform.up * speed * Time.fixedDeltaTime);
                    OnUpForce?.Invoke(this, EventArgs.Empty);
                }
                if (Keyboard.current.leftArrowKey.isPressed)
                {
                    landerRigidbody2D.AddTorque(torque * Time.fixedDeltaTime);
                    OnLeftForce?.Invoke(this, EventArgs.Empty);
                }
                if (Keyboard.current.rightArrowKey.isPressed)
                {
                    landerRigidbody2D.AddTorque(-1 * torque * Time.fixedDeltaTime);
                    OnRightForce?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }    
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        float softLandingVelocityMagnitude = 3.5f;
        float dotProduct = Vector2.Dot(transform.up, Vector2.up);
        float relativeVelocityMagnitude = collision2D.relativeVelocity.magnitude;
        float minDotProduct = 0.95f;
        if (!collision2D.gameObject.TryGetComponent<LandingPad>(out LandingPad landingPad))
        {
            //Debug.Log("We hit the object that is not landing pad");
            OnLanding?.Invoke(this, new OnLandingEventArgs
            {
                landingType = LandingType.WrongLandingArea,
                score = 0,
                dotProduct = dotProduct,
                landingSpeed = relativeVelocityMagnitude,
                scoreMultiplier = 0,
            });
            SetState(State.GameOver);

            return;
        }
        
        if (relativeVelocityMagnitude > softLandingVelocityMagnitude)
        {
            //Debug.Log("Lander Crashed");
            OnLanding?.Invoke(this, new OnLandingEventArgs
            {
                landingType = LandingType.TooFastLanding,
                score = 0,
                dotProduct = dotProduct,
                landingSpeed = relativeVelocityMagnitude,
                scoreMultiplier = 0,
            });
            SetState(State.GameOver);
            return;
        }
        
        if (dotProduct < minDotProduct)
        {
            //Debug.Log("Too steep of Angle");
            OnLanding?.Invoke(this, new OnLandingEventArgs
            {
                landingType = LandingType.TooSteepAngle,
                score = 0,
                dotProduct = dotProduct,
                landingSpeed = relativeVelocityMagnitude,
                scoreMultiplier = 0,
            });
            SetState(State.GameOver);
            return;
        }
        
        

        float maxScoreLandingAngle = 100f;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore= maxScoreLandingAngle-((1-dotProduct)*maxScoreLandingAngle*scoreDotVectorMultiplier);

        float maxScoreLandingSpeed = 100f;
        float landingSpeedScore = maxScoreLandingSpeed * (softLandingVelocityMagnitude - relativeVelocityMagnitude);

        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.getScoreMultiplier());

        //Debug.Log("score :"+score);

        OnLanding?.Invoke(this,new OnLandingEventArgs { 
            landingType=LandingType.Success,
            score= score,
            dotProduct=dotProduct,
            landingSpeed=relativeVelocityMagnitude,
            scoreMultiplier=landingPad.getScoreMultiplier(),
        });
        SetState(State.GameOver);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out FuelPickUp fuelPickUp))
        {
            float addFuelAmount = 10f;
            fuelAmount += addFuelAmount;
            if (fuelAmount > fuelAmountMax)
            {
                fuelAmount= fuelAmountMax;
            }
            fuelPickUp.DestroySelf();
        }
        if(collision.gameObject.TryGetComponent(out CoinPickUp coinPickUp))
        {
            OnCoinPickUp?.Invoke(this, EventArgs.Empty);
            coinPickUp.DestroySelf();
        }
    }

    private void ConsumeFuel()
    {
        float fuelConsumptionAMount = 1f;
        fuelAmount -= fuelConsumptionAMount*Time.deltaTime;
    }
    public float GetSpeedX()
    {
        return landerRigidbody2D.linearVelocityX;
    }
    public float GetSpeedY()
    {
        return landerRigidbody2D.linearVelocityY;
    }
    public float GetFuelAmount()
    {
        return fuelAmount;
    }
    public float GetFuelAmountNormalized()
    {
        return fuelAmount / fuelAmountMax;
    }
    private void SetState(State state)
    {
        this.state = state;
        OnStateChange?.Invoke(this, new OnStateChangeEventArgs { state = state });
    }
}
