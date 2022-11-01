using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject shellPrefab;
    public GameObject shellSpawnPos;
    public GameObject target;
    public GameObject parent;   
    float speed = 15;
    float turnspeed = 2;
    bool canShoot = true;
    
    void Start()
    {
        
    }

    void CanShootAgain()
    {
        canShoot = true;
    }

    void Fire()
    {
        if (canShoot)
        {
            GameObject shell = Instantiate(shellPrefab, shellSpawnPos.transform.position, shellSpawnPos.transform.rotation);
            shell.GetComponent<Rigidbody>().velocity = speed * this.transform.forward;
            canShoot = false;
            Invoke("CanShootAgain", 0.1f);
        }
    }

   
    void Update()
    {
        
        Vector3 direction = (target.transform.position - parent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        parent.transform.rotation = Quaternion.Slerp(parent.transform.rotation, lookRotation, Time.deltaTime * turnspeed);

        float? angel = RotateTurrte();
        if (angel != null && Vector3.Angle(direction, parent.transform.forward) < 10)
        {
            Fire();
        }
        
    }


    float? RotateTurrte()
    {
        float? angel = CalculationAngle(true);

        if (angel != null)
        {
            this.transform.localEulerAngles = new Vector3(360f - (float)angel, 0f, 0f);
        }
        return angel;

    }
    float? CalculationAngle(bool low)
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float gravity = 9.81f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngel = sSqr + root;
            float lowAngel = sSqr - root;
            if (low)
            {
                return (Mathf.Atan2(lowAngel, gravity * x) * Mathf.Rad2Deg);
            }
            else
            {
                return (Mathf.Atan2(highAngel, gravity * x) * Mathf.Rad2Deg);
            }
        }
        else
        {
            return null;
        }


    }
}
