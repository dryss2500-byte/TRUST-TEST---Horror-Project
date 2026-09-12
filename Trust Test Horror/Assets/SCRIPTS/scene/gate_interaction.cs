using System.Diagnostics;
using UnityEngine;
using static UnityEngine.Audio.GeneratorInstance;

public class gate_interaction : InteractionObject
{

    private void Start()
    {
        interaction_enabled = false;
        outdoor_scene.instance.crowbar.EA_INTERACTED += set_will_interact;
    }

    void set_will_interact(MonoBehaviour noutil)
    {
        interaction_enabled = true;

    }


    public override void Interacted()
    {
        base.Interacted();
        this.gameObject.SetActive(false); //no opening animation for now
    }

}