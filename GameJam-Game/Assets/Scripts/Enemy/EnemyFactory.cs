using System;
using System.Collections;
using System.Collections.Generic;
using Nidavellir.PathManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Nidavellir
{
    public class EnemyFactory : MonoBehaviour
    {
        // Start is called before the first frame update
        
        [SerializeField] public GameObject enemy;
        [SerializeField] private float timer;
        [SerializeField] private Path path;
        [SerializeField] private List<GameObject> enemyList;
        private int m_idx;
        
        void Start()
        {
            this.timer = 0.5f;
            this.m_idx = 0;
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            if (!(timer <= 0.0f)) return;
            if (m_idx < enemyList.Count)
            {
                SpawnEnemy(enemyList[m_idx]);
                m_idx += 1;
            }
            timer = 0.5f;
        }

        private void SpawnEnemy(GameObject e)
        {
            Enemy tmp = Instantiate(e).GetComponent<Enemy>();
            float x = Random.Range(-1.0f, 1.0f);
            float z = Random.Range(-1.0f, 1.0f);
            Debug.Log("x:" + x + " y:" + z);
            tmp.transform.position = new Vector3(x, 0.0f, z);
            tmp.Path = this.path;
        }
    }
}

