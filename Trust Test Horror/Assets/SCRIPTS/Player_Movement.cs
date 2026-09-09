using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class Player_Movement : MonoBehaviour
{
    public static Player_Movement Instance;


    [Header("COMPONENTS")]
    [field: SerializeField] public CharacterController ccontroller { get; private set; }
    private void Awake()
    {
        this.ccontroller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        bool flowControl = CheckInstance();
        if (!flowControl)
        {
            return;
        }
    }

    private bool CheckInstance()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(Instance);
            Instance = this;
            return false;
        }

        return true;
    }

    [Header("other")]
    public bool moving;
    [SerializeField]
    private bool canmove;
    [Range(0f, 10.0f)]
    public float movement_speed;
    [SerializeField]
    private float fallmultiplier;
    private float Yspeed;

    [Header("INPUT")]
    public InputActionReference what_is_movement_input;


    private void Update()
    {
        moving = return_moving();


        bool flowControl = movement();
        if (!flowControl)
        {
            return;
        }
    }

    private bool movement()
    {
        if (!canmove) return false;
        check_move(Camera.main.transform);
        return true;
    }

    private void check_move(Transform cam)
    {
        Vector2 minput_value = what_is_movement_input.action.ReadValue<Vector2>().normalized;
        /*Debug.Log(minput_value);*/
        float v = minput_value.y;
        float h = minput_value.x;
        Vector3 move_amount = (cam.forward * v * movement_speed * Time.deltaTime) + (cam.right * h * movement_speed * Time.deltaTime);
        Vector3 move_result = move_amount;
        move_result.y = Yspeed;

        Yspeed -= (Physics.gravity.y * fallmultiplier) * Time.deltaTime;

        ccontroller.Move(move_result);
    }

    private bool return_moving()
    {
        float vel = ccontroller.velocity.sqrMagnitude;

        if (vel <= 0f) return false; else return true;
    }
}