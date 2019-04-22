#ifdef GL_ES
precision mediump float;
#endif

uniform vec3      iResolution;           // viewport resolution (in pixels)
uniform float     iGlobalTime;           // shader playback time (in seconds)
uniform float     iChannelTime[4];       // channel playback time (in seconds)
uniform vec3      iChannelResolution[4]; // channel resolution (in pixels)
uniform vec4      iMouse;                // mouse pixel coords. xy: current (if MLB down), zw: click
uniform samplerXX iChannel0..3;          // input channel. XX = 2D/Cube
uniform vec4      iDate;                 // (year, month, day, time in seconds)
uniform float     iSampleRate;           // sound sample rate (i.e., 44100)

bool PixelInsideCircle( vec3 circle )
{
    return length(vec2(gl_FragCoord.xy - circle.xy)) < circle.z;
}

bool PixelOnCircleContour( vec3 circle )
{
    return PixelInsideCircle(circle) && !PixelInsideCircle( vec3(circle.xy,circle.z-1.0) );
}

void main( void )
{
    float timeFactor = (2.0+sin(iGlobalTime))/2.0;

    const int NB_CIRCLES=3;
    vec3 c[NB_CIRCLES];
    c[0] = vec3( 0.6, 0.4, 0.07 ) * iResolution;
    c[1] = vec3( 0.45, 0.69, 0.09 ) * iResolution;
    c[2] = vec3( 0.35, 0.58, 0.06 ) * iResolution;
    c[0].z = 0.09*iResolution.x*timeFactor;
    c[1].z = 0.1*iResolution.x*timeFactor;
    c[2].z = 0.07*iResolution.x*timeFactor;

    c[0].xy = iMouse.xy;

    bool keep = false;
    for ( int i = 0; i < NB_CIRCLES; ++i )
    {
        if ( !PixelOnCircleContour(c[i]) )
            continue;

        bool insideOther = false;
        for ( int j = 0; j < NB_CIRCLES; ++j )
        {
            if ( i == j )
                continue;

            if ( PixelInsideCircle(c[j]) )
                insideOther = true;
        }

        keep = keep || !insideOther;
    }

    if ( keep )
        gl_FragColor = vec4(1.0,1.0,0.0,1.0); 
}