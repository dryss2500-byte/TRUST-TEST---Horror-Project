using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractionObject : MonoBehaviour, IInteractable
{
    [field: SerializeField] public bool interaction_enabled { get; private set; }

    [field: SerializeField] public InteractionTypes.TYPE this_interaction_type { get; private set; }

    public event Action<MonoBehaviour> EA_INTERACTED;

    void IInteractable.object_Interact()
    {
        if(interaction_enabled) this.Interacted();
        //throw new System.NotImplementedException();
    }

    public virtual void Interacted()
    {
        EA_INTERACTED?.Invoke(this);
        UnityEngine.Debug.Log("OBJECT INTERACTED");
    }
}
[System.Serializable]
public static class InteractionTypes
{

    public enum TYPE { Pickup, Default }


}
