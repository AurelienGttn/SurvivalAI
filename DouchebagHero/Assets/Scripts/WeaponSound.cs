using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour {

    public AudioClip axeSound, pickaxeSound, hammerSound, swordSound;
    public GameObject axe, pickaxe, hammer;
    private AudioSource source;
    private bool worker = false, enemy = false, hero = false;

    private void Start()
    {
        // Check current unit type
        if (CompareTag("Worker"))
            worker = true;
        if (CompareTag("Player"))
            hero = true;
        if (CompareTag("Enemy"))
            enemy = true;

        source = GetComponentInChildren<AudioSource>();
    }

    // Method called during animation to play a sound
    public void PlayWeaponSound()
    {
        if (worker)
        {
            if (axe.activeSelf)
                source.PlayOneShot(axeSound);
            if (pickaxe.activeSelf)
                source.PlayOneShot(pickaxeSound);
            if (hammer.activeSelf)
                source.PlayOneShot(hammerSound);
        }

        if (hero || enemy)
        {
            source.PlayOneShot(swordSound);
        }
    }
}
