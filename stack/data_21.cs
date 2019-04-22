public interface ISingleFingerHandler
    {
    void OnSingleFingerDown (Vector2 position);
    voidpublic interface ISingleFingerHandler
    {
    void OnSingleFingerDown (Vector2 position);
    void OnSingleFingerUp (Vector2 position);
    void OnSingleFingerDrag (Vector2 delta);
    }

/* note, Unity chooses to have "one interface for each action"
however here we are dealing with a consistent paradigm ("dragging")
which has three parts; I feel it's better to have one interface
forcing the consumer to have the three calls (no problem if empty) */


using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SingleFingerInputModule:MonoBehaviour,
                IPointerDownHandler,IPointerUpHandler,IDragHandler

    {
    private ISingleFingerHandler needsUs = null;
    // of course that would be a List,
    // just one shown for simplicity in this example code

    private int currentSingleFinger = -1;
    private int kountFingersDown = 0;

    void Awake()
        {
        needsUs = GetComponent(typeof(ISingleFingerHandler)) as ISingleFingerHandler;
        // of course, you may prefer this to search the whole scene,
        // just this gameobject shown here for simplicity
        // alternately it's a very good approach to have consumers register
        // for it. to do so just add a register function to the interface.
        }

    public void OnPointerDown(PointerEventData data)
        {
        kountFingersDown = kountFingersDown + 1;

        if (currentSingleFinger == -1 && kountFingersDown == 1)
            {
            currentSingleFinger = data.pointerId;
            if (needsUs != null) needsUs.OnSingleFingerDown(data.position);
            }
        }

    public void OnPointerUp (PointerEventData data)
        {
        kountFingersDown = kountFingersDown - 1;

        if ( currentSingleFinger == data.pointerId )
            {
            currentSingleFinger = -1;
            if (needsUs != null) needsUs.OnSingleFingerUp(data.position);
            }
        }

    public void OnDrag (PointerEventData data)
        {
        if ( currentSingleFinger == data.pointerId && kountFingersDown == 1 )
            {
            if (needsUs != null) needsUs.OnSingleFingerDrag(data.delta);
            }
        }

    } OnSingleFingerUp (Vector2 position);
    void OnSingleFingerDrag (Vector2 delta);
    }

/* note, Unity chooses to have "one interface for each action"
however here we are dealing with a consistent paradigm ("dragging")
which has three parts; I feel it's better to have one interface
forcing the consumer to have the three calls (no problem if empty) */


using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SingleFingerInputModule:MonoBehaviour,
                IPointerDownHandler,IPointerUpHandler,IDragHandler

    {
    private ISingleFingerHandler needsUs = null;
    // of course that would be a List,
    // just one shown for simplicity in this example code

    private int currentSingleFinger = -1;
    private int kountFingersDown = 0;

    void Awake()
        {
        needsUs = GetComponent(typeof(ISingleFingerHandler)) as ISingleFingerHandler;
        // of course, you may prefer this to search the whole scene,
        // just this gameobject shown here for simplicity
        // alternately it's a very good approach to have consumers register
        // for it. to do so just add a register function to the interface.
        }

    public void OnPointerDown(PointerEventData data)
        {
        kountFingersDown = kountFingersDown + 1;

        if (currentSingleFinger == -1 && kountFingersDown == 1)
            {
            currentSingleFinger = data.pointerId;
            if (needsUs != null) needsUs.OnSingleFingerDown(data.position);
            }
        }

    public void OnPointerUp (PointerEventData data)
        {
        kountFingersDown = kountFingersDown - 1;

        if ( currentSingleFinger == data.pointerId )
            {
            currentSingleFinger = -1;
            if (needsUs != null) needsUs.OnSingleFingerUp(data.position);
            }
        }

    public void OnDrag (PointerEventData data)
        {
        if ( currentSingleFinger == data.pointerId && kountFingersDown == 1 )
            {
            if (needsUs != null) needsUs.OnSingleFingerDrag(data.delta);
            }
        }

    }

    public class FingerMove:MonoBehaviour, ISingleFingerHandler
    {
    public void OnSingleFingerDown(Vector2 position) {}
    public void OnSingleFingerUp (Vector2 position) {}
    public void OnSingleFingerDrag (Vector2 delta)
        {
        _processSwipe(delta);
        }

    private void _processSwipe(Vector2 screenTravel)
        {
        .. move the camera or whatever ..
        }
    }

    /*
IPinchHandler - strict two sequential finger pinch Handling

Put this daemon ON TO the game object, with a consumer of the service.

(Note, as always, the "philosophy" of a glass gesture is up to you.
There are many, many subtle questions; eg should extra fingers block,
can you 'swap primary' etc etc etc - program it as you wish.)
*/


public interface IPinchHandler
    {
    void OnPinchStart ();
    void OnPinchEnd ();
    void OnPinchZoom (float gapDelta);
    }

/* note, Unity chooses to have "one interface for each action"
however here we are dealing with a consistent paradigm ("pinching")
which has three parts; I feel it's better to have one interface
forcing the consumer to have the three calls (no problem if empty) */


using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class PinchInputModule:MonoBehaviour,
                IPointerDownHandler,IPointerUpHandler,IDragHandler

    {
    private IPinchHandler needsUs = null;
    // of course that would be a List,
    // just one shown for simplicity in this example code

    private int currentFirstFinger = -1;
    private int currentSecondFinger = -1;
    private int kountFingersDown = 0;
    private bool pinching = false;

    private Vector2 positionFirst = Vector2.zero;
    private Vector2 positionSecond = Vector2.zero;
    private float previousDistance = 0f;
    private float delta = 0f;

    void Awake()
        {
        needsUs = GetComponent(typeof(IPinchHandler)) as IPinchHandler;
        // of course, this could search the whole scene,
        // just this gameobject shown here for simplicity
        }

    public void OnPointerDown(PointerEventData data)
        {
        kountFingersDown = kountFingersDown + 1;

        if (currentFirstFinger == -1 && kountFingersDown == 1)
            {
            // first finger must be a pure first finger and that's that

            currentFirstFinger = data.pointerId;
            positionFirst = data.position;

            return;
            }

        if (currentFirstFinger != -1 && currentSecondFinger == -1 && kountFingersDown == 2)
            {
            // second finger must be a pure second finger and that's that

            currentSecondFinger = data.pointerId;
            positionSecond = data.position;

            FigureDelta();

            pinching = true;
            if (needsUs != null) needsUs.OnPinchStart();
            return;
            }

        }

    public void OnPointerUp (PointerEventData data)
        {
        kountFingersDown = kountFingersDown - 1;

        if ( currentFirstFinger == data.pointerId )
            {
            currentFirstFinger = -1;

            if (pinching)
                {
                pinching = false;
                if (needsUs != null) needsUs.OnPinchEnd();
                }
            }

        if ( currentSecondFinger == data.pointerId )
            {
            currentSecondFinger = -1;

            if (pinching)
                {
                pinching = false;
                if (needsUs != null) needsUs.OnPinchEnd();
                }
            }

        }

    public void OnDrag (PointerEventData data)
        {

        if ( currentFirstFinger == data.pointerId )
            {
            positionFirst = data.position;
            FigureDelta();
            }

        if ( currentSecondFinger == data.pointerId )
            {
            positionSecond = data.position;
            FigureDelta();
            }

        if (pinching)
            {
            if ( data.pointerId == currentFirstFinger || data.pointerId == currentSecondFinger )
                {
                if (kountFingersDown==2)
                    {
                    if (needsUs != null) needsUs.OnPinchZoom(delta);
                    }
                return;
                }
            }
        }

    private void FigureDelta()
        {
        float newDistance = Vector2.Distance(positionFirst, positionSecond);
        delta = newDistance - previousDistance;
        previousDistance = newDistance;
        }

    }
    ublic class FingerMove:MonoBehaviour, ISingleFingerHandler, IPinchHandler
    {
    public void OnSingleFingerDown(Vector2 position) {}
    public void OnSingleFingerUp (Vector2 position) {}
    public void OnSingleFingerDrag (Vector2 delta)
        {
        _processSwipe(delta);
        }

    public void OnPinchStart () {}
    public void OnPinchEnd () {}
    public void OnPinchZoom (float delta)
        {
        _processPinch(delta);
        }

    private void _processSwipe(Vector2 screenTravel)
        {
        .. handle drag (perhaps move LR/UD)
        }

    private void _processPinch(float delta)
        {
        .. handle zooming (perhaps move camera in-and-out)
        }
    }