using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float speed = 1500;

    public WheelJoint2D leftWheel;
    public WheelJoint2D rightWheel;

    public GameObject screenWrapper;
    private UIController uiController;

    private float movement = 0f;
    private bool isGasActive = false;
    private bool isBrakeActive = false;

    void Start()
    {
        uiController = screenWrapper.GetComponent<UIController>();
    }

    void Update()
    {
        if (isBrakeActive)
        {
            movement = 1 * speed;
        }
        else if (isGasActive)
        {
            movement = -1 * speed;
        }
        else
        {
            //movement = 0f;
            movement = -1 * Input.GetAxisRaw("Horizontal") * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item_Canister")
        {
            uiController.IncrementGasValue(10);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Item_Coin")
        {
            uiController.IncrementCoinValue(1);
            Destroy(collision.gameObject);
        }
    }

    public void OnGasStart() {
        isGasActive = true;
    }

    public void OnGasEnd() {
        isGasActive = false;
    }

    public void OnBrakeStart() {
        isBrakeActive = true;
    }

    public void OnBrakeEnd() {
        isBrakeActive = false;
    }

    private void FixedUpdate()
    {
        if (movement == 0f)
        {

            leftWheel.useMotor = false;
            rightWheel.useMotor = false;

        }
        else
        {

            leftWheel.useMotor = true;
            rightWheel.useMotor = true;

            JointMotor2D motor = new JointMotor2D
            {
                motorSpeed = movement,
                maxMotorTorque = 10000
            };

            rightWheel.motor = motor;
            leftWheel.motor = motor;
        }

    }
}

