using UnityEngine;

public class flashlight_sway : MonoBehaviour
{
    public Transform holder;
    [SerializeField]
    Quaternion localstartrot = Quaternion.identity;
    [SerializeField]
    [Range(0f, 10.0f)]
    private float smooth, amp, coolingdown;
    private Quaternion swayresult = new Quaternion();

    private void Awake()
    {
        holder = this.gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        float delta = Time.deltaTime;
        float ver = Input.GetAxis("Mouse Y") * amp;
        float hor = Input.GetAxis("Mouse X") * amp;

        swayback(delta);

        sway(delta, ver, hor);
    }

    private void sway(float delta, float ver, float hor)
    {
        Quaternion swayX, swayY;
        get_values(ver, hor, out swayX, out swayY);
        swayresult = (swayX * swayY);
        holder.transform.localRotation = Quaternion.Slerp(holder.transform.localRotation, swayresult, smooth * delta);
    }

    private void get_values(float ver, float hor, out Quaternion swayX, out Quaternion swayY)
    {
        swayX = Quaternion.AngleAxis(-hor, holder.transform.right);
        swayY = Quaternion.AngleAxis(ver, holder.transform.up);
    }

    private void swayback(float delta)
    {
        holder.transform.localRotation = Quaternion.Slerp(holder.transform.localRotation, localstartrot, coolingdown * delta);
    }
}
