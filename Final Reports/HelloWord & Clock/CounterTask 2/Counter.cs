using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace CounterTask
{
    public class Counter
    {
        // fields
        int _count;
        string _name;

        public Counter(string name, int count) //Constructor
        {
            _name = name;
            _count = count;
        }

        //methods
        public void Increment()
        {
            _count++;
        }

        public void Reset()
        {
            _count = 0;
        }
        public void SetCount(int value)
        {
            _count = value;
        }

        //public void ResetByDefault()
        //{
        //    unchecked
        //    {
        //        _count = (int)2147483647489; //105547489
        //    }
        //}

        //properties
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int Ticks
        {
            get
            {
                return _count;
            }
        }
    }
}
