using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


[System.Serializable]
public class BioStickData
{
    public bool mpu;
    public int x;
    public int y;
    public int z;
    public bool mov;
    public long delta;
    public string boton;
}


public class ControladorBioStick : MonoBehaviour
{
    private string url = "http://192.168.4.1/json";
    private string botonActual = "Ninguno";
    private string botonAnterior = "Ninguno";


    void Start()
    {
        StartCoroutine(ConsultarESP32());
    }


    IEnumerator ConsultarESP32()
    {
        while (true)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();


                if (!www.isNetworkError && !www.isHttpError)
                {
                    string jsonResult = www.downloadHandler.text;
                    BioStickData datos = JsonUtility.FromJson<BioStickData>(jsonResult);
                   
                    botonActual = datos.boton;


                    if (botonActual != botonAnterior)
                    {
                        if (botonActual != "Ninguno")
                        {
                            Debug.Log("¡Botón presionado: " + botonActual + "!");
                        }
                        botonAnterior = botonActual;
                    }
                }
            }


            yield return new WaitForSeconds(0.1f);
        }
    }
}