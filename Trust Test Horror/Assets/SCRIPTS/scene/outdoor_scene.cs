using UnityEngine;

public class outdoor_scene : MonoBehaviour
{
    public static outdoor_scene instance;

    [Header("UTILS")]

    [field: SerializeField] public baseinteractable crowbar { get; private set; }
    [field: SerializeField] public gate_interaction gate { get; private set; }

    [field: SerializeField] public Collider jp1trigger { get; private set; }

    [field: SerializeField] public Collider jp2trigger { get; private set; }

    private void Awake()
    {
        get_instance();
        crowbar.EA_INTERACTED += clear_from_instance;
    }
    private void get_instance()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(instance);
            instance = this;
        }
    }

    private void _clear()
    {
        crowbar = null;
        gate = null;
    }

    public void clear_from_instance(MonoBehaviour obj)
    {
        if (obj == gate) gate = null;
        if (obj == crowbar)
        {
            crowbar.gameObject.SetActive(false);
            crowbar = null;
        }
    }
}