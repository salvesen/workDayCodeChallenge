using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workDayCodeChallenge
{
    public class tests
    {

        public static string response1 = "27-07-2004 13:47";
        public static string response2 = "13-05-2004 10:01";
        public static string response3 = "10-06-2004 14:18";
        public static string response4 = "04-06-2004 10:12";
        public static string response5 = "11-06-2004 08:18";
        public static string response6 = "14-05-2004 12:00";

        public static WorkdayCalendar workDayCalender = new WorkdayCalendar();

        private static void setMainData()
        {
            workDayCalender.setHoliday(new DateTime(2004, 05, 17, 0, 0, 0));
            workDayCalender.setRecurringHoliday(new DateTime(2004, 05, 27, 0, 0, 0));
            workDayCalender.setWorkdayStartAndStop(new DateTime(2004, 01, 01, 8, 0, 0), new DateTime(2004, 01, 01, 16, 0, 0));
        }

        private static void handleLastData(DateTime start, double increment, string correctResponse)
        {
            DateTime response = workDayCalender.getWorkdayIncrement(start, increment);
            string responseString = response.ToString("dd-MM-yyyy HH:mm:ss");
            Console.WriteLine(start.ToString("dd-MM-yyyy HH:mm:ss") + " with the addition of " + increment + " working days is " + response.ToString("dd-MM-yyyy HH:mm:ss"));
            Console.Write("Expected result? ");
            Console.WriteLine(responseString.Contains(correctResponse));
        }

        public static void runTestOne()
        {
            setMainData();
            DateTime start = new DateTime(2004, 05, 24, 19, 03, 0);
            double increment = 44.723656;
            handleLastData(start, increment, response1);
        }

        public static void runTestTwo()
        {
            setMainData();
            DateTime start = new DateTime(2004, 05, 24, 18, 03, 0);
            double increment = -6.7470217;
            handleLastData(start, increment, response2);
        }

        public static void runTestThree()
        {
            setMainData();
            DateTime start = new DateTime(2004, 05, 24, 08, 03, 0);
            double increment = 12.782709;
            handleLastData(start, increment, response3);
        }

        public static void runTestFour()
        {
            setMainData();
            DateTime start = new DateTime(2004, 05, 24, 07, 03, 0);
            double increment = 8.276628;
            handleLastData(start, increment, response4);
        }

        public static void runTestFive()
        {
            setMainData();
            DateTime start = new DateTime(2004, 05, 24, 10, 03, 0);
            double increment = 12.782709;
            handleLastData(start, increment, response5);
        }

        public static void runTesSix()
        {
            setMainData();
            DateTime start = new DateTime(2004, 05, 24, 18, 06, 0);
            double increment = -5.5;
            handleLastData(start, increment, response6);
        }
    }
}
