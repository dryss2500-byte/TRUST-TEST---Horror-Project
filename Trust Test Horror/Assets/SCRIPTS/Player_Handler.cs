using UnityEngine;

public class Player_Handler : MonoBehaviour
{
    [Header("state")]
    public static Player_state.STATE state;

    [Header("ref")]
    [field: SerializeField] public Player_Movement pmovement { get; private set; }
    [field: SerializeField] public Player_CastInteraction pcasting { get; private set; }
    [field: SerializeField] public Player_headbob pheadbob { get; private set; }
    [field: SerializeField] public Player_current_interactable_target pvisibleobjects_detection { get; private set; }
    [field: SerializeField] public GameObject pflashlightobject { get; private set; }




    public static Player_Handler plrh;
    private void Awake()  {
        plrh = this;
    }
}
