using System;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    [Serializable]
    public struct Armor
    {
        [Range(1, 3)] public int ArmorType;
        public float BaseArmor;
        
        private float _armor;
        
        public float CurrentArmor
        {
            get { return BaseArmor + _armor; }
            set
            {
                _armor = value;
                OnArmorChanged?.Invoke();
            }
        }

        public UnityAction OnArmorChanged;
        
        public float CalculateDamage(float damage)
        {
            float coef = 1;
            switch (ArmorType)
            {
                case 1:
                    coef = 1.2f;
                    break;
                case 2:
                    coef = 1.5f;
                    break;
                case 3:
                    coef = 2.0f;
                    break;
            }

            if (CurrentArmor <= 0)
            {
                coef = 1;
            }

            var newDamage = damage / coef;
            var armorDamage = Mathf.Min(newDamage, CurrentArmor);
            var hpDamage = newDamage - armorDamage;
            _armor -= armorDamage;
            return hpDamage;
        }

        public void AddArmor(float armor)
        {
            _armor = armor;
        }

    }
}