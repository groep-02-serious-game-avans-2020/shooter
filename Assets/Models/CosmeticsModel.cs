using Assets.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    [Serializable]
    class CosmeticsModel
    {
        public string cosmeticId { get; set; }
        public CosmeticType cosmeticType { get; set; }
        public string name { get; set; }
    }
}
