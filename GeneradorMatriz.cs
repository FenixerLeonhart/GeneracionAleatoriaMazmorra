using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GeneradorMatriz : MonoBehaviour
{
    public GameObject Personaje,ObjetoPadre,PiezaPrefab;
    public GameObject[] Habitaciones;
    [SerializeField] int ancho, largo, SalasMaximas;
    private GameObject[,] Matriz;
    [SerializeField] List<int> anchoR = new List<int>();
    [SerializeField] List<int> largoR = new List<int>();

    // Variables
    int atras = 0;
    int centro = 1;
    int adelante = 2;

    void Start()
    {
        //Inicializar tamaño de matriz
        Matriz = new GameObject[ancho, largo];

        CrearEsquema();
        CrearPasillos();
        UltimosRetoques();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CrearEsquema()
    {
        //Recorrer posicion de matriz y crear una pieza en cada una
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < largo; j++)
            {
                Vector3 PosicionMatriz = new Vector3(i, -2.3f, j);
                GameObject puzzlePiece = Instantiate(PiezaPrefab, PosicionMatriz, Quaternion.identity);
                puzzlePiece.transform.parent = transform;
                Matriz[i, j] = puzzlePiece;
            }
        }
        // obtener la posicion del medio
        int middleRow = (int) Mathf.Floor(ancho / 2);
        int middleColumn = (int) Mathf.Floor(largo / 2);
        // acceder al gameobject de la posicion del medio
        GameObject PiezaDelMedio = Matriz[middleRow, middleColumn];
        // Guardar la posicion en una lista
        anchoR.Add(middleRow);
        largoR.Add(middleColumn);
        // hacer algo con el gameobject
        PiezaDelMedio.GetComponent<Renderer>().material.color = Color.green;

        // Por las Salas Maximas Inicializadas al principio crear direccion de generacion hasta tantas salas posibles
        for (int i = 0; i < SalasMaximas; i++)
        {
            
            int direccion = Random.Range(0,4);

            GameObject PiezaSeleccionada = Matriz[middleRow, middleColumn];
            switch(direccion){
                case 0: 
                    middleColumn++;

                    if(middleRow >= 0 && middleRow < ancho && middleColumn >= 0 && middleColumn < largo){
                        PiezaSeleccionada = Matriz[middleRow, middleColumn];

                        if(PiezaSeleccionada.GetComponent<Renderer>().material.color != Color.green) 
                        {
                            PiezaSeleccionada.GetComponent<Renderer>().material.color = Color.green;
                            anchoR.Add(middleRow);
                            largoR.Add(middleColumn);
                        }else{
                            i--;
                            middleColumn--;
                        }
                    }else{
                        i--;
                        middleColumn--;
                    }
                break;
                case 1:
                    middleColumn--;
                    
                    if(middleRow >= 0 && middleRow < ancho && middleColumn >= 0 && middleColumn < largo){
                        PiezaSeleccionada = Matriz[middleRow, middleColumn];

                        if(PiezaSeleccionada.GetComponent<Renderer>().material.color != Color.green) 
                        {
                            PiezaSeleccionada.GetComponent<Renderer>().material.color = Color.green;
                            anchoR.Add(middleRow);
                            largoR.Add(middleColumn);
                        }else{
                            i--;
                            middleColumn++;
                        }
                    }else{
                        i--;
                        middleColumn++;
                    }
                break;
                case 2:
                    middleRow++;
                    
                    if(middleRow >= 0 && middleRow < ancho && middleColumn >= 0 && middleColumn < largo){
                        PiezaSeleccionada = Matriz[middleRow, middleColumn];

                        if(PiezaSeleccionada.GetComponent<Renderer>().material.color != Color.green) 
                        {
                            PiezaSeleccionada.GetComponent<Renderer>().material.color = Color.green;
                            anchoR.Add(middleRow);
                            largoR.Add(middleColumn);
                        }else{
                            i--;
                            middleRow--;
                        }
                    }else{
                        i--;
                        middleRow--;
                    }
                break;
                case 3:
                    middleRow--;

                    if(middleRow >= 0 && middleRow < ancho && middleColumn >= 0 && middleColumn < largo){
                        PiezaSeleccionada = Matriz[middleRow, middleColumn];

                        if(PiezaSeleccionada.GetComponent<Renderer>().material.color != Color.green) 
                        {
                            PiezaSeleccionada.GetComponent<Renderer>().material.color = Color.green;
                            anchoR.Add(middleRow);
                            largoR.Add(middleColumn);
                        }else{
                            i--;
                            middleRow++;
                        }
                    }else{
                        i--;
                        middleRow++;
                    }
                break;

            }
            
        }
    }

    void CrearPasillos()
    {
        // Detectar que pasillo es el siguiente para saber cual construir en el centro
        if(anchoR[1] == anchoR[0]+1 && largoR[1] == largoR[0]){
            // Esta a la derecha 
            Matriz[anchoR[0],largoR[0]] = Instantiate(Habitaciones[3], new Vector3(anchoR[0], 0, largoR[0]), Quaternion.identity);
        }
        if(anchoR[1] == anchoR[0]-1 && largoR[1] == largoR[0]){
            // Esta a la izquierda
            Matriz[anchoR[0],largoR[0]] = Instantiate(Habitaciones[2], new Vector3(anchoR[0], 0, largoR[0]), Quaternion.identity);

        }
        if(anchoR[1] == anchoR[0] && largoR[1] == largoR[0] + 1){
            // Esta a la arriba
            Matriz[anchoR[0],largoR[0]] = Instantiate(Habitaciones[0], new Vector3(anchoR[0], 0, largoR[0]), Quaternion.identity);
        }
        if(anchoR[1] == anchoR[0] && largoR[1] == largoR[0] - 1){
            // Esta a la abajo
            Matriz[anchoR[0],largoR[0]] = Instantiate(Habitaciones[1], new Vector3(anchoR[0], 0, largoR[0]), Quaternion.identity);

        }
        
        for(int c = 0; c < anchoR.Count-1; c++)
        {
            Debug.Log("Numeros: " + atras + " " + centro + " " + adelante + " Cuenta Elem: " + anchoR.Count);
            if(adelante < anchoR.Count){
                if(anchoR[centro] == anchoR[atras]+1 && largoR[centro] == largoR[atras] && anchoR[adelante] == anchoR[centro]+1 && largoR[adelante] == largoR[centro]){
                // Esta a la derecha el de atras y el de delante

                    Instantiate(Habitaciones[5], new Vector3(anchoR[centro], 0, largoR[centro]), Quaternion.identity);
                }
                else if(anchoR[centro] == anchoR[atras]-1 && largoR[centro] == largoR[atras] && anchoR[adelante] == anchoR[centro]-1 && largoR[adelante] == largoR[centro]){
                    // Esta a la izquierda el de atras y el de delante
                    
                    Instantiate(Habitaciones[5], new Vector3(anchoR[centro], 0, largoR[centro]), Quaternion.identity);
                }
                else if(anchoR[centro] == anchoR[atras] && largoR[centro] == largoR[atras]+1 && anchoR[adelante] == anchoR[centro] && largoR[adelante] == largoR[centro]+1){
                    // Esta a arriba el de atras y el de delante
                    
                    Instantiate(Habitaciones[4], new Vector3(anchoR[centro], 0, largoR[centro]), Quaternion.identity);
                }
                else if(anchoR[centro] == anchoR[atras] && largoR[centro] == largoR[atras]-1 && anchoR[adelante] == anchoR[centro] && largoR[adelante] == largoR[centro]-1){
                    // Esta a abajo el de atras y el de delante
                    
                    Instantiate(Habitaciones[4], new Vector3(anchoR[centro], 0, largoR[centro]), Quaternion.identity);
                }
                else if(anchoR[centro] == anchoR[atras] && largoR[centro] == largoR[atras]+1 && anchoR[adelante] == anchoR[centro]-1 && largoR[adelante] == largoR[centro] || anchoR[centro] == anchoR[atras]+1 && largoR[centro] == largoR[atras] && anchoR[adelante] == anchoR[centro] && largoR[adelante] == largoR[centro]-1){
                    // Esta a abajo el de atras y el siguiente a la izquierda
                    
                    Instantiate(Habitaciones[9], new Vector3(anchoR[centro], 0, largoR[centro]), Quaternion.identity);
                }
                else if(anchoR[centro] == anchoR[atras] && largoR[centro] == largoR[atras]-1 && anchoR[adelante] == anchoR[centro]-1 && largoR[adelante] == largoR[centro] || anchoR[centro] == anchoR[atras]+1 && largoR[centro] == largoR[atras] && anchoR[adelante] == anchoR[centro] && largoR[adelante] == largoR[centro]+1){
                    // Esta a arriba el de atras y el siguiente a la izquierda
                   
                    Instantiate(Habitaciones[7], new Vector3(anchoR[centro], 0, largoR[centro]), Quaternion.identity);
                }
                else if(anchoR[centro] == anchoR[atras]-1 && largoR[centro] == largoR[atras] && anchoR[adelante] == anchoR[centro] && largoR[adelante] == largoR[centro]+1 || anchoR[centro] == anchoR[atras] && largoR[centro] == largoR[atras]-1 && anchoR[adelante] == anchoR[centro]+1 && largoR[adelante] == largoR[centro]){
                    // Esta a arriba el de atras y el siguiente a la derecha
                    Instantiate(Habitaciones[6], new Vector3(anchoR[centro], 0, largoR[centro]), Quaternion.identity);
                }
                else if(anchoR[centro] == anchoR[atras]-1 && largoR[centro] == largoR[atras] && anchoR[adelante] == anchoR[centro] && largoR[adelante] == largoR[centro]-1 || anchoR[centro] == anchoR[atras] && largoR[centro] == largoR[atras]+1 && anchoR[adelante] == anchoR[centro]+1 && largoR[adelante] == largoR[centro]){
                    // Esta a abajo el de atras y el siguiente a la derecha
                    Instantiate(Habitaciones[8], new Vector3(anchoR[centro], 0, largoR[centro]), Quaternion.identity);
                }
                atras++;
                centro++;
                adelante++;
            }

            
        }

        //Aca hacer que en el ultimo lugar aparezca la ultima sala

    }

    void UltimosRetoques()
    {
        GameObject[] Salas = GameObject.FindGameObjectsWithTag("Salas");

        for(int i = 0; i < Salas.Length; i++)
        {
            Salas[i].transform.SetParent(ObjetoPadre.transform);
        }

        ObjetoPadre.transform.localScale = new Vector3(5,5,5);

        Personaje.transform.position = Salas[0].transform.position;

        GameObject[] Sobras = GameObject.FindGameObjectsWithTag("Esquema");

        for(int i = 0; i < Sobras.Length; i++)
        {
            Destroy(Sobras[i]);
        }
    }

}
