using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    [Serializable]
    public class UserModel
    {
        public bool auth;
        public string token;
        public string userid;
        public string email;
        public string displayName;
        public List<String> scannedQrs;
        public CharacterModel characterModel;
    }
}
