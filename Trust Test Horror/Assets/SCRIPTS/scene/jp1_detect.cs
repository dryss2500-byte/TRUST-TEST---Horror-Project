using UnityEngine;
using UnityEngine.Playables;

public class jp1_detect : MonoBehaviour
{
    [field: SerializeField] private PlayableDirector director;
    [field: SerializeField] private bool didonce = false;

    private void OnTriggerEnter(Collider other)
    {
        Player_Movement pm;
        if (other.CompareTag("Player") || other.TryGetComponent<Player_Movement>(out pm))jumpscare();
    }

    void jumpscare()
    {
        bool flow = check_Cond();
        if (flow) return;

        Debug.Log("do");
        didonce = true;

        PlayerPrefs.SetInt("apparition 1", 1);

        director.Play();

        Destroy(GetComponent<Collider>());

        UnityEngine.Debug.Log($"{PlayerPrefs.GetInt("apparition 1")} JP1");

        this.enabled = false;

    }

    private bool check_Cond()
    {
        bool returnvalue;
        if (PlayerPrefs.GetInt("apparition 1") < 1 && !didonce) returnvalue = true; else returnvalue = false;
        return returnvalue;
    }
}
