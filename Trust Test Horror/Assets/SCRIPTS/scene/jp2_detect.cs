using System.Collections;
using UnityEngine;

public class jp2_detect : MonoBehaviour
{
    [Header("utils")]
    Collider detection;
    [SerializeField]
    GameObject npc;

    [SerializeField] private AudioClip[] swriter;
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



        char_dialogue_entry.instance.set_entries(true, "what's up unc? go take your pills", "YOU:", swriter[1]);

        yield return new WaitUntil(() => char_dialogue_entry.running == false); 

        char_dialogue_entry.instance.set_entries(true, "do you listen to weezer?", "??? [Stranger]:", swriter[1]);

        yield return new WaitUntil(() => char_dialogue_entry.running == false);

        char_dialogue_entry.instance.set_entries(true, "weezer is lowkey overrated, i'd sleep listening to BLINK-1999 (Blink 182 spotify playlist included) 24/7 rather than listening to these flopped stuff", "YOU:", swriter[1]);

        yield return new WaitUntil(() => char_dialogue_entry.running == false);

        _Audio_Manager_.manager.play_sfx(swriter[2]);

        char_dialogue_entry.instance.set_entries(true, "_", "??? [Stranger]:", swriter[1]);

        yield return new WaitUntil(() => char_dialogue_entry.running == false);

        char_dialogue_entry.instance.set_entries(true, "day 4: Ui overhaul (still WIP, no display prompt yet)", "[ThatOneDoom LOG]:", swriter[1]);



        yield return new WaitUntil(() => char_dialogue_entry.running == false);

        //ENDED
        yield return new WaitForSeconds(5f);
        plrh.set_state(Player_state.STATE.freeroam);
    }

}
