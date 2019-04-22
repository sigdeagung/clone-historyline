void Start()
{
    unsafe
    {
        fixed (TrafficRoadSystem** addressOfTrafficRoadSystem = &trafficRoadSystem)
        {
            string osmPath = "Assets/Resources/map.osm.pbf";
            int results;
            results = traffic_import_osm(osmPath, addressOfTrafficRoadSystem);
        }
    }
}
IntPtr ptr;
TrafficRoadSystem _struct;
try {
    ptr = Marshal.AllocHGlobal(Marshal.SizeOf(_struct));
    Marshal.StructureToPtr(_struct, ptr, false);
} finally {
    Marshal.FreeHGlobal(ptr);
}