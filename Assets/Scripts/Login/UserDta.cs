using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirtableUnity.PX.Model
{
    public class UserField
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserRecord<T>
    {
        public string id;
        public UserField fields;
        public string createdTime;
    }
}
