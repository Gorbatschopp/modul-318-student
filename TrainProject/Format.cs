namespace TrainProject
{
    using System.Collections.Generic;
    using System.Text;

    public class Format
    {
        /// <summary>
        /// Diese Funktion formatiert das Datum welches man von der API bekommt so, dass sie schön ist.
        /// </summary>
        /// <param name="formatDate"></param>
        /// <returns></returns>
        public string FormatDateCorrectly(string formatDate) //2018-11-27T15:07:00+0100
        {
            List<char> dateOnly = new List<char>();

            for (int i = 0; i < 10; i++)
            {
                if (formatDate[i] == '-')
                {
                    dateOnly.Add('.');
                }
                else
                {
                    dateOnly.Add(formatDate[i]);
                }
            }

            List<char> timeOnly = new List<char>();

            for (int i = 11; i < 16; i++)
            {
                timeOnly.Add(formatDate[i]);
            }

            formatDate = string.Empty;

            foreach (var item in dateOnly)
            {
                formatDate += item;
            }

            formatDate += " ";
            foreach (var item in timeOnly)
            {
                formatDate += item;
            }

            return formatDate;
        }

        /// <summary>
        /// Diese Funktion formatiert die Koordinaten, damit sie für Google Maps funktionieren
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public string FormatCoordinatesCorrectly(string coordinates)
        {
            StringBuilder sb = new StringBuilder(coordinates);
            for (int i = 0; i < coordinates.Length; i++)
            {
                if (coordinates[i] == ',')
                {
                    sb[i] = '.';
                }
            }

            coordinates = sb.ToString();
            return coordinates;
        }
    }
}
