using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterSingleton : MonoBehaviour
    {
        public static CharacterSingleton singleton;

        public Material skinMaterial;
        public Material shirtMaterial;
        public Material pantsMaterial;
        public Material shoesMaterial;
        public Material eyeMaterial;

        private void Awake()
        {
            if (singleton)
            {
                Destroy(gameObject);
                return;
            }
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            //Initialize the singleton with the default look
/*            skinMaterial = Resources.Load("Materials/character/skin/Skin_01", typeof(Material)) as Material;
            shirtMaterial = Resources.Load("Materials/character/shirt/Shirt_01", typeof(Material)) as Material;
            pantsMaterial = Resources.Load("Materials/character/pants/Pants_01", typeof(Material)) as Material;
            shoesMaterial = Resources.Load("Materials/character/shoes/Shoes_01", typeof(Material)) as Material;
            eyeMaterial = Resources.Load("Materials/character/eye/Eye_01", typeof(Material)) as Material;*/
        }

        public void SetMaterial(int slot, Material mat)
        {
          switch(slot)
            {
                case 1:
                    skinMaterial = mat;
                    break;
                case 2:
                    eyeMaterial = mat;
                    break;
                case 3:
                    shirtMaterial = mat;
                    break;
                case 4:
                    pantsMaterial = mat;
                    break;
                case 5:
                    shoesMaterial = mat;
                    break;
            }
        }
    }
}
