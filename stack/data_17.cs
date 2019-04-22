//Upper left corner
    Vector3[] v = new Vector3[4];
    image.rectTransform.GetWorldCorners(v);
    var recPos = v[1];

    var recWidth = image.rectTransform.sizeDelta.x;
    var imgWidth = image.sprite.texture.width;
    var realOffsetX = offsetX * (recWidth / imgWidth);
    var realPosX = recPos.x + realOffsetX;

    private Vector3 GetPositionOffset(Image image, float offsetX, float offsetY)
{
    //Upper left corner
    Vector3[] v = new Vector3[4];
    image.rectTransform.GetWorldCorners(v);
    var recPos = v[1];

    //X coordinate
    var recWidth = image.rectTransform.sizeDelta.x;
    var imgWidth = image.sprite.texture.width;
    var realOffsetX = offsetX * (recWidth / imgWidth);
    var realPosX = recPos.x + realOffsetX;

    //Y coordinate
    var recHeight = image.rectTransform.sizeDelta.y;
    var imgHeight = image.sprite.texture.height;
    var realOffsetY = offsetY * (recWidth / imgWidth);
    var realPosY = recPos.y - realOffsetY;

    //Position
    return new Vector3(realPosX, realPosY, image.transform.position.z);
}