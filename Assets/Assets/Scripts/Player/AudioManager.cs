using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _moneyCollect;
    [SerializeField] private AudioClip _badCollect;
    [SerializeField] private AudioClip _flagsSound;
    [SerializeField] private AudioSource _audioSource;
	
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Money")
        {
            _audioSource.clip = _moneyCollect;
            _audioSource.Play();
        }

        if (other.tag == "Bottle")
        {
            _audioSource.clip = _badCollect;
            _audioSource.Play();
        }
        if (other.tag == "Obstacle")
        {
            _audioSource.clip = _flagsSound;
            _audioSource.Play();
        }
    }
}
