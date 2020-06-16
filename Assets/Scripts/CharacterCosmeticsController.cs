using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class CharacterCosmeticsController : MonoBehaviour
    {
        public Material skinMaterial;
        public Material shirtMaterial;
        public Material pantsMaterial;
        public Material shoesMaterial;
        public Material eyeMaterial;

        public MeshRenderer skin;
        public MeshRenderer shirt;
        public MeshRenderer pants;
        public MeshRenderer shoes;
        public MeshRenderer eye;

        private void Start()
        {
            skin.material = skinMaterial;
            shirt.material = shirtMaterial;
            pants.material = pantsMaterial;
            shoes.material = shoesMaterial;
            eye.material = eyeMaterial;
        }
    }
}
