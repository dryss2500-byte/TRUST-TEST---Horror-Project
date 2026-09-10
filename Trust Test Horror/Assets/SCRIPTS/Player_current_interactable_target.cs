using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Player_current_interactable_target : MonoBehaviour
{
    public Camera main;
    Plane[] main_calculated;
    public Collider []objects;
    public Collider selected;

    private void Awake()
    {
        main = Camera.main;




    }




    private void Update()
    {
        main_calculated = GeometryUtility.CalculateFrustumPlanes(main);
        Collider flowcontrol = inside_main_bound();
        Debug.Log(flowcontrol);
    }

    private float cond_by_distance(Collider obj)
    {
        return (main.transform.position.sqrMagnitude - obj.gameObject.transform.position.sqrMagnitude);
    }

    Collider inside_main_bound(){
        foreach (Collider c in objects)
        {



           float distance = cond_by_distance((Collider)c);
            Debug.Log(c.gameObject.name + " " + distance);

            //DAY 1 LOG: if the distance between the camera and the object is less than 5, attribute variable (selected) to the obj collider, check if there isn't another close by object, discard one of them

        }
        if (GeometryUtility.TestPlanesAABB(main_calculated, selected.bounds)) return selected; else return selected;

    }
}