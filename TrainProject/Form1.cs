using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwissTransport;

namespace TrainProject
{
    public partial class Form1 : Form
    {
        Transport Transport = new Transport();
        List<string> FromStationID = new List<string>();
        string TimeQueryConnections;
        string TimeQueryBoard;
        string departureTime;
        string departureDate;
        List<string> fromCoordinates = new List<string>();
        List<string> toCoordinates = new List<string>();
        int DepartureOrArrival = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void ChangesFromText(object sender, EventArgs e)
        {
            FromCombo.Items.Clear();
            SearchForStation(FromStationText.Text, 0);
        }

        private void ChangesToText(object sender, EventArgs e)
        {
            ToCombo.Items.Clear();
            if (ToStationText.Text == "")
            {
                ToCombo.SelectedItem = null;
                SearchConnections();
            }
            else
            {
                SearchForStation(ToStationText.Text, 1);
            }
        }
        void SearchForStation(string stationName, int FromOrTo)
        {
            Stations AllStations = Transport.GetStations(stationName);
            if (FromOrTo == 0)
            {
                List<string> FromComboList = new List<string>();

                foreach (var item in AllStations.StationList)
                {
                    if (item.Name != null)
                    {
                        FromComboList.Add(item.Name);

                        if (item.Id != null)
                        {
                            FromStationID.Add(item.Id.ToString());
                        }
                        if (item.Coordinate != null)
                        {
                            fromCoordinates.Add(formatCoordinatesCorrectly(item.Coordinate.XCoordinate.ToString()) + "," + formatCoordinatesCorrectly(item.Coordinate.YCoordinate.ToString()));
                        }
                        
                     }
                }

                foreach (var item in FromComboList)
                {
                    FromCombo.Items.Add(item);
                }

                if (FromCombo.Items.Count > 0)
                {
                    FromCombo.SelectedIndex = 0;
                }
            }
            else if (FromOrTo == 1)
            {
                List<string> ToComboList = new List<string>();
                if (groupBox1.Visible == false)
                {
                    groupBox1.Visible = true;
                }

                foreach (var item in AllStations.StationList)
                {
                    if (item.Name != null)
                    {
                        ToComboList.Add(item.Name);
                    }
                    if (item.Coordinate != null)
                    {
                        toCoordinates.Add(formatCoordinatesCorrectly(item.Coordinate.XCoordinate.ToString()) + "," + formatCoordinatesCorrectly(item.Coordinate.YCoordinate.ToString()));
                    }
                }

                foreach (var item in ToComboList)
                {
                    ToCombo.Items.Add(item);
                }

                if (ToCombo.Items.Count > 0)
                {
                    ToCombo.SelectedIndex = 0;
                }
            }
            renameSearchButton();
            SearchConnections();
        }

        private void SearchConnections()
        {
            dataConnections.Rows.Clear();
            string FromListItem = null;

            if (FromCombo.SelectedItem != null)
            {
                FromListItem = FromCombo.SelectedItem.ToString();
            }
            string ToListItem = null;

            if (ToCombo.SelectedItem != null)
            {
                ToListItem = ToCombo.SelectedItem.ToString();
            }

            if (!string.IsNullOrEmpty(FromListItem) && !string.IsNullOrEmpty(ToListItem))
            {
                SearchForConnections();
            }
            else if (!string.IsNullOrEmpty(FromListItem))
            {
                SearchForDepartures();
            }
            else
            {
                //Error pls no
            }
        }


        void SearchForConnections()
        {
            Connections Connections = Transport.GetConnections(FromCombo.SelectedItem.ToString(), ToCombo.SelectedItem.ToString(), TimeQueryConnections, arrivalOrDeparture());

            foreach (var item in Connections.ConnectionList)
            {
                dataConnections.Rows.Add(item.From.Station.Name, item.To.Station.Name, item.Duration, formatDateCorrectly(item.From.Departure), formatDateCorrectly(item.To.Arrival), item.Duration);
            }
        }
        void SearchForDepartures()
        {
            int maxAmountOfConnections = 5;
            int connectionsShown = 0;
            StationBoardRoot StationBoard = Transport.GetStationBoard(FromCombo.SelectedItem.ToString(), FromStationID[FromCombo.SelectedIndex], TimeQueryBoard);

            foreach (var item in StationBoard.Entries)
            {
                if (connectionsShown <= maxAmountOfConnections)
                {
                    dataConnections.Rows.Add(FromCombo.SelectedItem.ToString(), item.To, null, item.Stop.Departure, null);
                    connectionsShown++;
                }
            }
        }

        string formatDateCorrectly(string FormatDate) //2018-11-27T15:07:00+0100
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

        void FormatDateTimeForConnectionQuery()
        {
            departureDate = departureDatePicker.Value.Year + "-" + departureDatePicker.Value.Month + "-" + departureDatePicker.Value.Day;
            departureTime= txtDepartureTime.Text;

            TimeQueryConnections = "&date=" + departureDate + "&time=" + departureTime;
        }
        void FormatDateTimeForBoardQuery()
        {
            departureDate = departureDatePicker.Value.Year + "-" + departureDatePicker.Value.Month + "-" + departureDatePicker.Value.Day;
            departureTime = txtDepartureTime.Text;

            TimeQueryBoard = "&datetime=" + departureDate + departureTime;
        }

        void renameSearchButton()
        {
            if (FromCombo.Items.Count >= 1 && ToCombo.Items.Count >= 1)
            {
                label3.Text = "Verbindungen zwischen den Stationen";
            }
            else if (ToCombo.Items.Count < 1 && FromCombo.Items.Count >= 1)
            {
                label3.Text = "Fahrplan von der Station";
            }
        }

        private void ComboFromTextChanged(object sender, EventArgs e)
        {
            SearchConnections();
        }

        private void ComboToTextChanged(object sender, EventArgs e)
        {
            SearchConnections();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtDepartureTime.Text = DateTime.Now.ToString("HH:mm");
        }

        private void txtDepartureTime_TextChanged(object sender, EventArgs e)
        {
            FormatDateTimeForConnectionQuery();
            FormatDateTimeForBoardQuery();
            SearchConnections();
        }

        private void departureDatePicker_ValueChanged(object sender, EventArgs e)
        {
            FormatDateTimeForBoardQuery();
            FormatDateTimeForConnectionQuery();
            SearchConnections();
        }

        private void btnFromMap_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(FromCombo.SelectedText))
            {
                System.Diagnostics.Process.Start("https://www.google.com/maps/search/?api=1&query=" + fromCoordinates[FromCombo.SelectedIndex]);
            }
        }

        private void btnToMap_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ToCombo.SelectedText))
            {
                System.Diagnostics.Process.Start("https://www.google.com/maps/search/?api=1&query=" + toCoordinates[ToCombo.SelectedIndex]);
            }
                
        }
        string formatCoordinatesCorrectly(string coordinates)
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
        int arrivalOrDeparture()
        {
            if (btnArriveAt.Checked)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void btnArriveAt_CheckedChanged(object sender, EventArgs e)
        {
            SearchConnections();
        }

        private void btnClearFromStation_Click(object sender, EventArgs e)
        {
            FromCombo.Items.Clear();
            FromCombo.Text = "";
            FromStationText.Text = "";
        }

        private void btnClearToStation_Click(object sender, EventArgs e)
        {
            ToCombo.Items.Clear();
            ToCombo.Text = "";
            ToStationText.Text = "";
        }

        private void btnSwitchStation_Click(object sender, EventArgs e)
        {
            string tempFromText = FromStationText.Text;
            string tempFromCombo = FromCombo.Text;
            List<string> tempFromList = new List<string>();
            foreach (string item in FromCombo.Items)
            {
                tempFromList.Add(item);
            }

            FromCombo.Items.Clear();
            foreach (string item in ToCombo.Items)
            {
                FromCombo.Items.Add(item);
            }
            FromStationText.Text = ToStationText.Text;
            FromCombo.Text = ToCombo.Text;

            ToCombo.Items.Clear();
            foreach (string item in tempFromList)
            {
                ToCombo.Items.Add(item);
            }
            ToStationText.Text = tempFromText;
            ToCombo.Text = tempFromCombo;
        }
    }
}