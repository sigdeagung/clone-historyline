private bool fading = false;

IEnumerator FadeInAnimation(string level){

    if (fading) yield break;
    fading = true;

    canvas.SetActive (true);

    anim.Play ("FadeIn");

    yield return new WaitForSeconds (1f);

    Application.LoadLevel (level);

    anim.Play ("FadeOut");

    yield return new WaitForSeconds (2f);

    canvas.SetActive (false);

    fading = false;

}