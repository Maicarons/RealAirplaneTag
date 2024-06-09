namespace RealAirplaneTag;

public class planeFunc
{
    public planeFunc(PlaneType pt)
    {
        Plane = pt;
    }

    public PlaneType Plane { get; set; }
    public string WTCtoName()
    {
        switch (Plane.Wtc)
        {
            
            case Wtc.Light:
                return "超轻型";
            case Wtc.LightMedium:
                return "轻型";
            case Wtc.Medium:
                return "中型";
            case  Wtc.Heavy:
                return "大型";
            case Wtc.Super:
                return "超大型";
            default:
                return "中型";
        }
    }
    public int WtCtoLevel()
    {
        switch (Plane.Wtc)
        {
            
            case Wtc.Light:
                return 1;
            case Wtc.LightMedium:
                return 2;
            case Wtc.Medium:
                return 3;
            case  Wtc.Heavy:
                return 4;
            case Wtc.Super:
                return 5;
            default:
                return 3;
        }
    }
}