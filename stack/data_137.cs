public class MaterialRandomiser : MonoBehaviour {

  [SerializeField]
  private Material[] _materials;
  [SerializeField]
  private Renderer _renderer;

  public void Start () {
    ChangeMaterial();
  }

  public void Reset () {
    _renderer = GetComponent<Renderer>();
  }

  public void ChangeMaterial () {
    _renderer.material = SelectRandomMaterial();
  }

  private Material SelectRandomMaterial () {
    return _materials[Random.Range(0, _materials.Length)];
  }

}
public GameObject eggPrefab;
public Vector3 spawnPos;
public Material mat;

void Start()
{
    GameObject obj = Instantiate(eggPrefab, spawnPos, Quaternion.identity);
    obj.GetComponent<MeshRenderer>().material = mat;
}

public Material[] materialsArray;
    public GameObject prefab;
    private Vector3 pos = new Vector3(0, 0, 0);
    private Quaternion rot = Quaternion.identity;

    private void Start()
    {
        Material mat = RandomMaterial(materialsArray);
        InstantiateWithMaterial(prefab, pos, rot, mat);
    }

    public Material RandomMaterial(Material[] _array_)
    {
        return _array_[Random.Range(0, _array_.Length)];
    }

    public void InstantiateWithMaterial(GameObject _prefab_, Vector3 _pos_, Quaternion _rot_, Material _mat_)
    {
        GameObject obj_ = Instantiate(_prefab_, _pos_, _rot_);
        obj_.gameObject.GetComponent<MeshRenderer>().material = _mat_;
    }