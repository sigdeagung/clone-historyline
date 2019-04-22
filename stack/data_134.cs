public void CycleCams()
        {
        StartCoroutine(_cycle());
        }

    private IEnumerator _cycle()
        {
        WebCamDevice[] d = WebCamTexture.devices;
        if ( d.Length <= 1 )
            {
            Debug.Log("only one.");
            if (d.Length==1) Debug.Log("Name is " +d[0].name);
            yield break;
            }

        Debug.Log("0 " +d[0].name);
        Debug.Log("1 " +d[1].name);

        if ( wct.deviceName == d[0].name )
            {
            wct.Stop();
            yield return new WaitForSeconds(.1f);
            wct.deviceName = d[1].name;
            yield return new WaitForSeconds(.1f);
            wct.Play();

            nameDisplay.text = "\"" +wct.deviceName +"\"";
            yield break;
            }

        if ( wct.deviceName == d[1].name )
            {
            wct.Stop();
            yield return new WaitForSeconds(.1f);
            wct.deviceName = d[0].name;
            yield return new WaitForSeconds(.1f);
            wct.Play();

            nameDisplay.text = "\"" +wct.deviceName +"\"";
            yield break;
            }
        }