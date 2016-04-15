using UnityEngine;
using System.Collections;

public class water : MonoBehaviour
{
    public GameObject waterDirt;

    void OnMouseDown()
    {
        doEffect();
        
    }

    void doEffect()
    {
        Vector3 v2 = Input.mousePosition;
        v2.z = 1f;

        v2 = Camera.main.ScreenToWorldPoint(v2);

        GameObject instance = (GameObject)Instantiate(waterDirt, v2, Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), 15f));

        StartCoroutine(DisableRigidbody(instance));
        StartCoroutine(DestroyInstance(instance));
    }

    IEnumerator DestroyInstance(GameObject instance)
    {
        yield return new WaitForSeconds(1f);

        instance.GetComponent<Animator>().Play("shrinkAnim");

        yield return new WaitForSeconds(1f);

        Destroy(instance);
    }

    IEnumerator DisableRigidbody(GameObject instance)
    {
        yield return new WaitForSeconds(0.65f);
        instance.GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
