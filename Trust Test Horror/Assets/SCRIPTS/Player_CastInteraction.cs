using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player_CastInteraction : MonoBehaviour
{
    [Header("REFERENCIAS")]
    [SerializeField]
    private Transform player_camera;
    public InputActionReference what_is_interaction_input;
    [field: SerializeField] private LayerMask what_is_interaction_layer;
    [Header("outros")]
    public UnityEvent timer_reached;
    private bool is_interaction_interval = false;
    [SerializeField] private float interaction_max_timer, elapsed_timer;
    [field: SerializeField] public float castlength { get; private set; }

    private void Awake()
    {
        player_camera = Camera.main.transform;

        timer_reached.AddListener(() => StartCoroutine(check_interaction()));
    }


    private void Start()
    {
        StartCoroutine(check_interaction());
    }

    IEnumerator check_interaction()
    {
        yield return new_input_CO();
        if (!is_interaction_interval)
        {
            Debug.Log("attempt interacting");
            handle_castinteraction(player_camera);
            StartCoroutine(elapsed_interaction_timer());
            yield break;
        }

    }

    IEnumerator new_input_CO()
    {
        while (!Keyboard.current.eKey.wasPressedThisFrame) yield return null;
    }
    IEnumerator elapsed_interaction_timer()
    {
        is_interaction_interval = true;
        while (is_interaction_interval && elapsed_timer < interaction_max_timer) { elapsed_timer += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        if (elapsed_timer >= interaction_max_timer) {
            timer_reached?.Invoke();
            elapsed_timer = 0f;
            is_interaction_interval = false;
            yield break;
                }
    }

    void handle_castinteraction(Transform cam_obj)
    {
        Debug.Log("Player : Interaction Input");

        Ray ray = new Ray(cam_obj.transform.position, cam_obj.transform.forward);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, castlength, what_is_interaction_layer)){
            bool flowControl = hit_check(hit);
            if (!flowControl)
            {
                return;
            }
        }
    }

    private static bool hit_check(RaycastHit hit)
    {
        if (hit.collider.gameObject == null) return false;
        Debug.Log(hit);
        IInteractable itf;
        bool find_interface_attempt = hit.collider.gameObject.TryGetComponent<IInteractable>(out itf);
        if (!find_interface_attempt) return false;
        itf = hit.collider.GetComponent<IInteractable>();
        itf.object_Interact();
        return true;
    }
}