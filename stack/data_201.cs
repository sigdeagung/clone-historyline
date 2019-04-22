void OnTriggerEnter(Collider other){

    if (other.tag == "Enemy") {

        other.GetComponent<BasicEnemy>().setHealthLower(damage);

    }

    if (other.tag != "Turret" && other.tag != "Bullet") {

        Vector3 reflectposition = -2 * (Vector3.Dot(transform.position, hit.normal) * hit.normal - transform.position);
        reflectposition.y = this.transform.position.y;
        this.transform.LookAt(reflectposition);


    }


}

void Update () {

    Ray cast = new Ray(transform.position, transform.forward);

    Physics.Raycast (cast, out hit);

    transform.position += transform.forward * speed * Time.deltaTime;

}