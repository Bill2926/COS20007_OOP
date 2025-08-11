using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterTask
{
    public class Clock
    {
        //field
        Counter _second = new Counter("second", 0);
        Counter _minute = new Counter("minute", 0);
        Counter _hour = new Counter("hour", 12);
        string _period = "AM";

        //constructor

        //method
        public void TimeIncrease()
        {
            _second.Increment();

            if (_second.Ticks > 59)
            {
                _second.Reset();
                _minute.Increment();

                if (_minute.Ticks > 59)
                {
                    _minute.Reset();
                    _hour.Increment();

                    if (_hour.Ticks > 12) // 12-hour format
                    {
                        _hour.SetCount(1);
                    }
                    else if (_hour.Ticks == 12 && _minute.Ticks == 0 && _second.Ticks == 0)
                    {
                        _period = _period == "AM" ? "PM" : "AM";
                    }
                }
            }
        }

        public string ClockDisplay()
        {
            return $"{_hour.Ticks:D2}:{_minute.Ticks:D2}:{_second.Ticks:D2} {_period}";
        }
    }
}
