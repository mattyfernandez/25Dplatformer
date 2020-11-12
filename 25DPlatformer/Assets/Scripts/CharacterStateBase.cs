﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer
{
    public class CharacterStateBase : StateMachineBehaviour
    {
        private CharacterControl characterControl;
        public CharacterControl GetCharacterControl(Animator animator)
        {
            if(characterControl == null)
            {
                characterControl = animator.GetComponentInParent<CharacterControl>();
            }
            return characterControl;
        }
    }

}
