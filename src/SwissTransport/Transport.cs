 using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SwissTransport
{
    public class Transport : ITransport
    {
        public Stations GetStations(string query)
        {
            var request = CreateWebRequest("http://transport.opendata.ch/v1/locations?query=" + query);
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();

                if (responseStream != null)
                {
                    var message = new StreamReader(responseStream).ReadToEnd();
                    var stations = JsonConvert.DeserializeObject<Stations>(message
                        , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    return stations;
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("Es sieht so aus, als ob es einen Fehler gab.\n " +
                    "Bitte überprüfen Sie, dass Sie mit dem Internet verbunden sind", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            return null;
        }

        public StationBoardRoot GetStationBoard(string station, string id, string departDate)
        {
            var request = CreateWebRequest("http://transport.opendata.ch/v1/stationboard?Station=" + station + "&id=" + id + departDate);
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();

                if (responseStream != null)
                {
                    var readToEnd = new StreamReader(responseStream).ReadToEnd();
                    var stationboard =
                        JsonConvert.DeserializeObject<StationBoardRoot>(readToEnd);
                    return stationboard;
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("Es sieht so aus, als ob es einen Fehler gab.\n " +
                    "Bitte überprüfen Sie, dass Sie mit dem Internet verbunden sind", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public Connections GetConnections(string fromStation, string toStattion, string departDate, int arrivalOrDepart)
        {
            var request = CreateWebRequest("http://transport.opendata.ch/v1/connections?from=" + fromStation + "&to=" + toStattion + departDate + "&isArrivalTime=" + arrivalOrDepart);
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();

                if (responseStream != null)
                {
                    var readToEnd = new StreamReader(responseStream).ReadToEnd();
                    var connections =
                        JsonConvert.DeserializeObject<Connections>(readToEnd);
                    return connections;
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("Es sieht so aus, als ob es einen Fehler gab.\n " +
                    "Bitte überprüfen Sie, dass Sie mit dem Internet verbunden sind", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private static WebRequest CreateWebRequest(string url)
        {
            var request = WebRequest.Create(url);
            var webProxy = WebRequest.DefaultWebProxy;

            webProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Proxy = webProxy;
            
            return request;
        }

        public Stations GetNearStationBoard(string XCoord, string YCoord) //http://transport.opendata.ch/v1/locations?x=47.547408&y=7.589547
        {
            var request = CreateWebRequest("http://transport.opendata.ch/v1/locations?x=" + XCoord + "&y=" + YCoord);
            try
            {
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();

                if (responseStream != null)
                {
                    var readToEnd = new StreamReader(responseStream).ReadToEnd();
                    var stationboard = JsonConvert.DeserializeObject<Stations>(readToEnd);
                    return stationboard;
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("Es sieht so aus, als ob es einen Fehler gab.\n " +
                    "Bitte überprüfen Sie, dass Sie mit dem Internet verbunden sind", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }
    }
}
