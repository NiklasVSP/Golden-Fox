﻿using System;

namespace GoldenFox.NewModel
{
    public class DayInMonth : Interval
    {
        private readonly int _day;

        private readonly Timestamp _timestamp;

        public DayInMonth(int day, Timestamp timestamp)
        {
            _day = day;
            _timestamp = timestamp;
        }

        public override DateTime Evaluate(DateTime from, bool includeNow = false)
        {
            var comparison = _timestamp.CompareTo(from);

            if (includeNow && from.Day == _day && comparison == 0)
            {
                return from;
            }
            else
            {
                int daysToAdd;
                if (comparison >= 0 && _day == @from.Day)
                {
                    if (comparison == 0 && includeNow)
                    {
                        // Same day, just later
                        daysToAdd = 0;
                    }
                    else if (comparison == 0 && !includeNow)
                    {
                        daysToAdd = DateTime.DaysInMonth(@from.Year, @from.Month) - @from.Day + _day;
                    }
                    else
                    {
                        daysToAdd = 0;
                    }
                }
                else
                {
                    // next occurence of day of month
                    var dayDiff = (DateTime.DaysInMonth(@from.Year, @from.Month) - @from.Day + _day) % DateTime.DaysInMonth(@from.Year, @from.Month) + (_day < 0 ? 1 : 0);
                    if (dayDiff == 0)
                    {
                        daysToAdd = DateTime.DaysInMonth(@from.Year, @from.Month) - @from.Day + _day;
                    }
                    else
                    {
                        daysToAdd = dayDiff;
                    }
                }
                return from.AddDays(daysToAdd).SetTime(_timestamp);
            }
        }
    }
}
