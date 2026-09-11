using UnityEngine;

public class Player_check_closest_object : MonoBehaviour
{
    public Camera main;
    [field: SerializeField] private Ray casted_ray;
    [field: SerializeField] private LayerMask what_is_interaction_layer;
    [field: SerializeField] public bool has_detected { get; private set; }
    [field: SerializeField] public static RaycastHit current { get; private set; }


    private void Update()
    {
        RaycastHit newhit = new RaycastHit();
        casted_ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(casted_ray, out newhit, GameObject.FindAnyObjectByType<Player_CastInteraction>().castlength, what_is_interaction_layer))
        {
            Player_check_closest_object.current = newhit;
            has_detected = true;
        }
        else{
            has_detected = false;
        }
    }

    public static RaycastHit return_is_detected() { return current; }
}
