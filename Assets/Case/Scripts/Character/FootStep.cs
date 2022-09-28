using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case.Characters.Sound
{
    public class FootStep : MonoBehaviour
    {
        [SerializeField] private AudioSource _sound;
        private void Step()
        {
            _sound.Stop();
            _sound.Play();
        }
    }

}
