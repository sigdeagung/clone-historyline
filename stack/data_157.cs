

void Main()
{
    var config = Config.Qualities[MaterialQualities.Low];
    var cost = config.Cost;
    var value = config.Value;
}

public static class Config
{
    public static Dictionary<MaterialQualities, MaterialQuality> Qualities =
        new Dictionary<MaterialQualities, MaterialQuality>
        {
            { MaterialQualities.Low, new MaterialQuality { Value = 0.1F, Cost = 10 }},
            { MaterialQualities.Medium, new MaterialQuality { Value = 0.2F, Cost = 20 }}, 
            { MaterialQualities.High, new MaterialQuality { Value = 0.2F, Cost = 40 }},
        };  
}

public class MaterialQuality
{
    public float Value { get; set; }
    public int Cost { get; set; }
}

public enum MaterialQualities
{
    Low, Medium, High
}

public static class Config
{
    public class Material
    {
        public Material(float value, int cost){
            Value = value;
            Cost = cost;
        }

        public float Value {get; private set;}
        public int Cost {get; private set;}

        public Material GetFor(MaterialQuality quality){
             switch(quality){
                 case MaterialQuality.Low: return new Material(0.1f, 10);
                 case MaterialQuality.Medium: return new Material(0.2f, 20);
                 case MaterialQuality.High: return new Material(0.2f, 40);
             }
             throw new Exception("Unknown material quality " + quality);
        }

    }
}