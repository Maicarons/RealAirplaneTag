namespace RealAirplaneTag;

public class PlaneTag
{
    public readonly string CallSign;
    public readonly string AirType;
    public readonly int SizeOfPlane;

    public PlaneTag(string callSign, string airType, int sizeOfPlane)
    {
        CallSign = callSign;
        AirType = airType;
        SizeOfPlane = sizeOfPlane;
    }
}