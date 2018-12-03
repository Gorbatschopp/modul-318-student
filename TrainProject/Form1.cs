//-----------------------------------------------------------------------
// <copyright file="TrainProjekt.cs" company="Nikola Gerun">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TrainProject
{
    using System;
    using System.Collections.Generic;
    using System.Device.Location;
    using System.Windows.Forms;
    using SwissTransport;

    public partial class Form1 : Form
    {
        private static GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
        private static string textForMail = string.Empty;
        private static GeoCoordinate coord = new GeoCoordinate();

        private Transport transport = new Transport();
        private Format format = new Format();

        private string tempFromStation = string.Empty;
        private string tempToStation = string.Empty;
        private string timeQueryConnections;
        private string timeQueryBoard;
        private string departureTime;
        private string departureDate;

        private List<string> fromStationID = new List<string>();
        private List<string> fromCoordinates = new List<string>();
        private List<string> toCoordinates = new List<string>();
        private List<string> listForTextForMail = new List<string>();

        private bool skipRound = false;
        private bool startedLoop = false;

        private int departureOrArrival = 0;

        private Timer timer1 = new Timer();

        public Form1()
        {
            this.InitializeComponent();
            this.SetFromStationChangeButtons();
            this.SetToStationChangeButtons();
            GetLocationProperty();
            if (coord.IsUnknown)
            {
                findNearStation.Visible = false;
            }
        }

        private void UpdateTexts()
        {
            while (this.startedLoop)
            {
                if (!this.skipRound)
                {
                    if (FromStationText.Text != string.Empty && this.tempFromStation != FromStationText.Text)
                    {
                        FromCombo.Items.Clear();
                        this.SearchForStation(FromStationText.Text, 0);
                        this.SetFromStationChangeButtons();
                    }

                    this.tempFromStation = FromStationText.Text;

                    if (ToStationText.Text != string.Empty && this.tempToStation != ToStationText.Text)
                    {
                        ToCombo.Items.Clear();
                        if (ToStationText.Text == string.Empty)
                        {
                            ToCombo.SelectedItem = null;
                            this.SearchConnections();
                        }
                        else
                        {
                            this.SearchForStation(ToStationText.Text, 1);
                        }

                        this.SetToStationChangeButtons();
                    }

                    this.tempToStation = ToStationText.Text;
                    this.wait(1000);
                }
                else
                {
                    this.tempToStation = ToStationText.Text;
                    this.tempFromStation = FromStationText.Text;
                    this.skipRound = false;
                }
            }

            if (coord.IsUnknown)
            {
                GetLocationProperty();
            }
        }

        private void SetFromStationChangeButtons()
        {
            if (FromStationText.Text == string.Empty)
            {
                btnClearFromStation.Enabled = false;
            }
            else
            {
                btnClearFromStation.Enabled = true;
            }

            if (FromCombo.Text == string.Empty)
            {
                btnFromMap.Enabled = false;
            }
            else
            {
                btnFromMap.Enabled = true;
            }

            if (FromCombo.Text != string.Empty && ToCombo.Text != string.Empty)
            {
                btnSwitchStation.Enabled = true;
            }
            else
            {
                btnSwitchStation.Enabled = false;
            }
        }

        private void SetToStationChangeButtons()
        {
            if (ToStationText.Text == string.Empty)
            {
                btnClearToStation.Enabled = false;
            }
            else
            {
                btnClearToStation.Enabled = true;
            }

            if (FromCombo.Text == string.Empty)
            {
                btnToMap.Enabled = false;
            }
            else
            {
                btnToMap.Enabled = true;
            }

            if (FromCombo.Text != string.Empty && ToCombo.Text != string.Empty)
            {
                btnSwitchStation.Enabled = true;
            }
            else
            {
                btnSwitchStation.Enabled = false;
            }
        }

        private void ChangesFromText(object sender, EventArgs e)
        {
            FromCombo.Items.Clear();
            this.SearchForStation(FromStationText.Text, 0);
            this.SetFromStationChangeButtons();
        }

        private void ChangesToText(object sender, EventArgs e)
        {
            ToCombo.Items.Clear();
            if (ToStationText.Text == string.Empty)
            {
                ToCombo.SelectedItem = null;
                this.SearchConnections();
            }
            else
            {
                SearchForStation(ToStationText.Text, 1);
            }
            this.SetToStationChangeButtons();
        }
        void SearchForStation(string stationName, int FromOrTo)
        {
            fromCoordinates.Clear();
            Stations AllStations = transport.GetStations(stationName);
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
                            fromStationID.Add(item.Id.ToString());
                        }
                        if (item.Coordinate != null)
                        {
                            fromCoordinates.Add(format.formatCoordinatesCorrectly(item.Coordinate.XCoordinate.ToString()) + "," + format.formatCoordinatesCorrectly(item.Coordinate.YCoordinate.ToString()));
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
                toCoordinates.Clear();
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
                        toCoordinates.Add(format.formatCoordinatesCorrectly(item.Coordinate.XCoordinate.ToString()) + "," + format.formatCoordinatesCorrectly(item.Coordinate.YCoordinate.ToString()));
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
            listForTextForMail.Clear();
            Connections Connections = transport.GetConnections(FromCombo.SelectedItem.ToString(), ToCombo.SelectedItem.ToString(), timeQueryConnections, arrivalOrDeparture());

            foreach (var item in Connections.ConnectionList)
            {
                dataConnections.Rows.Add(item.From.Station.Name, item.To.Station.Name, item.Duration, format.formatDateCorrectly(item.From.Departure), format.formatDateCorrectly(item.To.Arrival), item.Duration);
            }

            foreach (var item in Connections.ConnectionList)
            {
                listForTextForMail.Add("Von "+ item.From.Station.Name + " zu " + item.To.Station.Name + " für " + item.Duration + " h am " + format.formatDateCorrectly(item.From.Departure) + " um " + format.formatDateCorrectly(item.To.Arrival));
            }
        }
        void SearchForDepartures()
        {
            listForTextForMail.Clear();
            int maxAmountOfConnections = 5;
            int connectionsShown = 0;
            StationBoardRoot StationBoard = transport.GetStationBoard(FromCombo.SelectedItem.ToString(), fromStationID[FromCombo.SelectedIndex], timeQueryBoard);

            foreach (var item in StationBoard.Entries)
            {
                if (connectionsShown <= maxAmountOfConnections)
                {
                    dataConnections.Rows.Add(FromCombo.SelectedItem.ToString(), item.To, null, item.Stop.Departure, null);
                    connectionsShown++;
                }
            }
            connectionsShown = 0;
            foreach (var item in StationBoard.Entries)
            {
                if (connectionsShown <= maxAmountOfConnections)
                {
                    listForTextForMail.Add("Von " + FromCombo.SelectedItem.ToString() + " zu " + item.To + " am " + item.Stop.Departure);
                    connectionsShown++;
                }
            }
        }

        void FormatDateTimeForConnectionQuery()
        {
            departureDate = departureDatePicker.Value.Year + "-" + departureDatePicker.Value.Month + "-" + departureDatePicker.Value.Day;
            departureTime = txtDepartureTime.Text;

            timeQueryConnections = "&date=" + departureDate + "&time=" + departureTime;
        }
        void FormatDateTimeForBoardQuery()
        {
            departureDate = departureDatePicker.Value.Year + "-" + departureDatePicker.Value.Month + "-" + departureDatePicker.Value.Day;
            departureTime = txtDepartureTime.Text;

            timeQueryBoard = "&datetime=" + departureDate + departureTime;
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
            if (!String.IsNullOrEmpty(FromCombo.Text))
            {
                System.Diagnostics.Process.Start("https://www.google.com/maps/search/?api=1&query=" + fromCoordinates[FromCombo.SelectedIndex]);
            }
        }

        private void btnToMap_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ToCombo.Text))
            {
                System.Diagnostics.Process.Start("https://www.google.com/maps/search/?api=1&query=" + toCoordinates[ToCombo.SelectedIndex]);
            }

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
            FromCombo.Text = string.Empty;
            FromStationText.Text = string.Empty;
        }

        private void btnClearToStation_Click(object sender, EventArgs e)
        {
            ToCombo.Items.Clear();
            ToCombo.Text = string.Empty;
            ToStationText.Text = string.Empty;
        }

        private void btnSwitchStation_Click(object sender, EventArgs e)
        {
            skipRound = true;
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
        private void wait(int min)
        {
            if (min == 0 || min < 0) return;
            //Console.WriteLine("start wait timer");
            timer1.Interval = min;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                //Console.WriteLine("stop wait timer");
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void FromStationText_TextChanged(object sender, EventArgs e)
        {

            if (!startedLoop)
            {
                startedLoop = true;
                UpdateTexts();
            }
        }

        private void ToStationText_TextChanged(object sender, EventArgs e)
        {
            if (!startedLoop)
            {
                startedLoop = true;
                UpdateTexts();
            }

        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            startedLoop = false;
            timer1.Enabled = false;
        }

        static void GetLocationProperty()
        {
            // Do not suppress prompt, and wait 1000 milliseconds to start.
            watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));

            coord = watcher.Position.Location;

            if (coord.IsUnknown != true)
            {
                Console.WriteLine("Lat: {0}, Long: {1}",
                    coord.Latitude,
                    coord.Longitude);
            }
            else
            {
                Console.WriteLine("Unknown latitude and longitude.");
            }
        }

        private void findNearStation_Click(object sender, EventArgs e)
        {

            string Lattitude = coord.Latitude.ToString();
            string trimedLattitude = string.Empty;

            string Longitude = coord.Longitude.ToString();
            string trimedLongitude = string.Empty;

            bool pointAppeared = false;
            int amountOfNumbersAfterPoint = 0;

            foreach (var item in Lattitude)
            {
                if (item == ',')
                {
                    trimedLattitude += item;
                    pointAppeared = true;

                }
                else
                {
                    
                    if (amountOfNumbersAfterPoint < 6)
                    {
                        trimedLattitude += item;
                    }
                }
                if (pointAppeared)
                {
                    amountOfNumbersAfterPoint++;
                }
            }

            pointAppeared = false;
            amountOfNumbersAfterPoint = 0;

            foreach (var item in Longitude)
            {
                if (item == ',')
                {
                    trimedLongitude += item;
                    pointAppeared = true;

                }
                else
                {
                    if (amountOfNumbersAfterPoint < 6)
                    {
                        trimedLongitude += item;
                    }
                }
                if (pointAppeared)
                {
                    amountOfNumbersAfterPoint++;
                }

            }

            Stations nearStationBoard = transport.GetNearStationBoard(trimedLattitude, trimedLongitude);
            if (nearStationBoard.StationList.Count > 0)
            {
                FromCombo.Items.Clear();
                fromCoordinates.Clear();
                fromStationID.Clear();
                foreach (var item in nearStationBoard.StationList)
                {
                    fromCoordinates.Add(format.formatCoordinatesCorrectly(item.Coordinate.XCoordinate.ToString()) + "," + item.Coordinate.YCoordinate.ToString());

                    FromCombo.Items.Add(item.Name);

                    fromStationID.Add(item.Id.ToString());
                }
                FromCombo.SelectedIndex = 0;
            }

        }

        private void btnMakeMail_Click(object sender, EventArgs e)
        {
            foreach (var item in listForTextForMail)
            {
                textForMail += item + "\n";
            }
            MailForm Form2 = new MailForm(listForTextForMail);
            Form2.ShowDialog();
        }
    }
}