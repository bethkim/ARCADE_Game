using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn; // Aquí pondrás tus objetos buenos y malos
    public float spawnRate = 1.5f; // Cada cuánto tiempo sale un objeto
    public float minForce = 12f;
    public float maxForce = 16f;
    public float maxTorque = 10f; // Rotación al azar
    public float xRange = 4f; // Qué tan ancho es el espacio donde aparecen

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            
            if (gameManager.isGameActive)
            {
                // Elegir un objeto al azar de la lista
                int index = Random.Range(0, prefabsToSpawn.Count);
                
                // Posición aleatoria en el eje X
                Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), transform.position.y, 0);
                
                // Crear el objeto
                GameObject spawnedObj = Instantiate(prefabsToSpawn[index], spawnPos, Quaternion.identity);
                
                // Darle un empujón hacia arriba y hacerlo girar
                Rigidbody rb = spawnedObj.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * Random.Range(minForce, maxForce), ForceMode.Impulse);
                rb.AddTorque(Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), ForceMode.Impulse);
            }
        }
    }
}