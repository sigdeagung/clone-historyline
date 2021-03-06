public static bool HasComponent <T>(this GameObject obj) where T:Component
    {
    return obj.GetComponent<T>() != null;
    }
    public static class ExtensionsHandy // this class name is irrelevant and not used
    {

    public static bool HasComponent <T>(this GameObject obj) where T:Component
        {
        return obj.GetComponent<T>() != null;
        }

    public static bool IsNear(this float ff, float target)
        {
        float difference = ff-target;
        difference = Mathf.Abs(difference);
        if ( difference < 20f ) return true;
        else return false;
        }

    public static float Jiggle(this float ff)
        {
        return ff * UnityEngine.Random.Range(0.9f,1.1f);
        }

    public static Color Colored( this float alpha, int r, int g, int b )
        {
        return new Color(
            (float)r / 255f,
            (float)g / 255f,
            (float)b / 255f,
            alpha );
        }

    }

    // the unbelievably useful array handling category for games!

public static T AnyOne<T>(this T[] ra) where T:class
    {
    int k = ra.Length;
    int r = UnityEngine.Random.Range(0,k);
    return ra[r];
    }