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

        private List<string> fromStationID = new List<string>();
        private List<string> fromCoordinates = new List<string>();
        private List<string> toCoordinates = new List<string>();
        private List<string> listForTextForMail = new List<string>();

        private bool skipRound = false;
        private bool startedLoop = false;

        private Timer timer1 = new Timer();

        public Form1()
        {
            this.InitializeComponent();
            this.SetStationChangeButtons();
            GetLocationProperty();
            if (coord.IsUnknown)
            {
                this.findNearStation.Visible = false;
            }
        }

        /// <summary>
        /// Diese Funktion wird zum ersten mal ausgeführt, wenn ein Text verändert wird.
        /// Danach wird sie jede Sekunde aufgerufen und überprüft ob sich etwas geändert hat.
        /// </summary>
        private void UpdateTexts()
        {
            while (this.startedLoop)
            {
                if (!this.skipRound)
                {
                    if (this.tempFromStation != this.FromStationText.Text)
                    {
                        this.FromCombo.Items.Clear();
                        this.SearchForStation(this.FromStationText.Text, 0);
                        if (this.FromStationText.Text == string.Empty)
                        {
                            this.FromCombo.Text = string.Empty;
                        }
                    }

                    this.tempFromStation = this.FromStationText.Text;

                    if (this.tempToStation != this.ToStationText.Text)
                    {
                        this.ToCombo.Items.Clear();
                        if (this.ToStationText.Text == string.Empty)
                        {
                            this.ToCombo.Text = string.Empty;
                            this.UseApi();
                        }
                        else
                        {
                            this.SearchForStation(this.ToStationText.Text, 1);
                        }
                    }

                    if (this.ToStationText.Text != string.Empty)
                    {
                        this.groupBox1.Visible = true;
                    }
                    else
                    {
                        this.groupBox1.Visible = false;
                    }

                    SetStationChangeButtons();

                    this.tempToStation = this.ToStationText.Text;
                    this.Wait(1000);
                }
                else
                {
                    this.tempToStation = this.ToStationText.Text;
                    this.tempFromStation = this.FromStationText.Text;
                    this.skipRound = false;
                }
            }

            if (coord.IsUnknown)
            {
                GetLocationProperty();
            }
            else
            {
                this.findNearStation.Visible = true;
            }
        }

        /// <summary>
        /// Diese Funktion kümmert sich darum, alle Buttons an bzw. aus zu schalten.
        /// </summary>
        private void SetStationChangeButtons()
        {
            if (this.FromStationText.Text == string.Empty)
            {
                this.btnClearFromStation.Enabled = false;
            }
            else
            {
                this.btnClearFromStation.Enabled = true;
            }

            if (this.FromCombo.Text == string.Empty)
            {
                this.btnFromMap.Enabled = false;
            }
            else
            {
                this.btnFromMap.Enabled = true;
            }

            if (this.FromCombo.Text != string.Empty && this.ToCombo.Text != string.Empty)
            {
                this.btnSwitchStation.Enabled = true;
            }
            else
            {
                this.btnSwitchStation.Enabled = false;
            }
            if (this.ToStationText.Text == string.Empty)
            {
                this.btnClearToStation.Enabled = false;
            }
            else
            {
                this.btnClearToStation.Enabled = true;
            }

            if (this.ToCombo.Text == string.Empty)
            {
                this.btnToMap.Enabled = false;
            }
            else
            {
                this.btnToMap.Enabled = true;
            }

            if (this.FromCombo.Text != string.Empty && this.ToCombo.Text != string.Empty)
            {
                this.btnSwitchStation.Enabled = true;
            }
            else
            {
                this.btnSwitchStation.Enabled = false;
            }
        }

        /// <summary>
        /// Diese Funktion sucht nach den Stationen welche man in den Textboxen eingibt und schreibt diese in die Comboboxen
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="fromOrTo"></param>
        private void SearchForStation(string stationName, int fromOrTo)
        {
            this.fromCoordinates.Clear();
            Stations allStations = this.transport.GetStations(stationName);
            if (fromOrTo == 0)
            {
                List<string> fromComboList = new List<string>();
                try
                {
                    foreach (var item in allStations.StationList)
                    {
                        if (item.Name != null)
                        {
                            fromComboList.Add(item.Name);

                            if (item.Id != null)
                            {
                                this.fromStationID.Add(item.Id.ToString());
                            }

                            if (item.Coordinate != null)
                            {
                                this.fromCoordinates.Add(this.format.FormatCoordinatesCorrectly(item.Coordinate.XCoordinate.ToString()) + "," + this.format.FormatCoordinatesCorrectly(item.Coordinate.YCoordinate.ToString()));
                            }
                        }
                    }

                    foreach (var item in fromComboList)
                    {
                        this.FromCombo.Items.Add(item);
                    }

                    if (this.FromCombo.Items.Count > 0)
                    {
                        this.FromCombo.SelectedIndex = 0;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Es sieht so aus, als ob keine Verbindungen gefunden werden konnten.\n" +
                    "Bitte überprüfen Sie Ihre Eingabe oder versichern Sie sich, dass Sie mit dem Internet verbunden sind", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                
            }
            else if (fromOrTo == 1)
            {
                this.toCoordinates.Clear();
                List<string> toComboList = new List<string>();
                try
                {
                    foreach (var item in allStations.StationList)
                    {
                        if (item.Name != null)
                        {
                            toComboList.Add(item.Name);
                        }

                        if (item.Coordinate != null)
                        {
                            this.toCoordinates.Add(this.format.FormatCoordinatesCorrectly(item.Coordinate.XCoordinate.ToString()) + "," + this.format.FormatCoordinatesCorrectly(item.Coordinate.YCoordinate.ToString()));
                        }
                    }

                    foreach (var item in toComboList)
                    {
                        this.ToCombo.Items.Add(item);
                    }

                    if (this.ToCombo.Items.Count > 0)
                    {
                        this.ToCombo.SelectedIndex = 0;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Es sieht so aus, als ob keine Verbindungen gefunden werden konnten.\n" +
                    "Bitte überprüfen Sie Ihre Eingabe oder versichern Sie sich, dass Sie mit dem Internet verbunden sind", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }

            this.UseApi();
        }

        /// <summary>
        /// Diese Funktion entscheidet, ob die Verbindungen gesucht werden oder ob die Abfahrtstafel angezeigt werden soll
        /// Nach diesem Prinzip entscheidet sie dann, welche Funktionen von nun an ausgeführt werden sollen
        /// </summary>
        private void UseApi()
        {
            this.dataConnections.Rows.Clear();
            string fromListItem = null;

            if (this.FromCombo.SelectedItem != null)
            {
                fromListItem = this.FromCombo.SelectedItem.ToString();
            }

            string toListItem = null;

            if (this.ToCombo.SelectedItem != null)
            {
                toListItem = this.ToCombo.SelectedItem.ToString();
            }

            if (!string.IsNullOrEmpty(fromListItem) && !string.IsNullOrEmpty(toListItem))
            {
                this.SearchForConnections();
            }
            else if (!string.IsNullOrEmpty(fromListItem))
            {
                this.SearchForDepartures();
            }
        }

        /// <summary>
        /// Diese Funktion such nach den Verbindungen zwischen den zwei Stationen
        /// </summary>
        private void SearchForConnections()
        {
            this.listForTextForMail.Clear();
            Connections connections = this.transport.GetConnections(this.FromCombo.SelectedItem.ToString(), this.ToCombo.SelectedItem.ToString(), this.timeQueryConnections, this.ArrivalOrDeparture());
            try
            {
                foreach (var item in connections.ConnectionList)
                {
                    this.dataConnections.Rows.Add(item.From.Station.Name, item.To.Station.Name, item.Duration, this.format.FormatDateCorrectly(item.From.Departure), this.format.FormatDateCorrectly(item.To.Arrival), item.Duration);
                }

                foreach (var item in connections.ConnectionList)
                {
                    this.listForTextForMail.Add("Von "+ item.From.Station.Name + " zu " + item.To.Station.Name + " für " + item.Duration + " h am " + this.format.FormatDateCorrectly(item.From.Departure) + " um " + this.format.FormatDateCorrectly(item.To.Arrival));
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Es sieht so aus, als ob keine Verbindungen gefunden werden konnten.\n " +
                    "Bitte überprüfen Sie Ihre Eingabe oder versichern Sie sich, dass Sie mit dem Internet verbunden sind", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// Diese Funktion schreibt die Abfahrtstafel der Station rein
        /// </summary>
        private void SearchForDepartures()
        {
            this.listForTextForMail.Clear();
            int maxAmountOfConnections = 5;
            int connectionsShown = 0;
            StationBoardRoot stationBoard = this.transport.GetStationBoard(this.FromCombo.SelectedItem.ToString(), this.fromStationID[this.FromCombo.SelectedIndex], this.timeQueryBoard);
            try
            {
                foreach (var item in stationBoard.Entries)
                {
                    if (connectionsShown <= maxAmountOfConnections)
                    {
                        this.dataConnections.Rows.Add(this.FromCombo.SelectedItem.ToString(), item.To, null, item.Stop.Departure, null);
                        connectionsShown++;
                    }
                }

                connectionsShown = 0;
                foreach (var item in stationBoard.Entries)
                {
                    if (connectionsShown <= maxAmountOfConnections)
                    {
                        this.listForTextForMail.Add("Von " + this.FromCombo.SelectedItem.ToString() + " zu " + item.To + " am " + item.Stop.Departure);
                        connectionsShown++;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Es sieht so aus, als ob keine Abfahrtstafel gefunden werden konnte.\n " +
                    "Bitte überprüfen Sie Ihre Eingabe oder versichern Sie sich, dass Sie mit dem Internet verbunden sind", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtDepartureTime.Text = DateTime.Now.ToString("HH:mm");
        }

        private void BtnFromMap_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.FromCombo.Text))
            {
                System.Diagnostics.Process.Start("https://www.google.com/maps/search/?api=1&query=" + this.fromCoordinates[this.FromCombo.SelectedIndex]);
            }
        }

        private void BtnToMap_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ToCombo.Text))
            {
                System.Diagnostics.Process.Start("https://www.google.com/maps/search/?api=1&query=" + this.toCoordinates[this.ToCombo.SelectedIndex]);
            }
        }

        /// <summary>
        /// Diese Funktion schaut nach, wie die Einstellung für Arrival und Departure eingestellt ist
        /// </summary>
        /// <returns></returns>
        private int ArrivalOrDeparture()
        {
            if (this.btnArriveAt.Checked)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void BtnArriveAt_CheckedChanged(object sender, EventArgs e)
        {
            this.UseApi();
        }

        private void BtnClearFromStation_Click(object sender, EventArgs e)
        {
            this.FromCombo.Items.Clear();
            this.FromCombo.Text = string.Empty;
            this.FromStationText.Text = string.Empty;
        }

        private void BtnClearToStation_Click(object sender, EventArgs e)
        {
            this.ToCombo.Items.Clear();
            this.ToCombo.Text = string.Empty;
            this.ToStationText.Text = string.Empty;
        }

        private void BtnSwitchStation_Click(object sender, EventArgs e)
        {
            this.skipRound = true;
            string tempFromText = this.FromStationText.Text;
            string tempFromCombo = this.FromCombo.Text;
            List<string> tempFromList = new List<string>();
            foreach (string item in this.FromCombo.Items)
            {
                tempFromList.Add(item);
            }

            this.FromCombo.Items.Clear();
            foreach (string item in this.ToCombo.Items)
            {
                this.FromCombo.Items.Add(item);
            }

            this.FromStationText.Text = this.ToStationText.Text;
            this.FromCombo.Text = this.ToCombo.Text;

            this.ToCombo.Items.Clear();
            foreach (string item in tempFromList)
            {
                this.ToCombo.Items.Add(item);
            }

            this.ToStationText.Text = tempFromText;
            this.ToCombo.Text = tempFromCombo;
        }

        /// <summary>
        /// Diese Funktion wurde gemacht, damit eine Funktion warten kann, aber das Program während dem noch funktioniert
        /// </summary>
        /// <param name="min"></param>
        private void Wait(int min)
        {
            if (min == 0 || min < 0) return;
            this.timer1.Interval = min;
            this.timer1.Enabled = true;
            this.timer1.Start();
            this.timer1.Tick += (s, e) =>
            {
                this.timer1.Enabled = false;
                this.timer1.Stop();
            };
            while (this.timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            this.startedLoop = false;
            this.timer1.Enabled = false;
        }

        private static void GetLocationProperty()
        {
            watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));
            coord = watcher.Position.Location;
        }

        private void FindNearStation_Click(object sender, EventArgs e)
        {
            string lattitude = coord.Latitude.ToString();
            string trimedLattitude = string.Empty;

            string longitude = coord.Longitude.ToString();
            string trimedLongitude = string.Empty;

            bool pointAppeared = false;
            int amountOfNumbersAfterPoint = 0;

            foreach (var item in lattitude)
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

            foreach (var item in longitude)
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

            Stations nearStationBoard = this.transport.GetNearStationBoard(trimedLattitude, trimedLongitude);
            try
            {
                this.FromCombo.Items.Clear();
                this.fromCoordinates.Clear();
                this.fromStationID.Clear();
                foreach (var item in nearStationBoard.StationList)
                {
                    this.fromCoordinates.Add(this.format.FormatCoordinatesCorrectly(item.Coordinate.XCoordinate.ToString()) + "," + item.Coordinate.YCoordinate.ToString());
                    this.FromCombo.Items.Add(item.Name);
                    this.fromStationID.Add(item.Id.ToString());
                }

                this.FromCombo.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Es sieht so aus, als ob keine Station in Ihrer nähe gefunden werden konnte, alternativ könnte es auch an Ihrem Internet liegen", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void BtnMakeMail_Click(object sender, EventArgs e)
        {
            foreach (var item in this.listForTextForMail)
            {
                textForMail += item + "\n";
            }

            MailForm form2 = new MailForm(this.listForTextForMail);
            form2.ShowDialog();
        }

        private void StationText_TextChanged(object sender, EventArgs e)
        {
            if (!this.startedLoop)
            {
                this.startedLoop = true;
                this.UpdateTexts();
            }
        }

        private void ComboTextChanged(object sender, EventArgs e)
        {
            this.UseApi();
        }

        private void TimeOrDateChanged(object sender, EventArgs e)
        {
            this.FromCombo.Items.Clear();
            this.FromCombo.Text = string.Empty;
            this.FromStationText.Text = string.Empty;
        }
    }
}