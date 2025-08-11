using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class IdenObj
    {
        //fields
        private List<string> _identifiers;
        string _myStudentID = "7489";

        //constructor
        public IdenObj(string[] idents)
        {
            _identifiers = new List<string>();
            if (idents != null)
            {
                for (int i = 0; i < idents.Length; i++)
                {
                    _identifiers.Add(idents[i].ToLower());
                }
            }
        }

        //methods
        public bool AreYou(string id)
        {
            return _identifiers.Contains(id.ToLower());
        }

        public string FirstId
        {
            get
            {
                if( _identifiers.Count == 0)
                {
                    return "";
                } else { return _identifiers.First(); }
            }
        }

        public void AddIdentifier(string id)
        {
            _identifiers.Add(id.ToLower());
        }

        public void PrivilegeEscalation(string pin)
        {
            if(pin.Length == 4)
            {
                if(pin == _myStudentID) //105547489
                {
                    _identifiers[0] = _myStudentID;
                }
            }
            else
            {
                return;
            }

        }
    }
}
