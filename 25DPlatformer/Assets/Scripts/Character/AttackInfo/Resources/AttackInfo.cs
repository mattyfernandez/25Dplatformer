using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer
{
    public class AttackInfo : MonoBehaviour
    {
        public CharacterControl Attacker = null;
        public Attack AttackAbility;
        public List<string> ColliderNames = new List<string>();
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public int CurrentHits;
        public bool IsRegisterd;
        public bool IsFinished;

        public void ResetInfo(Attack attack, CharacterControl attacker)
        {
            IsRegisterd = false;
            IsFinished = false;
            AttackAbility = attack;
            Attacker = attacker;
        }

        public void Register(Attack attack)
        {
            IsRegisterd = true;

            AttackAbility = attack;
            ColliderNames = attack.ColliderNames;
            MustCollide = attack.MustCollide;
            MustFaceAttacker = attack.MustFaceAttacker;
            LethalRange = attack.LethalRange;
            MaxHits = attack.MaxHits;
            CurrentHits = 0;
        }

    }
}
