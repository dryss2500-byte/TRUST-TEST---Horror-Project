using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Transform))]
public class Player_headbob : MonoBehaviour
{
    [Header("other")]
    [SerializeField]
    private Transform holder;
    public Vector3 startpos;




    [Range(0, 10.0f)]
    public float radius, freq, smoothness;

    private void Awake()
    {
        bool flowControl = set_Pos();
        if (!flowControl)
        {
            return;
        }
    }

    private bool set_Pos()
    {
        if (startpos != holder.transform.localPosition) startpos = holder.transform.localPosition;
        else return false;
        return true;
    }

    private void Update()
    {


        if (!Player_Movement.Instance.moving) holder.transform.localPosition = Vector3.Lerp(holder.transform.localPosition, startpos, smoothness * Time.deltaTime);
        else
        {
            float sine_returned = return_sine();
            float ynew = holder.transform.localPosition.y - sine_returned;
            float xnew = holder.transform.localPosition.x + sine_returned;
            holder.transform.localPosition = new Vector3(transform.localPosition.x/*xnew*/, ynew, holder.transform.localPosition.z);
            /*Debug.Log(ynew);*/
        }


    }

    float return_sine()
    {
        return Mathf.Sin(Time.time * freq) * radius;
    }
}