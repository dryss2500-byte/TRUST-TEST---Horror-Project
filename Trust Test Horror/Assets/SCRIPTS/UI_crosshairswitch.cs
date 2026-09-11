using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_crosshairswitch : MonoBehaviour
{
    [Header("textures")]
    [field: SerializeField] public Texture2D crosshair_1;
    [field: SerializeField] public Texture2D crosshair_2;
    [Header("noutil")]
    private Player_check_closest_object detector;
    private RawImage rawimage;
    [SerializeField]
    private bool has_interaction_pending;

    private void Awake() {
        rawimage = GetComponent<RawImage>();
    }
    private void Update()
    {
        rawimage.texture = !has_interaction_pending ? crosshair_1 : crosshair_2;
        RectTransform rect = rawimage.GetComponent<RectTransform>();
        float t = 5.0f;

        Vector3 final_selected = new Vector3(.5f, .5f, .5f);
        Vector3 final_unselected = Vector3.one;

        Vector3 scale = new Vector3();
        if (has_interaction_pending) scale = final_selected; else scale = final_unselected;

        set_scale(rect, t, scale);

        static void set_scale(RectTransform rect, float t, Vector3 selection)
        {
            rect.transform.localScale = Vector3.Lerp(rect.transform.localScale, selection, t * Time.deltaTime);
        }
    }
    private void LateUpdate()
    {
        has_interaction_pending = FindAnyObjectByType<Player_check_closest_object>().has_detected;
    }
}