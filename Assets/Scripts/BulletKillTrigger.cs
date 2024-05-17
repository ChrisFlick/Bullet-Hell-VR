using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKillTrigger : MonoBehaviour
{
    public static event EventHandler OnAnyDodged;

    private void OnTriggerEnter(Collider other)
    {
        // Destroy bullets when they reach an invisible wall behind the player.

        if (other.gameObject.tag != "Bullet") return;

        Destroy(other.gameObject);

        OnAnyDodged?.Invoke(this, EventArgs.Empty);
    }
}
