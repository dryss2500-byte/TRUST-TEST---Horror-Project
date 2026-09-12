using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class char_dialogue_entry : MonoBehaviour
{
    public static bool freetoentry;
    public static bool running;
    public static char_dialogue_entry instance;
    public Canvas user_interface_prefab;
    public Canvas current_valid_user_interface;
    public static UnityEvent ue_pogressed;


    [Tooltip("utils")]

    public static string subentry_st, subname_st;
    private void Awake()
    {
        set_st_instance();
    }

    public void set_entries(bool qmstart, string entry, string name, AudioClip writer)
    {
        subentry_st = entry;
        subname_st = name;

        UnityEngine.Debug.Log($"DIALOGUE HANDLER : the entry = {entry}, character name = {name}");

        if (!qmstart) return;

        //start if needed
        current_valid_user_interface = Instantiate(user_interface_prefab, GameObject.FindGameObjectWithTag("User Interface").transform);
        dialogue_user_interface user_interface_cs;
        bool flow = current_valid_user_interface.TryGetComponent<dialogue_user_interface>(out user_interface_cs);
        Debug.Log(user_interface_cs);
        if (!flow) return;
        StartCoroutine(typewriter_effect_co(user_interface_cs.tmpentry,user_interface_cs.tmpname,.07f, true, user_interface_cs.background, user_interface_cs.tmp_paktc, writer));



    }

    IEnumerator typewriter_effect_co(TMPro.TMP_Text tmp, TMPro.TMP_Text Ntmp, float typespeed, bool lastsection, Image background, TMPro.TMP_Text tmppackt, AudioClip writer)
    {
        running = true;
        tmppackt.enabled = false;
        Ntmp.text = subname_st;
        if (subname_st == "YOU:") Ntmp.color = Color.HSVToRGB(124f, 252f, 255f, false);
        tmp.GetComponent<RectTransform>().sizeDelta = new Vector2(1250f, 120f);
        tmp.alignment = TMPro.TextAlignmentOptions.TopLeft;

        char[] entry_chars = subentry_st.ToCharArray();
        tmp.text = string.Empty;
        bool filled = false;

        foreach(char c in entry_chars)
        {

            tmp.text += c;

            _Audio_Manager_.manager.play_sfx(writer);

            if (tmp.text == subentry_st) filled = true;

            yield return new WaitForSeconds(typespeed);
        }

        yield return new WaitUntil(() => filled == true);
        Debug.Log("can press any input");
        tmppackt.enabled = true;
        yield return any_input_CO();
        Debug.Log("do");
        ///fade out
        ///
        if (!lastsection) yield break; //will just snap to another entry

        tmp.text = string.Empty;
        Ntmp.text = string.Empty;
        tmppackt.text = string.Empty;

        Animator anim = background.GetComponent<Animator>();
        anim.SetTrigger("f");


        yield return new WaitForSeconds(.5f); // anim length
        DestroyImmediate(current_valid_user_interface, true);

        ue_pogressed?.Invoke();




        running = false;

        erase_values();

        yield break;
    }

    IEnumerator any_input_CO()
    {
        while (!Keyboard.current.anyKey.wasPressedThisFrame) yield return null;
    }


    public static void erase_values()
    {
        subentry_st = string.Empty;
        subname_st = string.Empty;
    }
    private void set_st_instance()
    {
        if (instance != null)
        {
            Destroy(instance);
            {
                instance = this;
            }
        }
        else
            instance = this;
    }
}
