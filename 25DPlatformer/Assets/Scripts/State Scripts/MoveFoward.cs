using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer
{
    [CreateAssetMenu(fileName = "New State", menuName = "MyAssets/AbilityData/MoveFoward")]
    public class MoveFoward : StateData
    {
        public float Speed;
        public override void UpdateAbility(CharacterState characterState, Animator animator)
        {

            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.MoveRight && control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (!control.MoveRight && !control.MoveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (control.MoveRight)
            {
                control.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (control.MoveLeft)
            {
                control.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                control.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            }
        }
    }

}
