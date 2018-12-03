using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainProject
{
    class Format
    {
        public string formatDateCorrectly(string FormatDate) //2018-11-27T15:07:00+0100
        {
            List<char> dateOnly = new List<char>();

            for (int i = 0; i < 10; i++)
            {
                if (FormatDate[i] == '-')
                {
                    dateOnly.Add('.');
                }
                else
                {
                    dateOnly.Add(FormatDate[i]);
                }
            }

            List<char> timeOnly = new List<char>();

            for (int i = 11; i < 16; i++)
            {
                timeOnly.Add(FormatDate[i]);
            }

            FormatDate = "";

            foreach (var item in dateOnly)
            {
                FormatDate += item;
            }
            FormatDate += " ";
            foreach (var item in timeOnly)
            {
                FormatDate += item;
            }

            return FormatDate;
        }
        public string formatCoordinatesCorrectly(string coordinates)
        {
            StringBuilder sb = new StringBuilder(coordinates);
            for (int i = 0; i < coordinates.Length; i++)
            {
                if (coordinates[i] == ',')
                {
                    sb[i] = '.'; ;
                }
            }
            coordinates = sb.ToString();
            return coordinates;
        }
    }
}
