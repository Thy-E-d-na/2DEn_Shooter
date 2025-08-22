using System.Collections;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;


    private void Start()
    {
        StartCoroutine(CheckEggPosition());
    }

    private IEnumerator CheckEggPosition()
    {
        while (true)
        {
            Vector3 viewPort = Camera.main.WorldToViewportPoint(transform.position);
            if (viewPort.y < 0.05f)
            {
                anim.SetTrigger("Break");
                AudioController.Instance.PlayEggBreakClip();
                rb.bodyType = RigidbodyType2D.Static;
                Destroy(gameObject, 1f);
                break;
            }
            yield return null;
        }
    }
}