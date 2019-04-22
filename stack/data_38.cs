Vector2 textureCoordToHexGridCoord(int textX, int textY)
    {
        Vector2 hexGridCoord = new Vector2();
        float m = hexH / hexR;

        int xsection = (int)(textX / (hexH + hexS));
        int ysection = (int)(textY / (2 * hexR));

        int xSectionPixel = (int)(textX - xsection * (hexH + hexS));
        int ySectionPixel = (int)(textY - ysection * (2 * hexR));

        //A Section
        if(xsection % 2 == 0)
        {
            hexGridCoord.x = xsection;
            hexGridCoord.y = ysection;

            if(xSectionPixel < (hexH - ySectionPixel * m))
            {
                hexGridCoord.x--;
                hexGridCoord.y--;
            }

            if(xSectionPixel < (-hexH + ySectionPixel * m))
            {
                hexGridCoord.x--;
            }
        }

        //B Section
        else
        {
            if(xSectionPixel >= hexR)
            {
                if(ySectionPixel < (2 * hexH - xSectionPixel * m))
                {
                    hexGridCoord.x = xsection - 1;
                    hexGridCoord.y = ysection - 1;
                }
                else
                {
                    hexGridCoord.x = xsection;
                    //hexGridCoord.y = ysection;
                    hexGridCoord.y = ysection - 1;
                }
            }

            if(xSectionPixel < hexR)
            {
                if(ySectionPixel < (xSectionPixel * m))
                {
                    hexGridCoord.x = xsection;
                    //hexGridCoord.y = ysection - 1;
                    hexGridCoord.y = ysection;
                }
                else
                {
                    hexGridCoord.x = xsection - 1;
                    hexGridCoord.y = ysection;
                }
            }
        }

        return hexGridCoord;
    }