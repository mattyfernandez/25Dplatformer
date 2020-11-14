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

        public void ResetInfo(Attack attack)
        {
            IsRegisterd = false;
            IsFinished = false;
            AttackAbility = attack;
        }

        public void Register(Attack attack, CharacterControl attacker)
        {
            IsRegisterd = true;
            Attacker = attacker;

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
