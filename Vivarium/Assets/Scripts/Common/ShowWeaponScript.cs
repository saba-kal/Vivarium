using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWeaponScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> weaponList;

    GameObject currentWeaponModel;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void SpawnRandomWeapon()
    {
        var randInex = Random.Range(0, 2);
        var obj = GameObject.Instantiate(weaponList[randInex], this.transform);
        obj.transform.SetParent(this.gameObject.transform);
    }

    public void SetWeapon(GameObject weaponModel)
    {
        Destroy(currentWeaponModel);
        if (weaponModel == null)
        {
            return;
        }
        currentWeaponModel = GameObject.Instantiate(weaponModel, this.transform);
        currentWeaponModel.transform.SetParent(this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
