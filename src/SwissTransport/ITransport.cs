namespace SwissTransport
{
    public interface ITransport
    {
        Stations GetStations(string query);
        StationBoardRoot GetStationBoard(string station, string id, string departDate);
        Connections GetConnections(string fromStation, string toStattion, string departureDate, int arrivalOrDepart);
    }
}