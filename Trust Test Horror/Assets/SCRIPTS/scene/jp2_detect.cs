using System.Collections;
using UnityEngine;

public class jp2_detect : MonoBehaviour
{
    Collider detection;
    [SerializeField]
    GameObject npc;
    private void Awake()
    {
        npc.SetActive(false);
        detection = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Player_Handler plrh = Player_Handler.plrh;
        if (!other.TryGetComponent<Player_Handler>(out plrh)) return;
        if (other.GetComponent<Player_Handler>() == plrh)
        {
            Destroy(GetComponent<Collider>());
            StartCoroutine(DIALOGUE_CO(plrh));
        }
    }

    IEnumerator DIALOGUE_CO (Player_Handler plrh)
    {
        yield return null;
        //STARTED
        npc.SetActive(true);
        plrh.pcinecam.set_lock(true,npc.transform);
        plrh.set_state(Player_state.STATE.lookat);



        //UI




        //ENDED
        yield return new WaitForSeconds(5f);
        plrh.set_state(Player_state.STATE.freeroam);
    }
}
