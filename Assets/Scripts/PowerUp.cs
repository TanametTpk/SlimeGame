using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private BuffBase buff;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            BuffSystem playerBuffStatus = other.gameObject.GetComponent<BuffSystem>();

            if (playerBuffStatus == null) return;
            playerBuffStatus.Add(buff);
            AudioManager.instance.Play("CollectItem");
            Destroy(gameObject);
        }
    }
}
