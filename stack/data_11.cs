private float bodyRot = 0F;
private float FOV = 70f;

void LateUpdate() {
    if ( neckBone != null ) {
        Quaternion camRotQ = CameraFacing.transform.rotation * Quaternion.Euler( 0,0,-90);
        neckBone.transform.rotation = camRotQ;
        float camRot = camRotQ.eulerAngles.y;

        float delta = camRot- bodyRot;
        if ( delta > 180 ) {
            delta -= 360;
        }
        if ( delta < -180 ) {
            delta += 360;
        }
        if ( Math.Abs(delta) > FOV ) {
            if ((delta > FOV || delta < -180) && delta < 180) {
                bodyRot = camRot - FOV;
            }
            delta = camRot- bodyRot;
            if ((delta < FOV || delta > 180 ) ) {
                bodyRot = camRot + FOV;
            }
        }
        playerObj.transform.rotation =  Quaternion.Euler(0, bodyRot, 0); 
        CameraFacing.transform.position = cameraMount.transform.position;
    }
}

// Transform of the full body of the character.
Transform body;
// Transform of the head (child of |body| component).
Transform head;
// Maximum delta angle in degrees.
float maxAngle = 60.0f;

void RotateCharacter(Quaternion target) {
  // Rotate head as much as possible without exceeding the joint angle.
  head.rotation = Quaternion.RotateTowards (body.rotation, target, maxAngle);
  // Complete the remainder of the rotation by body.
  body.rotation = target * Quaternion.Inverse (head.localRotation);
}