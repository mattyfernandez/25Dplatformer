using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer
{
    public enum TransitionParameter
    {
        Move,
        Jump,
        ForceTransition,
    }

    public class CharacterControl : MonoBehaviour
    {
        public float Speed;
        public Animator animator;
        public Material material;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;

        public void ChangeMaterial()
        {
            if (material == null)
            {
                Debug.LogError("No Material Specify.");
            }

            Renderer[] arrMaterials = this.gameObject.GetComponentsInChildren<Renderer>();

            foreach(Renderer r in arrMaterials)
            {
                if(r.gameObject != this.gameObject)
                {

                    r.material = material;
                }
            }
        }
    }

}