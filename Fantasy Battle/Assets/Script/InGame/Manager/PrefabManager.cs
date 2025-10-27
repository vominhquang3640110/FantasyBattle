using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class PrefabManager : MonoBehaviour
    {
        public static PrefabManager instance;

        [Header("Card Prefab Data")]
        [Header("Equip Basic")]
        public GameObject _EquipBasic;
        [Header("Card Back")]
        public GameObject _CardBack;
        [Header("Start Card")]
        public GameObject _Mukurin;
        public GameObject _Slime;
        [Header("Item Card")]
        public GameObject _BloodCuffs;
        public GameObject _Bread;
        public GameObject _CursedBook;
        public GameObject _ItemBackpack;
        public GameObject _MysteriousBackpack;
        public GameObject _MagicRing;
        public GameObject _ManaBottle;
        public GameObject _MysteriousPendulum;
        public GameObject _RoyalShield;
        public GameObject _TheHopeDice;
        [Header("Magnus Card")]
        public GameObject _GOA;
        public GameObject _THFH;
        [Header("Assassin EE Card")]
        public GameObject _AssassinMain;
        public GameObject _BloodMask;
        public GameObject _HiredAssassin;
        public GameObject _LonelyWolf;
        public GameObject _Sniper;
        public GameObject _Barret;
        public GameObject _BladeDance;
        public GameObject _EdgeSword;
        public GameObject _NightBlade;
        public GameObject _ScytheBloody; 
        public GameObject _Stiletto;
        [Header("Slime EE Card")]
        public GameObject _AquaSlime;
        public GameObject _AxitSlime;
        public GameObject _IceSlime;
        public GameObject _ShiledSlime;
        public GameObject _SoulSlime;
        public GameObject _MergeSword;
        public GameObject _PowerGloves;
        public GameObject _SlimeOfEyes;
        public GameObject _SonicBoom;
        public GameObject _TwinIceSwords;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {

        }

        public GameObject FindCardPrefab(string cardName)
        {
            //Equip Basic
            if (cardName == "EquipBasic")
            {
                return _EquipBasic;
            }
            //Card Back
            else if (cardName == "Card Back")
            {
                return _CardBack;
            }
            //Start
            else if (cardName == "Mukurin")
            {
                return _Mukurin;
            }
            else if (cardName == "Slime")
            {
                return _Slime;
            }
            //Item
            else if (cardName == "Blood Cuffs (Item)")
            {
                return _BloodCuffs;
            }
            else if (cardName == "Bread (Item)")
            {
                return _Bread;
            }
            else if (cardName == "Cursed Book (Item)")
            {
                return _CursedBook;
            }
            else if (cardName == "Item Backpack (Item)")
            {
                return _ItemBackpack;
            }
            else if (cardName == "Mysterious Backpack (Item)")
            {
                return _MysteriousBackpack;
            }
            else if (cardName == "Magic Ring (Item)")
            {
                return _MagicRing;
            }
            else if (cardName == "Mana Bottle (Item)")
            {
                return _ManaBottle;
            }
            else if (cardName == "Mysterious Pendulum (Item)")
            {
                return _MysteriousPendulum;
            }
            else if (cardName == "Royal Shield (Item)")
            {
                return _RoyalShield;
            }
            else if (cardName == "The Hope Dice (Item)")
            {
                return _TheHopeDice;
            }
            //Magnus
            else if (cardName == "GOA (M)")
            {
                return _GOA;
            }
            else if (cardName == "THFH (M)")
            {
                return _THFH;
            }
            //Assassin
            else if (cardName == "Assassin Main")
            {
                return _AssassinMain;
            }
            else if (cardName == "Blood Mask")
            {
                return _BloodMask;
            }
            else if (cardName == "Hired Assassin")
            {
                return _HiredAssassin;
            }
            else if (cardName == "Lonely Wolf")
            {
                return _LonelyWolf;
            }
            else if (cardName == "Sniper")
            {
                return _Sniper;
            }
            else if (cardName == "Barret (E)")
            {
                return _Barret;
            }
            else if (cardName == "Blade Dance (E)")
            {
                return _BladeDance;
            }
            else if (cardName == "Edge Sword (E)")
            {
                return _EdgeSword;
            }
            else if (cardName == "Night Blade (E)")
            {
                return _NightBlade;
            }
            else if (cardName == "Scythe Bloody (E)")
            {
                return _ScytheBloody;
            }
            else if (cardName == "Stiletto (E)")
            {
                return _Stiletto;
            }
            //Slime
            else if (cardName == "Aqua Slime")
            {
                return _AquaSlime;
            }
            else if (cardName == "Axit Slime")
            {
                return _AxitSlime;
            }
            else if (cardName == "Ice Slime")
            {
                return _IceSlime;
            }
            else if (cardName == "Shiled Slime")
            {
                return _ShiledSlime;
            }
            else if (cardName == "Soul Slime")
            {
                return _SoulSlime;
            }
            else if (cardName == "Merge Sword (E)")
            {
                return _MergeSword;
            }
            else if (cardName == "Power Gloves (E)")
            {
                return _PowerGloves;
            }
            else if (cardName == "Slime Of Eyes (E)")
            {
                return _SlimeOfEyes;
            }
            else if (cardName == "Sonic Boom (E)")
            {
                return _SonicBoom;
            }
            else if (cardName == "Twin Ice Swords (E)")
            {
                return _TwinIceSwords;
            }
            return null;
        }
    }

}