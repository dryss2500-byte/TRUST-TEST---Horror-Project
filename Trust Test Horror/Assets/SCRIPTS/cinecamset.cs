using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class cinecamset : CinemachineBrain
{
    [SerializeField]
    CinemachineCamera vcam;
    [SerializeField]
    CinemachinePanTilt pantiltrotation_cinecam;
    [SerializeField]
    CinemachineInputAxisController axisdetect_cinecam;
    [SerializeField]
    CinemachineBasicMultiChannelPerlin noiseperlin_cinecam;
    [field: SerializeField] public bool canlook { get; set; }
    private bool target_assigned;
    [field: SerializeField] private Transform look_target;

    readonly float t = 5.0f;
    [Range(0, 20)]
    [SerializeField]
    float upmultiplier;

    private void Awake()
    {
        
        Player_Handler.a_statechanged += onstatechange;
    }


    public void set_lock(bool set, Transform newtarget)
    {
        if (!set)
        {
            unset();
            return;
        }

        set_target(newtarget);
        set_assigned();

    }

    void set_assigned()
    {
        target_assigned = look_target.IsUnityNull();
    }
    void unset()
    {
        if (!target_assigned) return;
        set_target(null);
        set_assigned();
    }

    private void set_target(Transform newtarget)
    {
        look_target = newtarget;
        Debug.Log($"playercinecam : new target {newtarget.name}");
    }

    void onstatechange(Player_state.STATE statenow)
    {
        switch (statenow)
        {
            case Player_state.STATE.lookat:
                canlook = true;
                break;

            default:
                canlook = false;
                break;
        }
    }

    private void Update()
    {
        if (canlook)
        {
            pantiltrotation_cinecam.enabled = false;
            axisdetect_cinecam.enabled = false;
            noiseperlin_cinecam.enabled = false;

            //disable cinecam components
            lerp();

        }
        else
        {
            pantiltrotation_cinecam.enabled = true;
            axisdetect_cinecam.enabled = true;
            noiseperlin_cinecam.enabled = true;


        }
    }

    private void lerp()
    {
        Vector3 relativedir = look_target.position - Camera.main.transform.position;
        vcam.transform.localRotation = Quaternion.Slerp(vcam.transform.localRotation, Quaternion.LookRotation(relativedir, look_target.transform.up), t * Time.deltaTime);
    }
}
