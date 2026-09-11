using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Player_current_interactable_target : MonoBehaviour
{
    public Camera main;
    Plane[] main_calculated;
    public Collider []objects;
    public Collider selected;
    [SerializeField] private bool closest_inside_bounds;
    private Collider noutil;

    private void Awake()
    {
        main = Camera.main;




    }




    private void Update()
    {
        main_calculated = GeometryUtility.CalculateFrustumPlanes(main);
         bool none_in_view = check_if_noneinview(); if (!none_in_view) selected = null;
        Collider flowcontrol = inside_main_bound();
        Debug.Log($"{flowcontrol} - has none in view : {none_in_view}");
    }

    private float cond_by_distance(Collider obj)
    {
        return (main.transform.position.sqrMagnitude - obj.gameObject.transform.position.sqrMagnitude);
    }


    bool check_if_noneinview()
    {
        bool returnvalue = false;
        bool has_atleast_one_object = false;
        if (selected != null)
        {

            for(int i = 0; i < objects.Length; i++)
            {
                bool result = GeometryUtility.TestPlanesAABB(main_calculated, objects[i].bounds);
                if (result) has_atleast_one_object = true;
                if (i >= objects.Length && !has_atleast_one_object) returnvalue = false; else returnvalue = true;

            }



        }


        return returnvalue;
    }



    Collider inside_main_bound(){
        foreach (Collider c in objects)
        {



            float distance = cond_by_distance((Collider)c);
            set_selected_inside_bounds();
            //Debug.Log(c.gameObject.name + " " + distance);

            set_on_distance(c, distance);


            //DAY 1 LOG: if the distance between the camera and the object is less than 5, attribute variable (selected) to the obj collider, check if there isn't another close by object, discard one of them

        }
        if (selected != null) if (GeometryUtility.TestPlanesAABB(main_calculated, selected.bounds)) noutil = selected; else noutil = null;
        return noutil;

    }

    private void set_selected_inside_bounds()
    {
        if (selected = null) return;
        closest_inside_bounds = cond_by_distance(selected) <= 5f && !closest_inside_bounds;
    }

    void set_on_distance(Collider obj, float distance) {
        if (distance > 5 || !closest_inside_bounds) return; else {
            if (selected != null) return;
            if (closest_inside_bounds) selected = obj; else selected = null;
        }
        }

    //DAY 2 LOG: if it works it works, don't touch it
}