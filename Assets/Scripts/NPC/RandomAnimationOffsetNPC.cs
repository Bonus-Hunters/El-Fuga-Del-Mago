using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.NPC
{
    public class RandomAnimationOffsetNPC : MonoBehaviour
    {
        private Animator animator;
        void Start()
        {
            animator = GetComponent<Animator>();

            // Only randomize if the Animator is valid
            if (animator != null)
            {
                float randomOffset = UnityEngine.Random.Range(0f, 1f);

                // Apply random normalized time to the current animation layer 0 state
                animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, randomOffset);
            }
        }
    }
}
