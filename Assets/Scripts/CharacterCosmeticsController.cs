using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterCosmeticsController : MonoBehaviour
    {
        public MeshRenderer skin;
        public MeshRenderer shirt;
        public MeshRenderer pants;
        public MeshRenderer shoes;
        public MeshRenderer eye;

        private void Start()
        {
            skin.material = CharacterSingleton.singleton.skinMaterial;
            shirt.material = CharacterSingleton.singleton.shirtMaterial;
            pants.material = CharacterSingleton.singleton.pantsMaterial;
            shoes.material = CharacterSingleton.singleton.shoesMaterial;
            eye.material = CharacterSingleton.singleton.eyeMaterial;
        }

        public void SetMaterial(int slot, Material mat)
        {
            switch (slot)
            {
                case 1:
                    skin.material = mat;
                    break;
                case 2:
                    eye.material = mat;
                    break;
                case 3:
                    shirt.material = mat;
                    break;
                case 4:
                    pants.material = mat;
                    break;
                case 5:
                    shoes.material = mat;
                    break;
            }
        }
    }
}
