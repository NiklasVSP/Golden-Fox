using System;
using System.Collections.Generic;

namespace GoldenFox.Internal.Constraints
{
    public class Except : IConstraint
    {
        public List<DayOfWeek> UnscheduledDays { get; set; }

        public Except(List<DayOfWeek> unscheduledDays)
        {
            UnscheduledDays = unscheduledDays;
        }

        public ConstraintResult Contains(DateTime dateTime)
        {
            var passed = true;
            var closestValidFutureInput = dateTime;
            foreach (var day in UnscheduledDays)
            {
                if (dateTime.DayOfWeek == day)
                {
                    passed = false;

                    var closestValidateInputIsAlsoInUnscheduledDays = true;
                    closestValidFutureInput = dateTime.SetTime(new Timestamp());
                    while (closestValidateInputIsAlsoInUnscheduledDays)
                    {
                        closestValidFutureInput = closestValidFutureInput.AddDays(1);
                        if (!UnscheduledDays.Contains(closestValidFutureInput.DayOfWeek))
                        {
                            closestValidateInputIsAlsoInUnscheduledDays = false;
                        }

                    }
                }
            }

            return new ConstraintResult
            {
                Passed = passed,
                ClosestValidFutureInput = closestValidFutureInput

            };

        }
    }
}