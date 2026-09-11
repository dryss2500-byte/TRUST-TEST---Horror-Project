using System;
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

    [field: SerializeField] public cinecamset pcinecam { get; set; }


    public static event Action<Player_state.STATE> a_statechanged;




    public static Player_Handler plrh;
    private void Awake()  {
        plrh = this;
    }

    public void set_state(Player_state.STATE newstate)
    {

        Player_Handler.state = newstate;
        switch_on_new_state(Player_Handler.state);

    }


    void switch_on_new_state(Player_state.STATE thestate)
    {
        switch (state)
        {
            case Player_state.STATE.freeroam:

                pmovement.enabled = true;
                pcasting.enabled = true;
                pheadbob.enabled = true;

                break;

            case Player_state.STATE.lookat:


                pmovement.enabled = false;
                pcasting.enabled = false;
                pheadbob.enabled = false;



                break;

            case Player_state.STATE.isinmenu:


                pmovement.enabled = false;
                pcasting.enabled = false;
                pheadbob.enabled = false;


                break;
        }
        a_statechanged?.Invoke(thestate);


    }
}
