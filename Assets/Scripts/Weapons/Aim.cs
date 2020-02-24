using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour
{
    public Animator animator;
    private Vector3 mouse_pos;
    public Transform target;
    private Vector3 object_pos;
    public GameObject weapon_sprite;
    public GameObject player_sprite;
    public float angle;

    void Update()
    {
        if (Time.timeScale != 0)
        {
            mouse_pos = Input.mousePosition;
            object_pos = Camera.main.WorldToScreenPoint(target.position);
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        //fliper
        if (angle > 90 || angle < -90)
        {
            weapon_sprite.GetComponent<SpriteRenderer>().flipY = true;
            player_sprite.GetComponent<SpriteRenderer>().flipX = true;

            animator.SetBool("lookRight", false);
        }
        else
        {
            weapon_sprite.GetComponent<SpriteRenderer>().flipY = false;
            player_sprite.GetComponent<SpriteRenderer>().flipX = false;

            animator.SetBool("lookRight", true);
        }
    }
}
