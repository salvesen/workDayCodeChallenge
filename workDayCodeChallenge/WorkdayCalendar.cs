using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workDayCodeChallenge
{
    public class WorkdayCalendar
    {
        public static DateTime workdayStart;
        public static DateTime workdayStop;
        public static List<DateTime> vacation = new List<DateTime>();
        public static List<DateTime> recurringVacation = new List<DateTime>();
        public static DateTime returningDate;

        public void setHoliday(DateTime date)
        {
            //This dat is considered a holliday for this year
            vacation.Add(date);
        }

        public void setRecurringHoliday(DateTime date)
        {
            //This dat is considered a holliday for all years to come
            recurringVacation.Add(date);
        }

        public void setWorkdayStartAndStop(DateTime start, DateTime stop)
        {
            //Set start date
            workdayStart = start;
            //Set stop date
            workdayStop = stop;
        }

        //getWorkdayIncrement(Date startDate, float incrementInWorkdays)
        public DateTime getWorkdayIncrement(DateTime startDate, double incrementInWorkdays)
        {
            //Find all of the red days betweeen startDate and the increment
            int redDays = getRedDays(startDate, incrementInWorkdays);

            //Add the red days to our increment
            incrementInWorkdays += redDays;

            //Check if we are before, after or in a working day
            //After working hours
            if (startDate.TimeOfDay >= workdayStop.TimeOfDay)
            {

                //Add days to the returning date
                returningDate = addFullDaysToDate(startDate, incrementInWorkdays);

                //Set the returning date time to start of a working day
                returningDate = setDateToWorkStartTime(returningDate);

                //Set the returning date hours worked for last day
                returningDate = returningDate.AddHours(getHoursToAdd(incrementInWorkdays));
            }

            //Before working hours
            else if (startDate.TimeOfDay <= workdayStart.TimeOfDay)
            {
                //Add days to the returning date
                returningDate = addFullDaysToDate(startDate, incrementInWorkdays-1);

                //Set the returning date time to start of a working day
                returningDate = setDateToWorkStartTime(returningDate);

                //Set the returning date hours worked for last day
                returningDate = returningDate.AddHours(getHoursToAdd(incrementInWorkdays));
            }

            //Between working hours
            else
            {
                //Add days to the returning date
                returningDate = addFullDaysToDate(startDate, incrementInWorkdays - 1);

                //Set the returning date time to start time
                returningDate = setDateToPreviousStartTime(startDate);

                //Set the returning date hours worked for last day
                returningDate = returningDate.AddHours(getHoursToAdd(incrementInWorkdays));

                //We passed the working hours and need to add a day
                if (returningDate.TimeOfDay > workdayStop.TimeOfDay)
                {
                    returningDate = passedWorkingHoursAddDay(returningDate);
                }

            }
            return returningDate;
        }


        private DateTime passedWorkingHoursAddDay(DateTime date)
        {
            //Add a day
            date = date.AddDays(1);
            //Get seconds for next day
            double getSeconds = (date.TimeOfDay - workdayStop.TimeOfDay).TotalSeconds;
            //Reset to startingtime
            date = setDateToWorkStartTime(date);
            //Set the working time for the day
            date = date.AddSeconds(getSeconds);
            return date;
        }


        private double getHoursToAdd(double increment)
        {
            //Handle negative increment
            if (increment < 0)
            {
                //Get the working hours
                return (1 - ((increment * -1) - Math.Truncate(increment * -1))) * getWorkingHours();
            }

            //Handle positive increment
            else
            {
                //Get the working hours
                return (increment - Math.Truncate(increment)) * getWorkingHours();
            }
        }

        private DateTime setDateToPreviousStartTime(DateTime date)
        {
            //Set time to previous started time
            return new DateTime(returningDate.Year, returningDate.Month, returningDate.Day, date.Hour, date.Minute, date.Second);
        }

        private DateTime setDateToWorkStartTime(DateTime date)
        {
            //Set time to start of day
            return new DateTime(date.Year, date.Month, date.Day, workdayStart.Hour, workdayStart.Minute, workdayStart.Second);
        }


        private DateTime addFullDaysToDate(DateTime startTime, double increment)
        {
            return startTime.AddDays(Math.Ceiling(increment));
        }

        private int getWorkingHours()
        {
            return workdayStop.Hour - workdayStart.Hour;
        }

        private int getRedDays(DateTime start, double increment)
        {
            //red days hound
            int redDays = 0;
            //Handle positive increment
            if (increment >= 0)
            {
                //Go trough all of the dates and check if it is a red day
                for (int i = 0; i <= increment; i++)
                {
                    //If the day is in the weekend or any any vacation days
                    if (start.AddDays(i).DayOfWeek == DayOfWeek.Saturday || start.AddDays(i).DayOfWeek == DayOfWeek.Sunday || vacation.Contains(start.AddDays(i).Date) || recurringVacation.Contains(start.AddDays(i).Date))
                    {
                        //Increase the increment as we need to look at more dates
                        increment++;
                        //add a red day
                        redDays++;
                    }
                }
            }
            //Handle negative increment
            else
            {
                increment = increment * (-1);
                //Go trough all of the dates and check if it is a red day
                for (int i = 0; i <= increment; i++)
                {
                    //If the day is in the weekend or any any vacation days
                    if (start.AddDays(-i).DayOfWeek == DayOfWeek.Saturday || start.AddDays(-i).DayOfWeek == DayOfWeek.Sunday || vacation.Contains(start.AddDays(-i).Date) || recurringVacation.Contains(start.AddDays(-i).Date))
                    {
                        //Increase the increment as we need to look at more dates
                        increment++;
                        //add a red day
                        redDays++;
                    }
                }
                //Make red days negative
                redDays = redDays * -1;
            }
            return redDays;
        }

    }
}
